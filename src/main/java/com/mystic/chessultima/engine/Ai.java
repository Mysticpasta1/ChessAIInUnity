package com.mystic.chessultima.engine;

import com.mystic.chessultima.model.Board;
import com.mystic.chessultima.model.Move;
import com.mystic.chessultima.model.Piece;
import com.mystic.chessultima.model.PieceType;

import java.util.List;

/**
 * A minimax / negamax search with alpha-beta pruning, mirroring the intent of the
 * original Unity AI (material-driven with light positional shaping). Depth is kept
 * modest because the 24x24 board with a large army produces very wide move lists.
 */
public final class Ai {

    private static final int MATE = 10_000_000;
    private final int depth;
    private final long timeBudgetMillis;
    private long deadline;

    public Ai(int depth) {
        this(depth, 8000);
    }

    public Ai(int depth, long timeBudgetMillis) {
        this.depth = Math.max(1, depth);
        this.timeBudgetMillis = timeBudgetMillis;
    }

    /** Choose a move for the side to move, or null if there are none. */
    public Move chooseMove(Board board) {
        deadline = System.currentTimeMillis() + timeBudgetMillis;
        boolean white = board.isWhiteToMove();
        List<Move> rootMoves = board.legalMoves(white);
        if (rootMoves.isEmpty()) return null;
        orderMoves(board, rootMoves);
        int color = white ? 1 : -1;

        // Iterative deepening. The previous iteration's best move is always searched
        // first, so even if a depth is cut off by the time budget its best-so-far is
        // a fully-searched, reliable move worth keeping.
        Move best = rootMoves.get(0);
        for (int d = 1; d <= depth; d++) {
            int alpha = -MATE * 4, beta = MATE * 4;
            int bestScore = Integer.MIN_VALUE;
            Move bestThisDepth = null;
            boolean aborted = false;
            for (Move m : rootMoves) {
                board.makeMove(m);
                int score = -negamax(board, d - 1, -beta, -alpha, -color);
                board.undoMove();
                if (score > bestScore) {
                    bestScore = score;
                    bestThisDepth = m;
                }
                if (score > alpha) alpha = score;
                if (timeUp()) { aborted = true; break; }
            }
            if (bestThisDepth != null) {
                best = bestThisDepth;
                // Search the current best first at the next depth (better pruning).
                rootMoves.remove(best);
                rootMoves.add(0, best);
            }
            if (aborted || timeUp()) break;
        }
        return best;
    }

    private int negamax(Board board, int d, int alpha, int beta, int color) {
        if (d == 0 || timeUp()) {
            return color * evaluate(board);
        }
        boolean white = board.isWhiteToMove();
        List<Move> moves = board.legalMoves(white);
        if (moves.isEmpty()) {
            // checkmate (bad for side to move) or stalemate
            if (board.isKingInCheck(white)) return -MATE + (depth - d);
            return 0;
        }
        orderMoves(board, moves);
        int best = -MATE * 4;
        for (Move m : moves) {
            board.makeMove(m);
            int score = -negamax(board, d - 1, -beta, -alpha, -color);
            board.undoMove();
            if (score > best) best = score;
            if (best > alpha) alpha = best;
            if (alpha >= beta) break;
            if (timeUp()) break;
        }
        return best;
    }

    /** Static evaluation from white's perspective. */
    private int evaluate(Board board) {
        int score = 0;
        int cx = Board.WIDTH / 2, cy = Board.HEIGHT / 2;
        for (int y = 0; y < Board.HEIGHT; y++) {
            for (int x = 0; x < Board.WIDTH; x++) {
                Piece p = board.pieceAt(x, y);
                if (p == null) continue;
                int v = p.type.value;
                // light central shaping for non-royal, non-pawn pieces
                if (p.type != PieceType.KING && p.type != PieceType.PAWN) {
                    int dist = Math.abs(x - cx) + Math.abs(y - cy);
                    v += Math.max(0, 12 - dist);
                } else if (p.type == PieceType.PAWN) {
                    v += p.white ? y : (Board.HEIGHT - 1 - y); // reward advancement
                }
                score += p.white ? v : -v;
            }
        }
        return score;
    }

    private void orderMoves(Board board, List<Move> moves) {
        moves.sort((a, b) -> captureScore(board, b) - captureScore(board, a));
    }

    private int captureScore(Board board, Move m) {
        Piece victim = board.pieceAt(m.captureX, m.captureY);
        int s = victim == null ? 0 : victim.type.value + 1;
        if (m.promotion != null) s += m.promotion.value;
        return s;
    }

    private boolean timeUp() {
        return System.currentTimeMillis() > deadline;
    }
}
