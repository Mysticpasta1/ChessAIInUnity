package com.mystic.chessultima.model;

/**
 * The full Chess-Ultima army. Ids, FEN letters and base material values are kept
 * identical to the original Unity project so existing FEN strings keep working.
 * Movement, however, is the "clean" corrected rule set (see MoveGenerator).
 */
public enum PieceType {
    NONE(0, ' ', "none", 0),
    PAWN(1, 'p', "pawn", 100),
    ROOK(2, 'r', "rook", 500),
    KNIGHT(3, 'n', "knight", 320),
    BISHOP(4, 'b', "bishop", 330),
    KING(5, 'k', "king", 1_000_000),
    QUEEN(6, 'q', "queen", 900),
    AMAZON(7, 'a', "amazon", 1200),
    WIZARD(8, 'z', "wizard", 300),
    CHAMPION(9, 'e', "champion", 300),
    ARCHBISHOP(10, 'c', "archbishop", 650),
    CHANCELLOR(11, 'f', "chancellor", 700),
    FALCON(12, 'l', "falcon", 450),
    HUNTER(13, 's', "hunter", 450),
    DRAGON_HORSE(14, 'h', "dragon_horse", 550),
    DRAGON_KING(15, 'i', "dragon_king", 600),
    GOLD_GENERAL(16, 'm', "gold_general", 280),
    SILVER_GENERAL(17, 't', "silver_general", 260),
    LANCE(18, 'u', "lance", 250),
    ULTIMA_PAWN(19, 'x', "ultima_pawn", 180),
    LONG_LEAPER(20, 'v', "long_leaper", 550),
    CANNON(21, 'd', "cannon", 450),
    GUARD(22, 'o', "guard", 260),
    MAO(23, 'w', "mao", 300),
    ELEPHANT(24, 'j', "elephant", 200);

    public final int id;
    /** Lower-case FEN letter; upper-case is used for the white side. */
    public final char fen;
    /** Base name; also the sprite base name (white_<sprite>.png / black_<sprite>.png). */
    public final String sprite;
    /** Base material value used by the AI evaluation. */
    public final int value;

    PieceType(int id, char fen, String sprite, int value) {
        this.id = id;
        this.fen = fen;
        this.sprite = sprite;
        this.value = value;
    }

    private static final PieceType[] BY_ID = new PieceType[25];
    private static final PieceType[] BY_FEN = new PieceType[128];

    static {
        for (PieceType t : values()) {
            BY_ID[t.id] = t;
            BY_FEN[t.fen] = t;
        }
    }

    public static PieceType byId(int id) {
        if (id < 0 || id >= BY_ID.length) return NONE;
        PieceType t = BY_ID[id];
        return t == null ? NONE : t;
    }

    public static PieceType byFen(char c) {
        char lower = Character.toLowerCase(c);
        if (lower >= BY_FEN.length) return NONE;
        PieceType t = BY_FEN[lower];
        return t == null ? NONE : t;
    }

    public String prettyName() {
        String s = sprite.replace('_', ' ');
        return Character.toUpperCase(s.charAt(0)) + s.substring(1);
    }
}
