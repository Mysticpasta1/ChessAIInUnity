package com.mystic.chessultima.ui;

import com.mystic.chessultima.engine.Ai;
import com.mystic.chessultima.model.Board;
import com.mystic.chessultima.model.Move;
import com.mystic.chessultima.model.Piece;
import com.mystic.chessultima.model.PieceType;
import com.mystic.chessultima.net.NetworkManager;
import javafx.animation.PauseTransition;
import javafx.application.Application;
import javafx.application.Platform;
import javafx.concurrent.Task;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.ChoiceDialog;
import javafx.scene.control.Label;
import javafx.scene.control.TextInputDialog;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.stage.Stage;
import javafx.util.Duration;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

public class ChessUltimaApp extends Application {

    private enum Mode { LOCAL_PVP, VS_AI, AI_VS_AI, ONLINE }

    private static final List<String> LEVELS = List.of(
            "1 – Easy", "2 – Normal", "3 – Hard", "4 – Expert", "5 – Master", "6 – Grandmaster");
    private static final int[] LEVEL_DEPTH = {1, 2, 3, 4, 5, 6};
    private static final long[] LEVEL_BUDGET = {800, 1500, 3000, 5000, 8000, 12000};

    private Stage stage;
    private PieceImages images;
    private BoardView boardView;

    private Board board;
    private Mode mode = Mode.LOCAL_PVP;

    private boolean humanIsWhite = true;   // VS_AI: colour the human plays
    private Ai ai;
    private Ai aiWhite, aiBlack;           // AI_VS_AI: one engine per side
    private int gameSession;               // bumped on new game / menu to stop stale AI loops

    private NetworkManager net;
    private boolean localPlaysWhite = true; // ONLINE

    private int selX = -1, selY = -1;
    private List<Move> selectedMoves = new ArrayList<>();
    private Move lastMove;
    private boolean gameOver;
    private boolean aiThinking;

    private Label statusLabel;
    private Label turnLabel;

    @Override
    public void start(Stage stage) {
        this.stage = stage;
        this.images = new PieceImages();
        stage.setTitle("Chess-Ultima");
        showMenu();
        stage.setWidth(1024);
        stage.setHeight(768);
        stage.setOnCloseRequest(e -> { if (net != null) net.close(); });
        stage.show();
    }

    // ---- menu ---------------------------------------------------------------

    private void showMenu() {
        gameSession++; // stop any running AI-vs-AI loop
        if (net != null) { net.close(); net = null; }
        Label title = new Label("Chess-Ultima");
        title.setStyle("-fx-font-size: 40px; -fx-font-weight: bold; -fx-text-fill: #f0e6d2;");
        Label subtitle = new Label("A 24x24 fairy-chess variant  •  JavaFX edition");
        subtitle.setStyle("-fx-font-size: 15px; -fx-text-fill: #b9b0a0;");

        Button local = menuButton("Local 2-Player");
        local.setOnAction(e -> startLocal());
        Button vsAi = menuButton("Play vs Computer");
        vsAi.setOnAction(e -> startVsAiDialog());
        Button aiVsAi = menuButton("AI vs AI (watch)");
        aiVsAi.setOnAction(e -> startAiVsAiDialog());
        Button host = menuButton("Online – Host Game");
        host.setOnAction(e -> hostDialog());
        Button join = menuButton("Online – Join Game");
        join.setOnAction(e -> joinDialog());
        Button fen = menuButton("Load Position (FEN)…");
        fen.setOnAction(e -> loadFenDialog());
        Button quit = menuButton("Quit");
        quit.setOnAction(e -> { if (net != null) net.close(); Platform.exit(); });

        VBox box = new VBox(12, title, subtitle, spacer(), local, vsAi, aiVsAi, host, join, fen, quit);
        box.setAlignment(Pos.CENTER);
        box.setPadding(new Insets(40));
        box.setStyle("-fx-background-color: #2b2b2b;");
        stage.setScene(new Scene(box));
    }

    private Button menuButton(String text) {
        Button b = new Button(text);
        b.setPrefWidth(260);
        b.setStyle("-fx-font-size: 15px; -fx-padding: 10 16;");
        return b;
    }

    private Label spacer() {
        Label l = new Label();
        l.setPadding(new Insets(8));
        return l;
    }

    // ---- game setup ---------------------------------------------------------

    private void startLocal() {
        mode = Mode.LOCAL_PVP;
        board = new Board();
        prepareGameScene(false);
    }

    private void startVsAiDialog() {
        ChoiceDialog<String> colour = new ChoiceDialog<>("White", List.of("White", "Black"));
        colour.setHeaderText("Choose your colour");
        colour.setContentText("You play as:");
        Optional<String> c = colour.showAndWait();
        if (c.isEmpty()) return;
        humanIsWhite = c.get().equals("White");

        Ai a = askDifficulty("Choose difficulty");
        if (a == null) return;

        mode = Mode.VS_AI;
        ai = a;
        board = new Board();
        prepareGameScene(!humanIsWhite);
        maybeTriggerAi();
    }

    private void startAiVsAiDialog() {
        Ai w = askDifficulty("White computer difficulty");
        if (w == null) return;
        Ai b = askDifficulty("Black computer difficulty");
        if (b == null) return;

        mode = Mode.AI_VS_AI;
        aiWhite = w;
        aiBlack = b;
        board = new Board();
        prepareGameScene(false);
        maybeTriggerAi();
    }

    /** Show the difficulty picker and build an engine, or null if cancelled. */
    private Ai askDifficulty(String header) {
        ChoiceDialog<String> diff = new ChoiceDialog<>(LEVELS.get(1), LEVELS);
        diff.setHeaderText(header);
        diff.setContentText("Difficulty:");
        Optional<String> d = diff.showAndWait();
        if (d.isEmpty()) return null;
        int idx = Math.max(0, LEVELS.indexOf(d.get()));
        return new Ai(LEVEL_DEPTH[idx], LEVEL_BUDGET[idx]);
    }

    private void hostDialog() {
        Integer port = askPort("Host Game", "Port to listen on:");
        if (port == null) return;
        ChoiceDialog<String> colour = new ChoiceDialog<>("White", List.of("White", "Black"));
        colour.setHeaderText("You are the host – choose your colour");
        colour.setContentText("Host plays as:");
        Optional<String> c = colour.showAndWait();
        if (c.isEmpty()) return;
        boolean hostWhite = c.get().equals("White");

        mode = Mode.ONLINE;
        board = new Board();
        net = new NetworkManager();
        net.host(port, hostWhite, board.toFen(), netListener());
        localPlaysWhite = hostWhite;
        prepareGameScene(!hostWhite);
        setStatus("Hosting on port " + port + " – waiting for opponent…");
    }

    private void joinDialog() {
        TextInputDialog addr = new TextInputDialog("127.0.0.1");
        addr.setHeaderText("Join Game");
        addr.setContentText("Host address:");
        Optional<String> a = addr.showAndWait();
        if (a.isEmpty() || a.get().isBlank()) return;
        Integer port = askPort("Join Game", "Host port:");
        if (port == null) return;

        mode = Mode.ONLINE;
        board = new Board();
        net = new NetworkManager();
        prepareGameScene(false);
        setStatus("Connecting to " + a.get() + ":" + port + "…");
        net.join(a.get().trim(), port, netListener());
    }

    private Integer askPort(String header, String content) {
        TextInputDialog dlg = new TextInputDialog("5555");
        dlg.setHeaderText(header);
        dlg.setContentText(content);
        Optional<String> r = dlg.showAndWait();
        if (r.isEmpty()) return null;
        try {
            return Integer.parseInt(r.get().trim());
        } catch (NumberFormatException e) {
            alert("Invalid port number.");
            return null;
        }
    }

    private void loadFenDialog() {
        TextInputDialog dlg = new TextInputDialog(Board.START_FEN);
        dlg.setHeaderText("Load a position from FEN");
        dlg.setContentText("FEN:");
        dlg.getEditor().setPrefWidth(560);
        Optional<String> r = dlg.showAndWait();
        if (r.isEmpty() || r.get().isBlank()) return;
        try {
            mode = Mode.LOCAL_PVP;
            board = Board.fromFen(r.get().trim());
            prepareGameScene(false);
        } catch (RuntimeException e) {
            alert("Could not parse that FEN string.");
        }
    }

    // ---- game scene ---------------------------------------------------------

    private void prepareGameScene(boolean flipped) {
        gameSession++; // invalidate any AI loop from a previous game
        selX = selY = -1;
        selectedMoves = new ArrayList<>();
        lastMove = null;
        gameOver = false;
        aiThinking = false;

        boardView = new BoardView(images);
        boardView.setFlipped(flipped);
        boardView.setOnSquareClicked(this::onSquareClicked);

        Button menu = new Button("← Menu");
        menu.setOnAction(e -> showMenu());
        Button flip = new Button("Flip");
        flip.setOnAction(e -> { boardView.setFlipped(!isFlipped()); flippedToggle = !flippedToggle; refresh(); });
        Button zin = new Button("+");
        zin.setOnAction(e -> boardView.zoomIn());
        Button zout = new Button("−");
        zout.setOnAction(e -> boardView.zoomOut());
        Button zreset = new Button("Reset");
        zreset.setOnAction(e -> boardView.resetZoom());

        turnLabel = new Label();
        turnLabel.setStyle("-fx-text-fill: #f0e6d2; -fx-font-weight: bold;");
        statusLabel = new Label("");
        statusLabel.setStyle("-fx-text-fill: #b9b0a0;");

        HBox bar = new HBox(8, menu, sep(), turnLabel, sep(), new Label("Zoom:"), zin, zout, zreset,
                sep(), flip, sep(), statusLabel);
        bar.setAlignment(Pos.CENTER_LEFT);
        bar.setPadding(new Insets(8));
        bar.setStyle("-fx-background-color: #1f1f1f;");

        flippedToggle = flipped;
        BorderPane root = new BorderPane();
        root.setTop(bar);
        root.setCenter(boardView.getNode());
        root.setStyle("-fx-background-color: #2b2b2b;");
        stage.setScene(new Scene(root));
        refresh();
    }

    private boolean flippedToggle;

    private boolean isFlipped() {
        return flippedToggle;
    }

    private Label sep() {
        Label l = new Label("|");
        l.setStyle("-fx-text-fill: #555;");
        return l;
    }

    // ---- interaction --------------------------------------------------------

    private void onSquareClicked(int bx, int by) {
        if (board == null || gameOver || aiThinking) return;
        if (!isHumanTurn()) return;

        boolean side = board.isWhiteToMove();
        Piece clicked = board.pieceAt(bx, by);

        if (selX >= 0) {
            Move chosen = findMove(bx, by);
            if (chosen != null) {
                performHumanMove(chosen);
                return;
            }
            if (clicked != null && clicked.white == side) {
                select(bx, by, side);
            } else {
                clearSelection();
            }
        } else if (clicked != null && clicked.white == side) {
            select(bx, by, side);
        }
        refresh();
    }

    private void select(int bx, int by, boolean side) {
        selX = bx;
        selY = by;
        selectedMoves = new ArrayList<>();
        for (Move m : board.legalMoves(side)) {
            if (m.fromX == bx && m.fromY == by) selectedMoves.add(m);
        }
    }

    private void clearSelection() {
        selX = selY = -1;
        selectedMoves = new ArrayList<>();
    }

    private Move findMove(int bx, int by) {
        List<Move> targeting = new ArrayList<>();
        for (Move m : selectedMoves) {
            if (m.toX == bx && m.toY == by) targeting.add(m);
        }
        if (targeting.isEmpty()) return null;
        if (targeting.size() == 1) return targeting.get(0);
        // multiple => promotion choice
        return askPromotion(targeting);
    }

    private Move askPromotion(List<Move> options) {
        List<String> names = new ArrayList<>();
        for (Move m : options) names.add(m.promotion != null ? m.promotion.prettyName() : "Move");
        ChoiceDialog<String> dlg = new ChoiceDialog<>(names.get(0), names);
        dlg.setHeaderText("Promote pawn to:");
        dlg.setContentText("Piece:");
        Optional<String> r = dlg.showAndWait();
        if (r.isEmpty()) return null;
        int idx = names.indexOf(r.get());
        return options.get(Math.max(0, idx));
    }

    private void performHumanMove(Move m) {
        applyMove(m);
        if (mode == Mode.ONLINE && net != null) net.sendMove(m);
        clearSelection();
        refresh();
        if (checkGameEnd()) return;
        if (mode == Mode.VS_AI) maybeTriggerAi();
    }

    private void applyMove(Move m) {
        board.makeMove(m);
        lastMove = m;
    }

    private boolean isHumanTurn() {
        switch (mode) {
            case LOCAL_PVP: return true;
            case VS_AI: return board.isWhiteToMove() == humanIsWhite;
            case ONLINE: return net != null && net.isConnected()
                    && board.isWhiteToMove() == localPlaysWhite;
            default: return false; // AI_VS_AI has no human
        }
    }

    private boolean isAiTurn() {
        switch (mode) {
            case VS_AI: return board.isWhiteToMove() != humanIsWhite;
            case AI_VS_AI: return true;
            default: return false;
        }
    }

    private Ai aiForSideToMove() {
        if (mode == Mode.AI_VS_AI) return board.isWhiteToMove() ? aiWhite : aiBlack;
        return ai;
    }

    // ---- AI -----------------------------------------------------------------

    private void maybeTriggerAi() {
        if (board == null || gameOver || aiThinking || !isAiTurn()) return;
        aiThinking = true;
        String side = board.isWhiteToMove() ? "White" : "Black";
        setStatus(mode == Mode.AI_VS_AI ? (side + " computer is thinking…") : "Computer is thinking…");
        final int session = gameSession;
        final Board snapshot = board.copy();
        final Ai mover = aiForSideToMove();
        Task<Move> task = new Task<>() {
            @Override protected Move call() {
                return mover.chooseMove(snapshot);
            }
        };
        task.setOnSucceeded(e -> {
            aiThinking = false;
            if (session != gameSession) return; // new game or returned to menu
            Move m = task.getValue();
            setStatus("");
            if (m != null) {
                applyMove(m);
                refresh();
                if (checkGameEnd()) return;
            }
            if (isAiTurn()) scheduleNextAi(session);
        });
        task.setOnFailed(e -> {
            aiThinking = false;
            setStatus("AI error.");
        });
        Thread t = new Thread(task, "ai-search");
        t.setDaemon(true);
        t.start();
    }

    /** In AI-vs-AI, pause briefly between moves so the game is watchable. */
    private void scheduleNextAi(int session) {
        if (mode != Mode.AI_VS_AI) { maybeTriggerAi(); return; }
        PauseTransition pause = new PauseTransition(Duration.millis(450));
        pause.setOnFinished(e -> { if (session == gameSession && !gameOver) maybeTriggerAi(); });
        pause.play();
    }

    // ---- online -------------------------------------------------------------

    private NetworkManager.Listener netListener() {
        return new NetworkManager.Listener() {
            @Override public void onConnected(boolean localWhite, String fen) {
                Platform.runLater(() -> {
                    localPlaysWhite = localWhite;
                    if (fen != null && !fen.isBlank()) board.setFen(fen);
                    flippedToggle = !localWhite;
                    boardView.setFlipped(!localWhite);
                    setStatus("Connected. You play " + (localWhite ? "White" : "Black") + ".");
                    refresh();
                });
            }
            @Override public void onMove(Move move) {
                Platform.runLater(() -> {
                    applyMove(move);
                    clearSelection();
                    refresh();
                    checkGameEnd();
                });
            }
            @Override public void onDisconnected(String reason) {
                Platform.runLater(() -> { setStatus(reason); alert(reason); });
            }
            @Override public void onStatus(String status) {
                Platform.runLater(() -> setStatus(status));
            }
        };
    }

    // ---- rendering / status -------------------------------------------------

    private void refresh() {
        if (boardView == null || board == null) return;
        int[] check = null;
        if (board.isKingInCheck(board.isWhiteToMove())) {
            check = board.kingSquare(board.isWhiteToMove());
        }
        boardView.update(board, selX, selY, selectedMoves, lastMove, check);
        if (turnLabel != null && !gameOver) {
            String side = board.isWhiteToMove() ? "White" : "Black";
            turnLabel.setText(side + " to move" + (isHumanTurn() ? "  (you)" : ""));
        }
    }

    private boolean checkGameEnd() {
        Board.Result r = board.result();
        if (r == Board.Result.ONGOING) return false;
        gameOver = true;
        String msg;
        if (r == Board.Result.CHECKMATE) {
            String winner = board.isWhiteToMove() ? "Black" : "White";
            msg = "Checkmate – " + winner + " wins!";
        } else {
            msg = "Stalemate – draw.";
        }
        if (turnLabel != null) turnLabel.setText(msg);
        setStatus(msg);
        alert(msg);
        return true;
    }

    private void setStatus(String s) {
        if (statusLabel != null) statusLabel.setText(s);
    }

    private void alert(String message) {
        Alert a = new Alert(Alert.AlertType.INFORMATION, message);
        a.setHeaderText(null);
        a.setTitle("Chess-Ultima");
        a.showAndWait();
    }

    public static void main(String[] args) {
        launch(args);
    }
}
