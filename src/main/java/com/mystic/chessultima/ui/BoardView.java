package com.mystic.chessultima.ui;

import com.mystic.chessultima.model.Board;
import com.mystic.chessultima.model.Move;
import com.mystic.chessultima.model.Piece;
import javafx.beans.binding.Bindings;
import javafx.scene.Group;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.control.ScrollPane;
import javafx.scene.image.Image;
import javafx.scene.layout.StackPane;
import javafx.scene.paint.Color;
import javafx.scene.text.Font;
import javafx.scene.text.TextAlignment;
import javafx.scene.transform.Scale;

import java.util.List;
import java.util.function.BiConsumer;

/**
 * Renders the board on a canvas that is scaled to fit its container and kept
 * centred. Scrolling the wheel zooms; when zoomed in past the fit size the board
 * becomes larger than the viewport and can be panned (drag) or scrolled.
 */
public final class BoardView {

    private static final double TILE = 40;
    private static final double BOARD_W = Board.WIDTH * TILE;
    private static final double BOARD_H = Board.HEIGHT * TILE;
    private static final Color LIGHT = Color.web("#e9d7b8");
    private static final Color DARK = Color.web("#a9805a");
    private static final Color SELECT = Color.web("#f6f36b", 0.65);
    private static final Color MOVE_DOT = Color.web("#2f8f4e", 0.75);
    private static final Color LAST = Color.web("#6fb7ff", 0.45);
    private static final Color CHECK = Color.web("#e2483a", 0.65);

    private final Canvas canvas = new Canvas(BOARD_W, BOARD_H);
    private final Scale scale = new Scale(1, 1);
    private final Group group = new Group(canvas);
    private final StackPane wrapper = new StackPane(group);
    private final ScrollPane scroll = new ScrollPane(wrapper);
    private final PieceImages images;

    private Board board;
    private int selX = -1, selY = -1;
    private List<Move> highlights = List.of();
    private Move lastMove;
    private boolean flipped;
    private int[] checkSquare;
    private BiConsumer<Integer, Integer> onSquareClicked;

    private double userZoom = 1.0; // multiplier on top of the fit-to-viewport scale

    public BoardView(PieceImages images) {
        this.images = images;
        // Scale the canvas (not the Group) so the Group's layout bounds reflect the
        // scaled size. The StackPane then centres it correctly and the ScrollPane's
        // pannable area covers the whole board when zoomed in.
        canvas.getTransforms().add(scale);

        wrapper.setStyle("-fx-background-color: #2b2b2b;");
        // Keep the centring area at least the size of the viewport so the board is
        // centred when it fits, and scrollable/pannable when it is larger.
        wrapper.minWidthProperty().bind(Bindings.createDoubleBinding(
                () -> scroll.getViewportBounds().getWidth(), scroll.viewportBoundsProperty()));
        wrapper.minHeightProperty().bind(Bindings.createDoubleBinding(
                () -> scroll.getViewportBounds().getHeight(), scroll.viewportBoundsProperty()));

        scroll.setPannable(true);
        scroll.setHbarPolicy(ScrollPane.ScrollBarPolicy.AS_NEEDED);
        scroll.setVbarPolicy(ScrollPane.ScrollBarPolicy.AS_NEEDED);
        scroll.setStyle("-fx-background: #2b2b2b; -fx-background-color: #2b2b2b;");
        scroll.viewportBoundsProperty().addListener((o, a, b) -> applyScale());

        canvas.setOnMouseClicked(e -> {
            if (e.isStillSincePress() && onSquareClicked != null) {
                int col = (int) Math.floor(e.getX() / TILE);
                int row = (int) Math.floor(e.getY() / TILE);
                if (col < 0 || col >= Board.WIDTH || row < 0 || row >= Board.HEIGHT) return;
                int bx = flipped ? (Board.WIDTH - 1 - col) : col;
                int by = flipped ? row : (Board.HEIGHT - 1 - row);
                onSquareClicked.accept(bx, by);
            }
        });

        scroll.addEventFilter(javafx.scene.input.ScrollEvent.SCROLL, e -> {
            double factor = e.getDeltaY() > 0 ? 1.1 : 1 / 1.1;
            userZoom = clamp(userZoom * factor, 1.0, 6.0);
            applyScale();
            e.consume();
        });
    }

    public ScrollPane getNode() {
        return scroll;
    }

    public void setOnSquareClicked(BiConsumer<Integer, Integer> cb) {
        this.onSquareClicked = cb;
    }

    public void setFlipped(boolean flipped) {
        this.flipped = flipped;
        draw();
    }

    public void zoomIn() { userZoom = clamp(userZoom * 1.15, 1.0, 6.0); applyScale(); }

    public void zoomOut() { userZoom = clamp(userZoom / 1.15, 1.0, 6.0); applyScale(); }

    public void resetZoom() { userZoom = 1.0; applyScale(); }

    /** Scale so the board fits the viewport, times the user's zoom multiplier. */
    private void applyScale() {
        double vw = scroll.getViewportBounds().getWidth();
        double vh = scroll.getViewportBounds().getHeight();
        if (vw <= 0 || vh <= 0) return;
        double fit = Math.min(vw / BOARD_W, vh / BOARD_H);
        double f = fit * userZoom;
        scale.setX(f);
        scale.setY(f);
    }

    public void update(Board board, int selX, int selY, List<Move> highlights, Move lastMove,
                       int[] checkSquare) {
        this.board = board;
        this.selX = selX;
        this.selY = selY;
        this.highlights = highlights == null ? List.of() : highlights;
        this.lastMove = lastMove;
        this.checkSquare = checkSquare;
        applyScale();
        draw();
    }

    private void draw() {
        GraphicsContext g = canvas.getGraphicsContext2D();
        g.clearRect(0, 0, canvas.getWidth(), canvas.getHeight());
        if (board == null) return;

        for (int y = 0; y < Board.HEIGHT; y++) {
            for (int x = 0; x < Board.WIDTH; x++) {
                double px = pixelX(x), py = pixelY(y);
                g.setFill(((x + y) % 2 == 0) ? DARK : LIGHT);
                g.fillRect(px, py, TILE, TILE);
            }
        }

        if (lastMove != null) {
            fillSquare(g, lastMove.fromX, lastMove.fromY, LAST);
            fillSquare(g, lastMove.toX, lastMove.toY, LAST);
        }
        if (selX >= 0) fillSquare(g, selX, selY, SELECT);
        if (checkSquare != null) fillSquare(g, checkSquare[0], checkSquare[1], CHECK);

        for (int y = 0; y < Board.HEIGHT; y++) {
            for (int x = 0; x < Board.WIDTH; x++) {
                Piece p = board.pieceAt(x, y);
                if (p == null) continue;
                drawPiece(g, x, y, p);
            }
        }

        g.setFill(MOVE_DOT);
        for (Move m : highlights) {
            double px = pixelX(m.toX), py = pixelY(m.toY);
            boolean capture = board.pieceAt(m.captureX, m.captureY) != null;
            if (capture) {
                g.setStroke(MOVE_DOT);
                g.setLineWidth(3.5);
                g.strokeOval(px + 3, py + 3, TILE - 6, TILE - 6);
            } else {
                g.fillOval(px + TILE * 0.34, py + TILE * 0.34, TILE * 0.32, TILE * 0.32);
            }
        }
    }

    private void drawPiece(GraphicsContext g, int x, int y, Piece p) {
        double px = pixelX(x), py = pixelY(y);
        Image img = images.get(p.type, p.white);
        if (img != null) {
            double iw = img.getWidth(), ih = img.getHeight();
            double maxDim = TILE * 0.86;
            double s = Math.min(maxDim / iw, maxDim / ih);
            double w = iw * s, h = ih * s;
            g.drawImage(img, px + (TILE - w) / 2, py + (TILE - h) / 2, w, h);
        } else {
            g.setFill(p.white ? Color.WHITE : Color.BLACK);
            g.setStroke(p.white ? Color.BLACK : Color.WHITE);
            g.setFont(Font.font(TILE * 0.6));
            g.setTextAlign(TextAlignment.CENTER);
            g.setLineWidth(0.6);
            String ch = String.valueOf(p.fenChar());
            g.fillText(ch, px + TILE / 2, py + TILE * 0.72);
            g.strokeText(ch, px + TILE / 2, py + TILE * 0.72);
        }
    }

    private void fillSquare(GraphicsContext g, int x, int y, Color c) {
        g.setFill(c);
        g.fillRect(pixelX(x), pixelY(y), TILE, TILE);
    }

    private double pixelX(int boardX) {
        int col = flipped ? (Board.WIDTH - 1 - boardX) : boardX;
        return col * TILE;
    }

    private double pixelY(int boardY) {
        int row = flipped ? boardY : (Board.HEIGHT - 1 - boardY);
        return row * TILE;
    }

    private static double clamp(double v, double lo, double hi) {
        return Math.max(lo, Math.min(hi, v));
    }
}
