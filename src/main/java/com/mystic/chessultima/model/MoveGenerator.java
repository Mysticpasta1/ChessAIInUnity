package com.mystic.chessultima.model;

import java.util.ArrayList;
import java.util.List;

/**
 * Pseudo-legal move generation and square-attack detection for every Chess-Ultima
 * piece. Rules are the corrected/clean variant of the original Unity project:
 * standard behaviour for classic and well-known fairy pieces.
 */
public final class MoveGenerator {

    private MoveGenerator() {}

    static final int[][] ORTHO = {{0, 1}, {0, -1}, {1, 0}, {-1, 0}};
    static final int[][] DIAG = {{1, 1}, {1, -1}, {-1, 1}, {-1, -1}};
    static final int[][] KING8 = {{0, 1}, {0, -1}, {1, 0}, {-1, 0}, {1, 1}, {1, -1}, {-1, 1}, {-1, -1}};
    static final int[][] KNIGHT = {{1, 2}, {2, 1}, {-1, 2}, {-2, 1}, {1, -2}, {2, -1}, {-1, -2}, {-2, -1}};
    static final int[][] WIZARD = {{1, 5}, {5, 1}, {-1, 5}, {-5, 1}, {1, -5}, {5, -1}, {-1, -5}, {-5, -1}};
    static final int[][] CHAMPION = {{0, 1}, {0, 2}, {2, 0}, {1, 0}, {0, -1}, {0, -2}, {-1, 0}, {-2, 0},
            {-2, 2}, {-2, -2}, {2, 2}, {2, -2}};
    static final int[][] ELE2 = {{2, 2}, {2, -2}, {-2, 2}, {-2, -2}};
    static final PieceType[] PROMOTIONS = {PieceType.QUEEN, PieceType.ROOK, PieceType.BISHOP, PieceType.KNIGHT};

    // ---- pseudo-legal generation -------------------------------------------

    public static List<Move> pseudoLegalMoves(Board b, boolean white) {
        List<Move> moves = new ArrayList<>();
        for (int y = 0; y < Board.HEIGHT; y++) {
            for (int x = 0; x < Board.WIDTH; x++) {
                Piece p = b.pieceAt(x, y);
                if (p != null && p.white == white) {
                    generate(b, x, y, p, moves);
                }
            }
        }
        return moves;
    }

    private static void generate(Board b, int x, int y, Piece p, List<Move> moves) {
        boolean w = p.white;
        int fwd = w ? 1 : -1;
        switch (p.type) {
            case PAWN -> pawnMoves(b, x, y, w, moves);
            case ROOK -> slides(b, x, y, w, ORTHO, moves);
            case KNIGHT -> steps(b, x, y, w, KNIGHT, moves);
            case BISHOP -> slides(b, x, y, w, DIAG, moves);
            case KING -> { steps(b, x, y, w, KING8, moves); castleMoves(b, x, y, p, moves); }
            case QUEEN -> { slides(b, x, y, w, ORTHO, moves); slides(b, x, y, w, DIAG, moves); }
            case AMAZON -> { slides(b, x, y, w, ORTHO, moves); slides(b, x, y, w, DIAG, moves);
                             steps(b, x, y, w, KNIGHT, moves); }
            case WIZARD -> { steps(b, x, y, w, KNIGHT, moves); steps(b, x, y, w, WIZARD, moves); }
            case CHAMPION -> steps(b, x, y, w, CHAMPION, moves);
            case ARCHBISHOP -> { slides(b, x, y, w, DIAG, moves); steps(b, x, y, w, KNIGHT, moves); }
            case CHANCELLOR -> { slides(b, x, y, w, ORTHO, moves); steps(b, x, y, w, KNIGHT, moves); }
            case FALCON -> slides(b, x, y, w, new int[][]{{1, fwd}, {-1, fwd}, {0, -fwd}}, moves);
            case HUNTER -> slides(b, x, y, w, new int[][]{{0, fwd}, {1, -fwd}, {-1, -fwd}}, moves);
            case DRAGON_HORSE -> { slides(b, x, y, w, DIAG, moves); steps(b, x, y, w, ORTHO, moves); }
            case DRAGON_KING -> { slides(b, x, y, w, ORTHO, moves); steps(b, x, y, w, DIAG, moves); }
            case GOLD_GENERAL -> steps(b, x, y, w, new int[][]{{0, 1}, {0, -1}, {1, 0}, {-1, 0},
                             {1, fwd}, {-1, fwd}}, moves);
            case SILVER_GENERAL -> steps(b, x, y, w, new int[][]{{1, 1}, {1, -1}, {-1, 1}, {-1, -1},
                             {0, fwd}}, moves);
            case LANCE -> slides(b, x, y, w, new int[][]{{0, fwd}}, moves);
            case ULTIMA_PAWN -> steps(b, x, y, w, ORTHO, moves);
            case LONG_LEAPER -> { slides(b, x, y, w, KING8, moves); leaperScreenCaptures(b, x, y, w, moves); }
            case CANNON -> cannonMoves(b, x, y, w, moves);
            case GUARD -> steps(b, x, y, w, KING8, moves);
            case MAO -> maoMoves(b, x, y, w, moves);
            case ELEPHANT -> elephantMoves(b, x, y, w, moves);
            default -> {}
        }
    }

    private static void slides(Board b, int x, int y, boolean white, int[][] dirs, List<Move> moves) {
        for (int[] d : dirs) {
            int nx = x + d[0], ny = y + d[1];
            while (Board.inBounds(nx, ny)) {
                Piece p = b.pieceAt(nx, ny);
                if (p == null) {
                    moves.add(new Move(x, y, nx, ny));
                } else {
                    if (p.white != white) moves.add(new Move(x, y, nx, ny));
                    break;
                }
                nx += d[0];
                ny += d[1];
            }
        }
    }

    private static void steps(Board b, int x, int y, boolean white, int[][] offsets, List<Move> moves) {
        for (int[] o : offsets) {
            int nx = x + o[0], ny = y + o[1];
            if (!Board.inBounds(nx, ny)) continue;
            Piece p = b.pieceAt(nx, ny);
            if (p == null || p.white != white) moves.add(new Move(x, y, nx, ny));
        }
    }

    private static void pawnMoves(Board b, int x, int y, boolean white, List<Move> moves) {
        int fwd = white ? 1 : -1;
        int promoRank = white ? Board.HEIGHT - 1 : 0;
        // forward push
        if (Board.inBounds(x, y + fwd) && b.isEmpty(x, y + fwd)) {
            addPawnMove(moves, x, y, x, y + fwd, promoRank);
            Piece self = b.pieceAt(x, y);
            if (self != null && self.moveCount == 0 && Board.inBounds(x, y + 2 * fwd)
                    && b.isEmpty(x, y + 2 * fwd)) {
                addPawnMove(moves, x, y, x, y + 2 * fwd, promoRank);
            }
        }
        // captures
        for (int dx : new int[]{-1, 1}) {
            int nx = x + dx, ny = y + fwd;
            if (!Board.inBounds(nx, ny)) continue;
            Piece p = b.pieceAt(nx, ny);
            if (p != null && p.white != white) addPawnMove(moves, x, y, nx, ny, promoRank);
        }
    }

    private static void addPawnMove(List<Move> moves, int fx, int fy, int tx, int ty, int promoRank) {
        if (ty == promoRank) {
            for (PieceType t : PROMOTIONS) moves.add(new Move(fx, fy, tx, ty, t, false));
        } else {
            moves.add(new Move(fx, fy, tx, ty));
        }
    }

    /**
     * The long leaper's special capture: leap over exactly one screening piece (of
     * either colour) along any of the eight directions and capture the first enemy
     * piece beyond it, landing on that piece. Its plain slides are handled separately.
     */
    private static void leaperScreenCaptures(Board b, int x, int y, boolean white, List<Move> moves) {
        for (int[] d : KING8) {
            int nx = x + d[0], ny = y + d[1];
            while (Board.inBounds(nx, ny) && b.isEmpty(nx, ny)) { nx += d[0]; ny += d[1]; }
            if (!Board.inBounds(nx, ny)) continue;             // no screen in this direction
            int mx = nx + d[0], my = ny + d[1];
            while (Board.inBounds(mx, my) && b.isEmpty(mx, my)) { mx += d[0]; my += d[1]; }
            if (Board.inBounds(mx, my)) {
                Piece target = b.pieceAt(mx, my);
                if (target != null && target.white != white) moves.add(new Move(x, y, mx, my));
            }
        }
    }

    private static void cannonMoves(Board b, int x, int y, boolean white, List<Move> moves) {
        for (int[] d : ORTHO) {
            int nx = x + d[0], ny = y + d[1];
            // non-capturing slide over empties
            while (Board.inBounds(nx, ny) && b.isEmpty(nx, ny)) {
                moves.add(new Move(x, y, nx, ny));
                nx += d[0];
                ny += d[1];
            }
            if (!Board.inBounds(nx, ny)) continue; // ran off board, no screen
            // (nx,ny) is the screen; look for the first piece beyond it
            int mx = nx + d[0], my = ny + d[1];
            while (Board.inBounds(mx, my) && b.isEmpty(mx, my)) {
                mx += d[0];
                my += d[1];
            }
            if (Board.inBounds(mx, my)) {
                Piece behind = b.pieceAt(mx, my);
                if (behind != null && behind.white != white) moves.add(new Move(x, y, mx, my));
            }
        }
    }

    private static int[] legOffset(int dx, int dy) {
        return new int[]{Math.abs(dx) == 2 ? Integer.signum(dx) : 0,
                         Math.abs(dy) == 2 ? Integer.signum(dy) : 0};
    }

    private static void maoMoves(Board b, int x, int y, boolean white, List<Move> moves) {
        for (int[] o : KNIGHT) {
            int[] leg = legOffset(o[0], o[1]);
            int lx = x + leg[0], ly = y + leg[1];
            if (!Board.inBounds(lx, ly) || !b.isEmpty(lx, ly)) continue; // blocked leg
            int nx = x + o[0], ny = y + o[1];
            if (!Board.inBounds(nx, ny)) continue;
            Piece p = b.pieceAt(nx, ny);
            if (p == null || p.white != white) moves.add(new Move(x, y, nx, ny));
        }
    }

    private static void elephantMoves(Board b, int x, int y, boolean white, List<Move> moves) {
        for (int[] o : ELE2) {
            int mx = x + o[0] / 2, my = y + o[1] / 2;
            if (!Board.inBounds(mx, my) || !b.isEmpty(mx, my)) continue; // blocked eye
            int nx = x + o[0], ny = y + o[1];
            if (!Board.inBounds(nx, ny)) continue;
            Piece p = b.pieceAt(nx, ny);
            if (p == null || p.white != white) moves.add(new Move(x, y, nx, ny));
        }
    }

    private static void castleMoves(Board b, int x, int y, Piece king, List<Move> moves) {
        if (king.moveCount != 0) return;
        if (b.isSquareAttacked(x, y, !king.white)) return; // cannot castle out of check
        for (int dx : new int[]{-1, 1}) {
            int[] rook = b.findCastlingRook(x, y, dx, king.white);
            if (rook == null) continue;
            int crossX = x + dx, toX = x + 2 * dx;
            if (!Board.inBounds(toX, y)) continue;
            if (!b.isEmpty(crossX, y) || !b.isEmpty(toX, y)) continue; // rook must be >=3 away
            if (b.isSquareAttacked(crossX, y, !king.white)) continue;
            if (b.isSquareAttacked(toX, y, !king.white)) continue;
            moves.add(new Move(x, y, toX, y, null, true));
        }
    }

    // ---- attack detection ---------------------------------------------------

    /** True if a piece of colour {@code byWhite} attacks square (tx, ty). */
    public static boolean isSquareAttacked(Board b, int tx, int ty, boolean byWhite) {
        int af = byWhite ? 1 : -1;

        // 1. Pawn diagonal attacks
        for (int dx : new int[]{-1, 1}) {
            Piece p = b.pieceAt(tx + dx, ty - af);
            if (p != null && p.white == byWhite && p.type == PieceType.PAWN) return true;
        }

        // 2. Knight-pattern leapers
        for (int[] o : KNIGHT) {
            Piece p = b.pieceAt(tx + o[0], ty + o[1]);
            if (p != null && p.white == byWhite && hasKnightAttack(p.type)) return true;
        }
        // 3. Wizard camel leaps
        for (int[] o : WIZARD) {
            Piece p = b.pieceAt(tx + o[0], ty + o[1]);
            if (p != null && p.white == byWhite && p.type == PieceType.WIZARD) return true;
        }
        // 4. Champion leaps
        for (int[] o : CHAMPION) {
            Piece p = b.pieceAt(tx + o[0], ty + o[1]);
            if (p != null && p.white == byWhite && p.type == PieceType.CHAMPION) return true;
        }

        // 5. Orthogonal rays
        for (int[] d : ORTHO) {
            int nx = tx + d[0], ny = ty + d[1];
            while (Board.inBounds(nx, ny) && b.isEmpty(nx, ny)) { nx += d[0]; ny += d[1]; }
            if (!Board.inBounds(nx, ny)) continue;
            Piece p = b.pieceAt(nx, ny);
            if (p.white != byWhite) continue;
            if (isFullOrthoSlider(p.type)) return true;
            // directional orthogonal sliders (vertical rays only)
            if (d[0] == 0) {
                if ((p.type == PieceType.LANCE || p.type == PieceType.HUNTER) && d[1] == -af) return true;
                if (p.type == PieceType.FALCON && d[1] == af) return true;
            }
        }
        // 6. Diagonal rays
        for (int[] d : DIAG) {
            int nx = tx + d[0], ny = ty + d[1];
            while (Board.inBounds(nx, ny) && b.isEmpty(nx, ny)) { nx += d[0]; ny += d[1]; }
            if (!Board.inBounds(nx, ny)) continue;
            Piece p = b.pieceAt(nx, ny);
            if (p.white != byWhite) continue;
            if (isFullDiagSlider(p.type)) return true;
            if (p.type == PieceType.FALCON && d[1] == -af) return true;
            if (p.type == PieceType.HUNTER && d[1] == af) return true;
        }

        // 8. Cannon (needs exactly one screen)
        for (int[] d : ORTHO) {
            int nx = tx + d[0], ny = ty + d[1];
            while (Board.inBounds(nx, ny) && b.isEmpty(nx, ny)) { nx += d[0]; ny += d[1]; }
            if (!Board.inBounds(nx, ny)) continue; // no screen
            int mx = nx + d[0], my = ny + d[1];
            while (Board.inBounds(mx, my) && b.isEmpty(mx, my)) { mx += d[0]; my += d[1]; }
            if (Board.inBounds(mx, my)) {
                Piece p = b.pieceAt(mx, my);
                if (p != null && p.white == byWhite && p.type == PieceType.CANNON) return true;
            }
        }

        // 8b. Long leaper capturing past exactly one screen (any of eight directions)
        for (int[] d : KING8) {
            int nx = tx + d[0], ny = ty + d[1];
            while (Board.inBounds(nx, ny) && b.isEmpty(nx, ny)) { nx += d[0]; ny += d[1]; }
            if (!Board.inBounds(nx, ny)) continue; // no screen
            int mx = nx + d[0], my = ny + d[1];
            while (Board.inBounds(mx, my) && b.isEmpty(mx, my)) { mx += d[0]; my += d[1]; }
            if (Board.inBounds(mx, my)) {
                Piece p = b.pieceAt(mx, my);
                if (p != null && p.white == byWhite && p.type == PieceType.LONG_LEAPER) return true;
            }
        }

        // 9. Adjacent single-steppers
        for (int[] d : KING8) {
            int nx = tx + d[0], ny = ty + d[1];
            Piece p = b.pieceAt(nx, ny);
            if (p == null || p.white != byWhite) continue;
            int vx = tx - nx, vy = ty - ny; // vector from that piece toward target
            int paf = p.white ? 1 : -1;
            switch (p.type) {
                case KING, GUARD -> { return true; }
                case ULTIMA_PAWN, DRAGON_HORSE -> { if (Math.abs(vx) + Math.abs(vy) == 1) return true; }
                case DRAGON_KING -> { if (Math.abs(vx) == 1 && Math.abs(vy) == 1) return true; }
                case GOLD_GENERAL -> {
                    if (Math.abs(vx) + Math.abs(vy) == 1) return true;            // orthogonal
                    if (Math.abs(vx) == 1 && vy == paf) return true;              // forward diagonal
                }
                case SILVER_GENERAL -> {
                    if (Math.abs(vx) == 1 && Math.abs(vy) == 1) return true;      // diagonal
                    if (vx == 0 && vy == paf) return true;                        // forward
                }
                default -> {}
            }
        }

        // 10. Mao (blockable knight)
        for (int[] o : KNIGHT) {
            int sx = tx - o[0], sy = ty - o[1];
            Piece p = b.pieceAt(sx, sy);
            if (p != null && p.white == byWhite && p.type == PieceType.MAO) {
                int[] leg = legOffset(o[0], o[1]);
                if (Board.inBounds(sx + leg[0], sy + leg[1]) && b.isEmpty(sx + leg[0], sy + leg[1])) {
                    return true;
                }
            }
        }
        // 11. Elephant (blockable 2-diagonal)
        for (int[] o : ELE2) {
            int sx = tx - o[0], sy = ty - o[1];
            Piece p = b.pieceAt(sx, sy);
            if (p != null && p.white == byWhite && p.type == PieceType.ELEPHANT) {
                int ex = sx + o[0] / 2, ey = sy + o[1] / 2;
                if (Board.inBounds(ex, ey) && b.isEmpty(ex, ey)) return true;
            }
        }
        return false;
    }

    private static boolean hasKnightAttack(PieceType t) {
        return t == PieceType.KNIGHT || t == PieceType.AMAZON || t == PieceType.WIZARD
                || t == PieceType.ARCHBISHOP || t == PieceType.CHANCELLOR;
    }

    private static boolean isFullOrthoSlider(PieceType t) {
        return t == PieceType.ROOK || t == PieceType.QUEEN || t == PieceType.AMAZON
                || t == PieceType.CHANCELLOR || t == PieceType.DRAGON_KING
                || t == PieceType.LONG_LEAPER;
    }

    private static boolean isFullDiagSlider(PieceType t) {
        return t == PieceType.BISHOP || t == PieceType.QUEEN || t == PieceType.AMAZON
                || t == PieceType.ARCHBISHOP || t == PieceType.DRAGON_HORSE
                || t == PieceType.LONG_LEAPER;
    }
}
