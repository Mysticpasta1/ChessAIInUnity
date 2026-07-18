# Chess-Ultima (JavaFX)

Chess-Ultima is a 24×24 fairy-chess variant with a large mixed army drawn from
Western chess, shogi, xiangqi and Ultima. This is a JavaFX rewrite of the
original Unity project — the piece set, FEN format and material values are kept,
while movement uses corrected, clean rules for each piece.

## Features

- **24×24 board** with the full Chess-Ultima army (24 piece types).
- **Local 2-player** on one machine.
- **Play vs Computer** — iterative-deepening negamax + alpha-beta AI, six difficulty levels (Easy → Grandmaster).
- **AI vs AI (watch)** — pit two engines against each other (pick a level per side) for testing.
- **Online multiplayer** — peer-to-peer over TCP (one side hosts, the other joins).
- **Load any position** from a Chess-Ultima FEN string.
- Zoomable / pannable board, legal-move highlighting, check indication,
  castling, and pawn promotion.

## Requirements

- JDK 17 or newer.
- No separate JavaFX install — the `org.openjfx` Gradle plugin pulls it in.

## Run

```bash
./gradlew run
```

The first run downloads the JavaFX artifacts, then launches the game menu.

## Build

```bash
./gradlew build
```

## Piece set

| Id | Piece | FEN | Movement (clean rules) |
|----|-------|-----|------------------------|
| 1  | Pawn | p | forward 1 (2 from start), captures diagonally, promotes |
| 2  | Rook | r | orthogonal slider |
| 3  | Knight | n | (1,2) leaper |
| 4  | Bishop | b | diagonal slider |
| 5  | King | k | one step any direction, castling |
| 6  | Queen | q | rook + bishop |
| 7  | Amazon | a | queen + knight |
| 8  | Wizard | z | knight + (1,5) camel leaps |
| 9  | Champion | e | 1–2 orthogonal + (2,2) diagonal leaps |
| 10 | Archbishop | c | bishop + knight |
| 11 | Chancellor | f | rook + knight |
| 12 | Falcon | l | forward diagonal + backward orthogonal slider |
| 13 | Hunter | s | forward orthogonal + backward diagonal slider |
| 14 | Dragon Horse | h | bishop + one-step orthogonal |
| 15 | Dragon King | i | rook + one-step diagonal |
| 16 | Gold General | m | shogi gold |
| 17 | Silver General | t | shogi silver |
| 18 | Lance | u | forward orthogonal slider |
| 19 | Ultima Pawn | x | one-step orthogonal |
| 20 | Long Leaper | v | slides/captures in all 8 directions; may also leap 1 screen to capture the piece beyond (landing on it) |
| 21 | Cannon | d | xiangqi cannon |
| 22 | Guard | o | one step any direction |
| 23 | Mao | w | xiangqi horse (blockable knight) |
| 24 | Elephant | j | xiangqi elephant (blockable 2-diagonal) |

## Online play

1. One player picks **Online – Host Game**, chooses a port and a colour.
2. The other picks **Online – Join Game** and enters the host's address and port.

The host decides the starting position and both players' colours; moves are then
exchanged automatically.
