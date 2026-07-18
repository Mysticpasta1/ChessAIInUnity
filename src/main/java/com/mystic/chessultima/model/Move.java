package com.mystic.chessultima.model;

/** A move from one square to another, with optional promotion / castling metadata. */
public final class Move {
    public final int fromX, fromY;
    public final int toX, toY;
    /** Square whose occupant is captured. Equals the destination for normal moves,
     *  but differs for the long-leaper, which captures a piece it jumps over. */
    public final int captureX, captureY;
    /** Non-null when this move promotes a pawn. */
    public final PieceType promotion;
    /** True when this is a castling king move (rook is relocated automatically). */
    public final boolean castle;

    public Move(int fromX, int fromY, int toX, int toY) {
        this(fromX, fromY, toX, toY, null, false);
    }

    public Move(int fromX, int fromY, int toX, int toY, PieceType promotion, boolean castle) {
        this(fromX, fromY, toX, toY, toX, toY, promotion, castle);
    }

    public Move(int fromX, int fromY, int toX, int toY, int captureX, int captureY,
                PieceType promotion, boolean castle) {
        this.fromX = fromX;
        this.fromY = fromY;
        this.toX = toX;
        this.toY = toY;
        this.captureX = captureX;
        this.captureY = captureY;
        this.promotion = promotion;
        this.castle = castle;
    }

    /** A long-leaper move that lands on (toX,toY) while capturing the piece on (capX,capY). */
    public static Move leap(int fromX, int fromY, int toX, int toY, int capX, int capY) {
        return new Move(fromX, fromY, toX, toY, capX, capY, null, false);
    }

    public Move withPromotion(PieceType t) {
        return new Move(fromX, fromY, toX, toY, captureX, captureY, t, castle);
    }

    public boolean sameSquares(Move o) {
        return o != null && fromX == o.fromX && fromY == o.fromY && toX == o.toX && toY == o.toY;
    }

    public boolean targets(int x, int y) {
        return toX == x && toY == y;
    }

    /** Compact wire form: "fromX,fromY,toX,toY,promoId,castle". */
    public String encode() {
        return fromX + "," + fromY + "," + toX + "," + toY + ","
                + (promotion == null ? 0 : promotion.id) + "," + (castle ? 1 : 0);
    }

    public static Move decode(String s) {
        String[] p = s.trim().split(",");
        PieceType promo = Integer.parseInt(p[4]) == 0 ? null : PieceType.byId(Integer.parseInt(p[4]));
        boolean castle = p.length > 5 && "1".equals(p[5]);
        return new Move(Integer.parseInt(p[0]), Integer.parseInt(p[1]),
                Integer.parseInt(p[2]), Integer.parseInt(p[3]), promo, castle);
    }

    @Override
    public String toString() {
        return "(" + fromX + "," + fromY + ")->(" + toX + "," + toY + ")"
                + (promotion != null ? "=" + promotion.sprite : "");
    }
}
