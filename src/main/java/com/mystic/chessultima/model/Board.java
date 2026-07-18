package com.mystic.chessultima.model;

import java.util.ArrayDeque;
import java.util.Deque;
import java.util.List;

/**
 * A square 24x24 Chess-Ultima board. Each army is eight ranks deep — six ranks of
 * pieces backed against the edge, then two ranks of pawns — so the two armies fill
 * the board up to the top and bottom edges, separated by an eight-rank gap
 * (8 + 8 + 8 = 24). Cells are square. Square (x, y) is stored at index
 * y * WIDTH + x. White occupies the bottom edge (small y) and moves toward larger
 * y; black is the reverse. Empty squares are {@code null}.
 */
public final class Board {

    public static final int WIDTH = 24;
    public static final int HEIGHT = 24;

    // The six piece ranks of the white army, from the edge inward. Only the back
    // rank carries the king and queen; the four extra ranks are minor/medium
    // fairy pieces (mirror-symmetric left-to-right). Black mirrors this.
    private static final String[] PIECE_RANKS = {
            "RTXXXXCFNNHIKQNNFCXXXXMR", // back  (rooks, generals, king, queen, ...)
            "USJVWOBZAADEEDAAZBOWVJLU", // second
            "NBUXTMOWJSLVVLSJWOMTXUBN", // extra 1
            "HECFIDNBUXTMMTXUBNDIFCEH", // extra 2
            "OWJSLVNBUXTMMTXUBNVLSJWO", // extra 3
            "ECFHISLVNBUXXUBNVLSIHFCE", // extra 4
    };
    private static final String PAWNS = "PPPPPPPPPPPPPPPPPPPPPPPP";
    private static final int GAP = 8;
    private static final int PAWN_RANKS = 2;

    public static final String START_FEN = buildStartFen();

    private static String buildStartFen() {
        StringBuilder sb = new StringBuilder();
        // black army from the top edge inward: piece ranks, then pawn ranks
        for (String r : PIECE_RANKS) sb.append(r.toLowerCase()).append('/');
        for (int i = 0; i < PAWN_RANKS; i++) sb.append(PAWNS.toLowerCase()).append('/');
        // gap
        for (int i = 0; i < GAP; i++) sb.append("888/");
        // white army toward the bottom edge: pawn ranks, then piece ranks (reversed)
        for (int i = 0; i < PAWN_RANKS; i++) sb.append(PAWNS).append('/');
        for (int i = PIECE_RANKS.length - 1; i >= 0; i--) sb.append(PIECE_RANKS[i]).append('/');
        sb.setLength(sb.length() - 1); // drop trailing '/'
        sb.append(" w - - 0 1");
        return sb.toString();
    }

    private final Piece[] squares = new Piece[WIDTH * HEIGHT];
    private boolean whiteToMove = true;
    private int fiftyMove = 0;
    private int ply = 0;

    private final Deque<Undo> history = new ArrayDeque<>();

    public Board() {
        setFen(START_FEN);
    }

    public static Board fromFen(String fen) {
        Board b = new Board();
        b.setFen(fen);
        return b;
    }

    // ---- basic access -------------------------------------------------------

    public static int index(int x, int y) {
        return y * WIDTH + x;
    }

    public static boolean inBounds(int x, int y) {
        return x >= 0 && x < WIDTH && y >= 0 && y < HEIGHT;
    }

    public Piece pieceAt(int x, int y) {
        if (!inBounds(x, y)) return null;
        return squares[index(x, y)];
    }

    public boolean isEmpty(int x, int y) {
        return pieceAt(x, y) == null;
    }

    public boolean isWhiteToMove() {
        return whiteToMove;
    }

    public void setWhiteToMove(boolean w) {
        this.whiteToMove = w;
    }

    public int ply() {
        return ply;
    }

    public int[] kingSquare(boolean white) {
        for (int y = 0; y < HEIGHT; y++) {
            for (int x = 0; x < WIDTH; x++) {
                Piece p = squares[index(x, y)];
                if (p != null && p.type == PieceType.KING && p.white == white) {
                    return new int[]{x, y};
                }
            }
        }
        return null;
    }

    // ---- FEN ---------------------------------------------------------------

    public void setFen(String fen) {
        java.util.Arrays.fill(squares, null);
        history.clear();
        String[] sections = fen.trim().split("\\s+");
        String placement = sections[0];
        int x = 0;
        int y = HEIGHT - 1;
        for (int i = 0; i < placement.length(); i++) {
            char c = placement.charAt(i);
            if (c == '/') {
                x = 0;
                y--;
            } else if (Character.isDigit(c)) {
                x += (c - '0');
            } else {
                boolean white = Character.isUpperCase(c);
                PieceType type = PieceType.byFen(c);
                if (type != PieceType.NONE && inBounds(x, y)) {
                    squares[index(x, y)] = new Piece(type, white);
                }
                x++;
            }
        }
        whiteToMove = sections.length < 2 || sections[1].equalsIgnoreCase("w");
        fiftyMove = 0;
        ply = 0;
        if (sections.length > 4) {
            try { fiftyMove = Integer.parseInt(sections[4]); } catch (NumberFormatException ignored) {}
        }
    }

    public String toFen() {
        StringBuilder sb = new StringBuilder();
        for (int y = HEIGHT - 1; y >= 0; y--) {
            int empty = 0;
            for (int x = 0; x < WIDTH; x++) {
                Piece p = squares[index(x, y)];
                if (p == null) {
                    empty++;
                } else {
                    if (empty > 0) { appendEmpty(sb, empty); empty = 0; }
                    sb.append(p.fenChar());
                }
            }
            if (empty > 0) appendEmpty(sb, empty);
            if (y != 0) sb.append('/');
        }
        sb.append(' ').append(whiteToMove ? 'w' : 'b');
        sb.append(" - - ").append(fiftyMove).append(' ').append((ply / 2) + 1);
        return sb.toString();
    }

    private static void appendEmpty(StringBuilder sb, int n) {
        while (n >= 8) { sb.append('8'); n -= 8; }
        if (n > 0) sb.append((char) ('0' + n));
    }

    // ---- make / undo -------------------------------------------------------

    private static final class Undo {
        final Move move;
        final Piece moved;            // reference, still on board after move
        final int movedPrevCount;
        final Piece captured;
        final int capturedX, capturedY;
        final PieceType promotedFrom;
        final boolean prevWhiteToMove;
        final int prevFifty, prevPly;
        // castling rook relocation
        final Piece rook;
        final int rookFromX, rookFromY;
        final int rookPrevCount;

        Undo(Move move, Piece moved, int movedPrevCount, Piece captured, int capturedX, int capturedY,
             PieceType promotedFrom, boolean prevWhiteToMove, int prevFifty, int prevPly,
             Piece rook, int rookFromX, int rookFromY, int rookPrevCount) {
            this.move = move;
            this.moved = moved;
            this.movedPrevCount = movedPrevCount;
            this.captured = captured;
            this.capturedX = capturedX;
            this.capturedY = capturedY;
            this.promotedFrom = promotedFrom;
            this.prevWhiteToMove = prevWhiteToMove;
            this.prevFifty = prevFifty;
            this.prevPly = prevPly;
            this.rook = rook;
            this.rookFromX = rookFromX;
            this.rookFromY = rookFromY;
            this.rookPrevCount = rookPrevCount;
        }
    }

    /** Apply a (already legal or pseudo-legal) move, recording undo information. */
    public void makeMove(Move m) {
        Piece moved = squares[index(m.fromX, m.fromY)];
        int movedPrevCount = moved == null ? 0 : moved.moveCount;
        int capturedX = m.captureX, capturedY = m.captureY;
        Piece captured = squares[index(capturedX, capturedY)];
        PieceType promotedFrom = null;
        boolean prevWhite = whiteToMove;
        int prevFifty = fiftyMove, prevPly = ply;

        Piece rook = null;
        int rookFromX = -1, rookFromY = -1, rookPrevCount = 0;

        if (m.castle && moved != null) {
            int dx = Integer.signum(m.toX - m.fromX);
            int[] rs = findCastlingRook(m.fromX, m.fromY, dx, moved.white);
            if (rs != null) {
                rook = squares[index(rs[0], rs[1])];
                rookFromX = rs[0];
                rookFromY = rs[1];
                rookPrevCount = rook.moveCount;
            }
        }

        // move the piece
        squares[index(m.fromX, m.fromY)] = null;
        squares[index(capturedX, capturedY)] = null; // clears jumped piece for leaps
        if (moved != null) {
            if (m.promotion != null) {
                promotedFrom = moved.type;
                moved.type = m.promotion;
            }
            moved.moveCount++;
            squares[index(m.toX, m.toY)] = moved;
        }

        // relocate the rook for castling: it lands on the square the king crossed
        if (rook != null) {
            int dx = Integer.signum(m.toX - m.fromX);
            int rookToX = m.fromX + dx;
            squares[index(rookFromX, rookFromY)] = null;
            rook.moveCount++;
            squares[index(rookToX, m.fromY)] = rook;
        }

        if (captured != null || (moved != null && moved.type == PieceType.PAWN)) {
            fiftyMove = 0;
        } else {
            fiftyMove++;
        }
        ply++;
        whiteToMove = !whiteToMove;

        history.push(new Undo(m, moved, movedPrevCount, captured, capturedX, capturedY,
                promotedFrom, prevWhite, prevFifty, prevPly, rook, rookFromX, rookFromY, rookPrevCount));
    }

    public void undoMove() {
        Undo u = history.pop();
        Move m = u.move;

        // undo castling rook
        if (u.rook != null) {
            int dx = Integer.signum(m.toX - m.fromX);
            int rookToX = m.fromX + dx;
            squares[index(rookToX, m.fromY)] = null;
            u.rook.moveCount = u.rookPrevCount;
            squares[index(u.rookFromX, u.rookFromY)] = u.rook;
        }

        // move piece back
        squares[index(m.toX, m.toY)] = null;
        if (u.moved != null) {
            if (u.promotedFrom != null) {
                u.moved.type = u.promotedFrom;
            }
            u.moved.moveCount = u.movedPrevCount;
            squares[index(m.fromX, m.fromY)] = u.moved;
        }

        // restore capture
        if (u.captured != null) {
            squares[index(u.capturedX, u.capturedY)] = u.captured;
        }

        whiteToMove = u.prevWhiteToMove;
        fiftyMove = u.prevFifty;
        ply = u.prevPly;
    }

    /** Scan from the king outward along dx for the first unmoved friendly rook. */
    public int[] findCastlingRook(int kingX, int kingY, int dx, boolean white) {
        for (int x = kingX + dx; inBounds(x, kingY); x += dx) {
            Piece p = squares[index(x, kingY)];
            if (p != null) {
                if (p.type == PieceType.ROOK && p.white == white && p.moveCount == 0) {
                    return new int[]{x, kingY};
                }
                return null; // blocked by some other piece
            }
        }
        return null;
    }

    // ---- legality ----------------------------------------------------------

    /** Does any enemy piece of colour {@code byWhite} attack square (x, y)? */
    public boolean isSquareAttacked(int x, int y, boolean byWhite) {
        return MoveGenerator.isSquareAttacked(this, x, y, byWhite);
    }

    public boolean isKingInCheck(boolean white) {
        int[] k = kingSquare(white);
        if (k == null) return false;
        return isSquareAttacked(k[0], k[1], !white);
    }

    /** Fully legal moves for the given side (does not leave own king in check). */
    public List<Move> legalMoves(boolean white) {
        List<Move> pseudo = MoveGenerator.pseudoLegalMoves(this, white);
        List<Move> legal = new java.util.ArrayList<>(pseudo.size());
        for (Move m : pseudo) {
            makeMove(m);
            boolean ok = !isKingInCheck(white);
            undoMove();
            if (ok) legal.add(m);
        }
        return legal;
    }

    public boolean hasLegalMoves(boolean white) {
        return !legalMoves(white).isEmpty();
    }

    public enum Result { ONGOING, CHECKMATE, STALEMATE }

    public Result result() {
        if (hasLegalMoves(whiteToMove)) return Result.ONGOING;
        return isKingInCheck(whiteToMove) ? Result.CHECKMATE : Result.STALEMATE;
    }

    public Board copy() {
        Board b = new Board();
        java.util.Arrays.fill(b.squares, null);
        for (int i = 0; i < squares.length; i++) {
            if (squares[i] != null) b.squares[i] = squares[i].copy();
        }
        b.whiteToMove = whiteToMove;
        b.fiftyMove = fiftyMove;
        b.ply = ply;
        return b;
    }
}
