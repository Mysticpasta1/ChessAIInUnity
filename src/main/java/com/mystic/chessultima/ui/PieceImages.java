package com.mystic.chessultima.ui;

import com.mystic.chessultima.model.PieceType;
import javafx.scene.image.Image;

import java.io.InputStream;
import java.util.EnumMap;
import java.util.Map;

/** Loads and caches the piece sprites bundled under resources/.../pieces/. */
public final class PieceImages {

    private final Map<PieceType, Image> white = new EnumMap<>(PieceType.class);
    private final Map<PieceType, Image> black = new EnumMap<>(PieceType.class);

    public PieceImages() {
        for (PieceType t : PieceType.values()) {
            if (t == PieceType.NONE) continue;
            white.put(t, load("white_" + t.sprite));
            black.put(t, load("black_" + t.sprite));
        }
    }

    private Image load(String name) {
        String path = "/com/mystic/chessultima/pieces/" + name + ".png";
        try (InputStream in = getClass().getResourceAsStream(path)) {
            if (in == null) return null;
            return new Image(in);
        } catch (Exception e) {
            return null;
        }
    }

    /** May be null if the sprite is missing (the board then draws a letter). */
    public Image get(PieceType type, boolean isWhite) {
        return (isWhite ? white : black).get(type);
    }
}
