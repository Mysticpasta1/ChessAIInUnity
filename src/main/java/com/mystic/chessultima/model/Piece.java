package com.mystic.chessultima.model;

/** A piece occupying a square. Mutable; the board owns instances. */
public final class Piece {
    public PieceType type;
    public boolean white;
    public int moveCount;

    public Piece(PieceType type, boolean white) {
        this.type = type;
        this.white = white;
        this.moveCount = 0;
    }

    public Piece copy() {
        Piece p = new Piece(type, white);
        p.moveCount = moveCount;
        return p;
    }

    /** FEN letter: upper-case for white, lower-case for black. */
    public char fenChar() {
        return white ? Character.toUpperCase(type.fen) : type.fen;
    }

    public boolean hasMoved() {
        return moveCount > 0;
    }
}
