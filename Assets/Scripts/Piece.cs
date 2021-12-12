using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int pieceType = 0;
    public bool color = true; //true is white, false is black
    public bool hasMoved = false;
    public int amountMoved = 0;

    public Vector2Int boardPosition;
    public Vector2Int startingBoardPosition;
    private static Piece[] pawn = new Piece[24];
    private static Piece[] none = new Piece[0];
    private static Piece[] rook = new Piece[26];
    private static Piece[] knight = new Piece[28];
    private static Piece[] bishop = new Piece[26];
    private static Piece[] king = new Piece[1];
    private static Piece[] queen = new Piece[1];
    private static Piece[] amazom = new Piece[4];
    private static Piece[] wizard = new Piece[2];
    private static Piece[] champion = new Piece[2];
    private static Piece[] archbishop = new Piece[2];
    private static Piece[] chancellor = new Piece[2];
    private static Piece[] falcon = new Piece[1];
    private static Piece[] hunter = new Piece[1];
    private static Piece[] dragon_horse = new Piece[1];
    private static Piece[] dragon_king = new Piece[1];
    private static Piece[] gold_general = new Piece[1];
    private static Piece[] silver_general = new Piece[1];
    private static Piece[] lance = new Piece[2];
    private static Piece[] ultima_pawn = new Piece[8];
    private static Piece[] long_leaper = new Piece[2];
    private static Piece[] cannon = new Piece[2];
    private static Piece[] guard = new Piece[2];
    private static Piece[] mao = new Piece[2];
    private static Piece[] elephant = new Piece[2];

    public void Initialize(Vector2 xy)
    {
        boardPosition = new Vector2Int((int)xy.x, (int)xy.y);
        startingBoardPosition = boardPosition;
        SpriteRenderer sprRenderer = gameObject.AddComponent<SpriteRenderer>();
        // int index = (int)((xy.y * 24) + xy.x);
        if (xy.y < 3)
        {
            color = true;
        }
        else
        {
            color = false;
        }

        if (xy.y == 2 || xy.y == 21)
        {
            pieceType = 1; //pawns
        }
        else
        {
            if (xy.y == 1 || xy.y == 22)
            {
                if (xy.x == 7 || xy.x == 16)
                {
                    pieceType = 8; //wizards
                }
                else if (xy.x == 8 || xy.x == 9 || xy.x == 14 || xy.x == 15)
                {
                    pieceType = 7; //amazons
                }
                else if (xy.x == 6 || xy.x == 17)
                {
                    pieceType = 4; //bishops
                }
                else if (xy.x == 11 || xy.x == 12)
                {
                    pieceType = 9; //champions
                }
                else if (xy.x == 22)
                {
                    pieceType = 12; //falcons
                }
                else if (xy.x == 1)
                {
                    pieceType = 13; //hunters
                }
                else if (xy.x == 2 || xy.x == 21)
                {
                    pieceType = 24; //elephants
                }
                else if (xy.x == 3 || xy.x == 20)
                {
                    pieceType = 20; //long leapers
                }
                else if (xy.x == 4 || xy.x == 19)
                {
                    pieceType = 23; //maos
                }
                else if (xy.x == 5 || xy.x == 18)
                {
                    pieceType = 22; //guards
                }
                else if (xy.x == 0 || xy.x == 23)
                {
                    pieceType = 18; //lances
                }
                else if (xy.x == 10 || xy.x == 13)
                {
                    pieceType = 21; // cannons
                }
            }
            else
            {
                if (xy.x == 0 || xy.x == 23)
                {
                    pieceType = 2; //rooks
                }
                else if (xy.x == 1)
                {
                    pieceType = 16; //gold general
                }
                else if (xy.x == 22)
                {
                    pieceType = 17; //silver general
                }
                else if (xy.x == 8 || xy.x == 9 || xy.x == 14 || xy.x == 15)
                {
                    pieceType = 3; //knights

                }
                else if (xy.x == 7 || xy.x == 16)
                {
                    pieceType = 11; //chancellors
                }
                else if (xy.x == 6 || xy.x == 17)
                {
                    pieceType = 10; //archbishops
                }
                else if (xy.x == 10)
                {
                    pieceType = 14; //dragon horses
                }
                else if (xy.x == 11)
                {
                    pieceType = 15; //dragon kings
                }
                else if (xy.x == 12)
                {
                    pieceType = 5; //kimgs

                }
                else if (xy.x == 13)
                {
                    pieceType = 6; //queens

                }
                else if (xy.x == 3 || xy.x == 4 || xy.x == 5 || xy.x == 2 || xy.x == 20 || xy.x == 19 || xy.x == 21 || xy.x == 18)
                {
                    pieceType = 19; //ultima pawns
                }
            }
        }

        if (color)
        {
            switch (pieceType)
            {
                case 0:
                    return;
                case 1:
                    sprRenderer.sprite = Resources.Load<Sprite>("pawn 1");
                    return;
                case 2:
                    sprRenderer.sprite = Resources.Load<Sprite>("rook 1");
                    return;
                case 3:
                    sprRenderer.sprite = Resources.Load<Sprite>("knight 1");
                    return;
                case 4:
                    sprRenderer.sprite = Resources.Load<Sprite>("bishop 1");
                    return;
                case 5:
                    sprRenderer.sprite = Resources.Load<Sprite>("king 1");
                    return;
                case 6:
                    sprRenderer.sprite = Resources.Load<Sprite>("queen 1");
                    return;
                case 7:
                    sprRenderer.sprite = Resources.Load<Sprite>("amazon 1");
                    return;
                case 8:
                    sprRenderer.sprite = Resources.Load<Sprite>("wizard 1");
                    return;
                case 9:
                    sprRenderer.sprite = Resources.Load<Sprite>("champion 1");
                    return;
                case 10:
                    sprRenderer.sprite = Resources.Load<Sprite>("archbishop 1");
                    return;
                case 11:
                    sprRenderer.sprite = Resources.Load<Sprite>("chancellor 1");
                    return;
                case 12:
                    sprRenderer.sprite = Resources.Load<Sprite>("falcon 1");
                    return;
                case 13:
                    sprRenderer.sprite = Resources.Load<Sprite>("hunter 1");
                    return;
                case 14:
                    sprRenderer.sprite = Resources.Load<Sprite>("dragon_horse 1");
                    return;
                case 15:
                    sprRenderer.sprite = Resources.Load<Sprite>("dragon_king 1");
                    return;
                case 16:
                    sprRenderer.sprite = Resources.Load<Sprite>("gold_general 1");
                    return;
                case 17:
                    sprRenderer.sprite = Resources.Load<Sprite>("silver_general 1");
                    return;
                case 18:
                    sprRenderer.sprite = Resources.Load<Sprite>("lance 1");
                    return;
                case 19:
                    sprRenderer.sprite = Resources.Load<Sprite>("ultima_pawn 1");
                    return;
                case 20:
                    sprRenderer.sprite = Resources.Load<Sprite>("long_leaper 1");
                    return;
                case 21:
                    sprRenderer.sprite = Resources.Load<Sprite>("cannon 1");
                    return;
                case 22:
                    sprRenderer.sprite = Resources.Load<Sprite>("guard 1");
                    return;
                case 23:
                    sprRenderer.sprite = Resources.Load<Sprite>("mao 1");
                    return;
                case 24:
                    sprRenderer.sprite = Resources.Load<Sprite>("elephant 1");
                    return;
            }
        }
        else
        {
            switch (pieceType)
            {
                case 0:
                    return;
                case 1:
                    sprRenderer.sprite = Resources.Load<Sprite>("pawn");
                    return;
                case 2:
                    sprRenderer.sprite = Resources.Load<Sprite>("rook");
                    return;
                case 3:
                    sprRenderer.sprite = Resources.Load<Sprite>("knight");
                    return;
                case 4:
                    sprRenderer.sprite = Resources.Load<Sprite>("bishop");
                    return;
                case 5:
                    sprRenderer.sprite = Resources.Load<Sprite>("king");
                    return;
                case 6:
                    sprRenderer.sprite = Resources.Load<Sprite>("queen");
                    return;
                case 7:
                    sprRenderer.sprite = Resources.Load<Sprite>("amazon");
                    return;
                case 8:
                    sprRenderer.sprite = Resources.Load<Sprite>("wizard");
                    return;
                case 9:
                    sprRenderer.sprite = Resources.Load<Sprite>("champion");
                    return;
                case 10:
                    sprRenderer.sprite = Resources.Load<Sprite>("archbishop");
                    return;
                case 11:
                    sprRenderer.sprite = Resources.Load<Sprite>("chancellor");
                    return;
                case 12:
                    sprRenderer.sprite = Resources.Load<Sprite>("falcon");
                    return;
                case 13:
                    sprRenderer.sprite = Resources.Load<Sprite>("hunter");
                    return;
                case 14:
                    sprRenderer.sprite = Resources.Load<Sprite>("dragon_horse");
                    return;
                case 15:
                    sprRenderer.sprite = Resources.Load<Sprite>("dragon_king");
                    return;
                case 16:
                    sprRenderer.sprite = Resources.Load<Sprite>("gold_general");
                    return;
                case 17:
                    sprRenderer.sprite = Resources.Load<Sprite>("silver_general");
                    return;
                case 18:
                    sprRenderer.sprite = Resources.Load<Sprite>("lance");
                    return;
                case 19:
                    sprRenderer.sprite = Resources.Load<Sprite>("ultima_pawn");
                    return;
                case 20:
                    sprRenderer.sprite = Resources.Load<Sprite>("long_leaper");
                    return;
                case 21:
                    sprRenderer.sprite = Resources.Load<Sprite>("cannon");
                    return;
                case 22:
                    sprRenderer.sprite = Resources.Load<Sprite>("guard");
                    return;
                case 23:
                    sprRenderer.sprite = Resources.Load<Sprite>("mao");
                    return;
                case 24:
                    sprRenderer.sprite = Resources.Load<Sprite>("elephant");
                    return;
            }
        }
    }

    public Sprite getPieceSprite(int pieceTypeSprite, bool colorSprite, SpriteRenderer sprRenderer)
    {
        if (colorSprite)
        {
            return pieceTypeSprite switch
            {
                0 => Resources.Load<Sprite>("none"),
                1 => Resources.Load<Sprite>("pawn 1"),
                2 => Resources.Load<Sprite>("rook 1"),
                3 => Resources.Load<Sprite>("knight 1"),
                4 => Resources.Load<Sprite>("bishop 1"),
                5 => Resources.Load<Sprite>("king 1"),
                6 => Resources.Load<Sprite>("queen 1"),
                7 => Resources.Load<Sprite>("amazon 1"),
                8 => Resources.Load<Sprite>("wizard 1"),
                9 => Resources.Load<Sprite>("champion 1"),
                10 => Resources.Load<Sprite>("archbishop 1"),
                11 => Resources.Load<Sprite>("chancellor 1"),
                12 => Resources.Load<Sprite>("falcon 1"),
                13 => Resources.Load<Sprite>("hunter 1"),
                14 => Resources.Load<Sprite>("dragon_horse 1"),
                15 => Resources.Load<Sprite>("dragon_king 1"),
                16 => Resources.Load<Sprite>("gold_general 1"),
                17 => Resources.Load<Sprite>("silver_general 1"),
                18 => Resources.Load<Sprite>("lance 1"),
                19 => Resources.Load<Sprite>("ultima_pawn 1"),
                20 => Resources.Load<Sprite>("long_leaper 1"),
                21 => Resources.Load<Sprite>("cannon 1"),
                22 => Resources.Load<Sprite>("guard 1"),
                23 => Resources.Load<Sprite>("mao 1"),
                24 => Resources.Load<Sprite>("elephant 1"),
                _ => Resources.Load<Sprite>("none"),
            };
        }
        else
        {
            return pieceTypeSprite switch
            {
                0 => Resources.Load<Sprite>("none"),
                1 => Resources.Load<Sprite>("pawn"),
                2 => Resources.Load<Sprite>("rook"),
                3 => Resources.Load<Sprite>("knight"),
                4 => Resources.Load<Sprite>("bishop"),
                5 => Resources.Load<Sprite>("king"),
                6 => Resources.Load<Sprite>("queen"),
                7 => Resources.Load<Sprite>("amazon"),
                8 => Resources.Load<Sprite>("wizard"),
                9 => Resources.Load<Sprite>("champion"),
                10 => Resources.Load<Sprite>("archbishop"),
                11 => Resources.Load<Sprite>("chancellor"),
                12 => Resources.Load<Sprite>("falcon"),
                13 => Resources.Load<Sprite>("hunter"),
                14 => Resources.Load<Sprite>("dragon_horse"),
                15 => Resources.Load<Sprite>("dragon_king"),
                16 => Resources.Load<Sprite>("gold_general"),
                17 => Resources.Load<Sprite>("silver_general"),
                18 => Resources.Load<Sprite>("lance"),
                19 => Resources.Load<Sprite>("ultima_pawn"),
                20 => Resources.Load<Sprite>("long_leaper"),
                21 => Resources.Load<Sprite>("cannon"),
                22 => Resources.Load<Sprite>("guard"),
                23 => Resources.Load<Sprite>("mao"),
                24 => Resources.Load<Sprite>("elephant"),
                _ => Resources.Load<Sprite>("none"),
            };
        }
    }

    public List<Vector2Int> GenerateMoves(Piece[] board)
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        if (pieceType == 1) //pawn
        {
            foreach (Vector2Int move in PawnMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 2) //rook
        {
            foreach (Vector2Int move in RookMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 3) //knight
        {
            foreach (Vector2Int move in KnightMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 4) //bishop
        {
            foreach (Vector2Int move in BishopMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 5) //king
        {
            foreach (Vector2Int move in KingMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 6) //queen
        {
            foreach (Vector2Int move in BishopMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in RookMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 7) //amazon
        {
            foreach (Vector2Int move in BishopMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in RookMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in KnightMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 8) //wizard
        {
            foreach (Vector2Int move in KnightMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in WizardMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 9) //Champion
        {
            foreach (Vector2Int move in ChampionMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 10) //Archbishop
        {
            foreach (Vector2Int move in KnightMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in BishopMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 11) //Chancellor
        {
            foreach (Vector2Int move in KnightMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in RookMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 12) //Falcon
        {
            foreach (Vector2Int move in FalconMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 13) //Hunter
        {
            foreach (Vector2Int move in HunterMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 14) //Dragon Horse
        {
            foreach (Vector2Int move in SingleRookMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in BishopMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 15) //Dragon King
        {
            foreach (Vector2Int move in SingleBishopMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in RookMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 16) //Gold General
        {
            foreach (Vector2Int move in SingleRookMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in GoldMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 17) //Silver General
        {
            foreach (Vector2Int move in SingleBishopMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in SilverMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 18) // Lance
        {
            foreach (Vector2Int move in LanceMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 19) //Ultima Pawn
        {
            foreach (Vector2Int move in SingleRookMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 20) // Long Leaper
        {
            foreach (Vector2Int move in BishopMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in LongMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in SingleLongMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 21) //Cannon
        {
            foreach (Vector2Int move in LongMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in CannonMoves(board))
            {
                moves.Add(move);
            }
        } 
        else if (pieceType == 22) //Guard
        {
            foreach (Vector2Int move in SingleBishopMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 23) //Mao
        {
            foreach (Vector2Int move in SingleBishopMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in SingleRookMoves(board))
            {
                moves.Add(move);
            }
            foreach (Vector2Int move in KnightMoves(board))
            {
                moves.Add(move);
            }
        }
        else if (pieceType == 24) //Elephant
        {
            foreach (Vector2Int move in ElephantMoves(board))
            {
                moves.Add(move);
            }
        }
        return moves;
    }

    List<Vector2Int> ElephantMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 2), new Vector2Int(-1, 2),
            new Vector2Int(1, -2), new Vector2Int(-1, -2)};
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach (Vector2Int offset in offsets)
        {
            Vector2Int index = boardPosition + offset;
            if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
            {
                continue;
            }
            else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
            {
                continue;
            }
            moves.Add(index);
        }
        return moves;
    }

    List<Vector2Int> CannonMoves(Piece[] board)
    {
        if (color)
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(0, 1), new Vector2Int(0, 3) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
        else
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(0, -1), new Vector2Int(0, -3) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> SingleLongMoves(Piece[] board)
    {
        if (color)
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, 1), new Vector2Int(0, 2) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, -1), new Vector2Int(0, -2) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> LongMoves(Piece[] board)
    {
        if (color)
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, -1),
            new Vector2Int(1, 0), new Vector2Int(-1, 0)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].color != color)
                        {
                            moves.Add(index);
                        }
                        break;
                    }
                    moves.Add(index);
                }
            }
            return moves;
        }
        else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, 1),
            new Vector2Int(1, 0), new Vector2Int(-1, 0)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].color != color)
                        {
                            moves.Add(index);
                        }
                        break;
                    }
                    moves.Add(index);
                }
            }
            return moves;
        }
    }

    public void PromotionPieceType()
    {
        MovePlays mp = new MovePlays();
        GameManager gm = GameManager.current;
        if (!gm.isAiVsMode)
        {
            if (!gm.isPlayerIsBlackMode)
            {
                if (gm.isPlayerAI)
                {
                    if (!gm.isPlayerWhite)
                    {
                        switch (mp.MoveFlagAI())
                        {
                            case MovePlays.Flag.PromoteToRook:
                                gm.promotePawnToRookAI();
                                break;
                            case MovePlays.Flag.PromoteToKnight:
                                gm.promotePawnToKnightAI();
                                break;
                            case MovePlays.Flag.PromoteToBishop:
                                gm.promotePawnToBishopAI();
                                break;
                            case MovePlays.Flag.PromoteToQueen:
                                gm.promotePawnToQueenAI();
                                break;
                        }
                    }
                    else
                    {
                        switch (mp.MoveFlagPlayer())
                        {
                            case MovePlays.Flag.PromoteToRook:
                                gm.promotePawnToRook();
                                break;
                            case MovePlays.Flag.PromoteToKnight:
                                gm.promotePawnToKnight();
                                break;
                            case MovePlays.Flag.PromoteToBishop:
                                gm.promotePawnToBishop();
                                break;
                            case MovePlays.Flag.PromoteToQueen:
                                gm.promotePawnToQueen();
                                break;
                        }
                    }
                }
                else
                {

                    switch (mp.MoveFlagPlayer())
                    {
                        case MovePlays.Flag.PromoteToRook:
                            gm.promotePawnToRook();
                            break;
                        case MovePlays.Flag.PromoteToKnight:
                            gm.promotePawnToKnight();
                            break;
                        case MovePlays.Flag.PromoteToBishop:
                            gm.promotePawnToBishop();
                            break;
                        case MovePlays.Flag.PromoteToQueen:
                            gm.promotePawnToQueen();
                            break;
                    }
                }
            } else
            {
                if (gm.isPlayerAI)
                {
                    if (gm.isPlayerWhite)
                    {
                        switch (mp.MoveFlagAI())
                        {
                            case MovePlays.Flag.PromoteToRook:
                                gm.promotePawnToRookAI();
                                break;
                            case MovePlays.Flag.PromoteToKnight:
                                gm.promotePawnToKnightAI();
                                break;
                            case MovePlays.Flag.PromoteToBishop:
                                gm.promotePawnToBishopAI();
                                break;
                            case MovePlays.Flag.PromoteToQueen:
                                gm.promotePawnToQueenAI();
                                break;

                        }
                    }
                    else
                    {
                        switch (mp.MoveFlagPlayer())
                        {
                            case MovePlays.Flag.PromoteToRook:
                                gm.promotePawnToRook();
                                break;
                            case MovePlays.Flag.PromoteToKnight:
                                gm.promotePawnToKnight();
                                break;
                            case MovePlays.Flag.PromoteToBishop:
                                gm.promotePawnToBishop();
                                break;
                            case MovePlays.Flag.PromoteToQueen:
                                gm.promotePawnToQueen();
                                break;
                        }
                    }
                }
                else
                {

                    switch (mp.MoveFlagPlayer())
                    {
                        case MovePlays.Flag.PromoteToRook:
                            gm.promotePawnToRook();
                            break;
                        case MovePlays.Flag.PromoteToKnight:
                            gm.promotePawnToKnight();
                            break;
                        case MovePlays.Flag.PromoteToBishop:
                            gm.promotePawnToBishop();
                            break;
                        case MovePlays.Flag.PromoteToQueen:
                            gm.promotePawnToQueen();
                            break;
                    }
                }
            }
        } else
        {
            if (gm.isPlayerAI)
            {
                switch (mp.MoveFlagAI())
                {
                    case MovePlays.Flag.PromoteToRook:
                        gm.promotePawnToRookAI();
                        break;
                    case MovePlays.Flag.PromoteToKnight:
                        gm.promotePawnToKnightAI();
                        break;
                    case MovePlays.Flag.PromoteToBishop:
                        gm.promotePawnToBishopAI();
                        break;
                    case MovePlays.Flag.PromoteToQueen:
                        gm.promotePawnToQueenAI();
                        break;
                }
            }
            else
            {
                switch (mp.MoveFlagAI())
                {
                    case MovePlays.Flag.PromoteToRook:
                        gm.promotePawnToRookAI();
                        break;
                    case MovePlays.Flag.PromoteToKnight:
                        gm.promotePawnToKnightAI();
                        break;
                    case MovePlays.Flag.PromoteToBishop:
                        gm.promotePawnToBishopAI();
                        break;
                    case MovePlays.Flag.PromoteToQueen:
                        gm.promotePawnToQueenAI();
                        break;
                }
            }
        }
    }

    public static  Piece[] getPieceFromInt(int piece, bool color)
    {
        if (color)
        {
            switch (piece)
            {
                case 0:
                    return none;
                case 1:
                    return pawn;
                case 2:
                    return rook;
                case 3:
                    return knight;
                case 4:
                    return bishop;
                case 5:
                    return king;
                case 6:
                    return queen;
                case 7:
                    return amazom;
                case 8:
                    return wizard;
                case 9:
                    return champion;
                case 10:
                    return archbishop;
                case 11:
                    return chancellor;
                case 12:
                    return falcon;
                case 13:
                    return hunter;
                case 14:
                    return dragon_horse;
                case 15:
                    return dragon_king;
                case 16:
                    return gold_general;
                case 17:
                    return silver_general;
                case 18:
                    return lance;
                case 19:
                    return ultima_pawn;
                case 20:
                    return long_leaper;
                case 21:
                    return cannon;
                case 22:
                    return guard;
                case 23:
                    return mao;
                case 24:
                    return elephant;
                default:
                    return none;
            }
        }
        else
        {
            switch (piece)
            {
                case 0:
                    return none;
                case 1:
                    return pawn;
                case 2:
                    return rook;
                case 3:
                    return knight;
                case 4:
                    return bishop;
                case 5:
                    return king;
                case 6:
                    return queen;
                case 7:
                    return amazom;
                case 8:
                    return wizard;
                case 9:
                    return champion;
                case 10:
                    return archbishop;
                case 11:
                    return chancellor;
                case 12:
                    return falcon;
                case 13:
                    return hunter;
                case 14:
                    return dragon_horse;
                case 15:
                    return dragon_king;
                case 16:
                    return gold_general;
                case 17:
                    return silver_general;
                case 18:
                    return lance;
                case 19:
                    return ultima_pawn;
                case 20:
                    return long_leaper;
                case 21:
                    return cannon;
                case 22:
                    return guard;
                case 23:
                    return mao;
                case 24:
                    return elephant;
                default:
                    return none;
            }
        }
    }

    List<Vector2Int> PawnMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(-1, 0), new Vector2Int(1, 0) };
        List<Vector2Int> moves = new List<Vector2Int>();
        if (boardPosition.y == 23|| boardPosition.y == 0)
        {
            PromotionPieceType();
            return moves;
        }
        int colorType = (color) ? 1 : -1;
        Vector2Int pawnMove = new Vector2Int(0, 1);
        Vector2Int[] Moons = new Vector2Int[] { new Vector2Int(0, 1) };
        Vector2Int index;
        foreach (Vector2Int offset in offsets)
        {
            index = boardPosition + pawnMove * colorType + offset;
            if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
            {
                continue;
            }
            else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color != color)
            {
                moves.Add(index);
            }
        }

        foreach (Vector2Int Moon in Moons)
        {
            index = boardPosition + Moon * colorType;
            if (index.x > 24 || index.y > 23 || index.x < 0 || index.y < 0)
            {
                continue;
            }
            else
            {
                if (board[index.y * 24 + index.x])
                {
                    return moves;
                }
                moves.Add(index);

                if (amountMoved == 0)
                {
                    index = boardPosition + 2 * pawnMove * colorType;
                    if (board[index.y * 24 + index.x])
                    {
                        return moves;
                    }
                    moves.Add(index);
                }
            }
        }
        return moves;
    }

    List<Vector2Int> RookMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, 1), new Vector2Int(0, -1),
            new Vector2Int(1, 0), new Vector2Int(-1, 0)};
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach (Vector2Int offset in offsets)
        {
            for (int i = 1; i < 24; i++)
            {
                Vector2Int index = boardPosition + offset * i;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    break;
                }
                else if (board[index.y * 24 + index.x] != null)
                {
                    if (board[index.y * 24 + index.x].color != color)
                    {
                        moves.Add(index);
                    }
                    break;
                }
                moves.Add(index);
            }
        }
        return moves;
    }
     List<Vector2Int> SingleBishopMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(-1, 1), new Vector2Int(1, -1),
            new Vector2Int(1, 1), new Vector2Int(-1, -1) };
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach (Vector2Int offset in offsets)
        {
            Vector2Int index = boardPosition + offset;
            if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
            {
                continue;
            }
            else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
            {
                continue;
            }
            moves.Add(index);
        }
        return moves;
    }

    List<Vector2Int> SingleRookMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(-1, 0), new Vector2Int(0, -1),
            new Vector2Int(0, 1), new Vector2Int(1, 0) };
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach (Vector2Int offset in offsets)
        {
            Vector2Int index = boardPosition + offset;
            if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
            {
                continue;
            }
            else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
            {
                continue;
            }
            moves.Add(index);
        }
        return moves;
    }

    List<Vector2Int> WizardMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 5), new Vector2Int(-1, 5),
            new Vector2Int(5, 1), new Vector2Int(5, -1),
            new Vector2Int(-5, 1), new Vector2Int(-5, -1),
            new Vector2Int(1, -5), new Vector2Int(-1, -5)};
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach (Vector2Int offset in offsets)
        {
            Vector2Int index = boardPosition + offset;
            if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
            {
                continue;
            }
            else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
            {
                continue;
            }
            moves.Add(index);
        }
        return moves;
    }

    List<Vector2Int> ChampionMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, 1), new Vector2Int(0, 2),
            new Vector2Int(2, 0), new Vector2Int(1, 0),
            new Vector2Int(0, -1), new Vector2Int(0, -2),
            new Vector2Int(-1, 0), new Vector2Int(-2, 0), 
            new Vector2Int(-2, 2), new Vector2Int(-2, -2),
            new Vector2Int(2, 2), new Vector2Int(2, -2)
        };
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach (Vector2Int offset in offsets)
        {
            Vector2Int index = boardPosition + offset;
            if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
            {
                continue;
            }
            else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
            {
                continue;
            }
            moves.Add(index);
        }
        return moves;
    }

    List<Vector2Int> SilverMoves(Piece[] board)
    {
        if (color)
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(0, 1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(0, -1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> GoldMoves(Piece[] board)
    {
        if (color)
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(1, 1), new Vector2Int(-1, 1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(-1,-1), new Vector2Int(1, -1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> LanceMoves(Piece[] board)
    {
        if (color)
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, 1), new Vector2Int(0, 2),
            new Vector2Int(0, 3), new Vector2Int(0, 4)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, -1), new Vector2Int(0, -2),
            new Vector2Int(0, -3), new Vector2Int(0, -4)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                Vector2Int index = boardPosition + offset;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    continue;
                }
                else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
                {
                    continue;
                }
                moves.Add(index);
            }
            return moves;
        }
    }

    List<Vector2Int> KnightMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 2), new Vector2Int(-1, 2),
            new Vector2Int(2, 1), new Vector2Int(2, -1),
            new Vector2Int(-2, 1), new Vector2Int(-2, -1),
            new Vector2Int(1, -2), new Vector2Int(-1, -2)};
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach (Vector2Int offset in offsets)
        {
            Vector2Int index = boardPosition + offset;
            if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
            {
                continue;
            }
            else if (board[index.y * 24 + index.x] != null && board[index.y * 24 + index.x].color == color)
            {
                continue;
            }
            moves.Add(index);
        }
        return moves;
    }

    List<Vector2Int> HunterMoves(Piece[] board)
    {
        if (!color)
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(1, 1), new Vector2Int(-1, 1), new Vector2Int(0, -1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].color != color)
                        {
                            moves.Add(index);
                        }
                        break;
                    }
                    moves.Add(index);
                }    
            }
            return moves;
        }
        else
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(-1, -1), new Vector2Int(1, -1), new Vector2Int(0, 1) };
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].color != color)
                        {
                            moves.Add(index);
                        }
                        break;
                    }
                    moves.Add(index);
                }
            }
            return moves;
        }
    }

    List<Vector2Int> FalconMoves(Piece[] board)
    {
        if (color)
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(1, 1), new Vector2Int(-1, 1), new Vector2Int(0, -1)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].color != color)
                        {
                            moves.Add(index);
                        }
                        break;
                    }
                    moves.Add(index);
                }
            }
            return moves;
        } else
        {
            Vector2Int[] offsets = new Vector2Int[] { new Vector2Int(-1, -1), new Vector2Int(1, -1), new Vector2Int(0, 1)};
            List<Vector2Int> moves = new List<Vector2Int>();
            foreach (Vector2Int offset in offsets)
            {
                for (int i = 1; i < 24; i++)
                {
                    Vector2Int index = boardPosition + offset * i;
                    if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                    {
                        break;
                    }
                    else if (board[index.y * 24 + index.x] != null)
                    {
                        if (board[index.y * 24 + index.x].color != color)
                        {
                            moves.Add(index);
                        }
                        break;
                    }
                    moves.Add(index);
                }
            }
            return moves;
        }
    }

    List<Vector2Int> BishopMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 1), new Vector2Int(1, -1),
            new Vector2Int(-1, -1), new Vector2Int(-1, 1)};
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach (Vector2Int offset in offsets)
        {
            for (int i = 1; i < 24; i++)
            {
                Vector2Int index = boardPosition + offset * i;
                if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
                {
                    break;
                }
                else if (board[index.y * 24 + index.x] != null)
                {
                    if (board[index.y * 24 + index.x].color != color)
                    {
                        moves.Add(index);
                    }
                    break;
                }
                moves.Add(index);
            }
        }
        return moves;
    }

    List<Vector2Int> KingMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{
            new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1),
            new Vector2Int(-1, 0), new Vector2Int(1, 0),
            new Vector2Int(1, -1), new Vector2Int(0, -1), new Vector2Int(-1, -1)};
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach (Vector2Int offset in offsets)
        {
            Vector2Int index = boardPosition + offset;
            if (index.x > 23 || index.y > 23 || index.x < 0 || index.y < 0)
            {
                continue;
            }
            else if (board[index.y * 24 + index.x] != null)
            {
                if (board[index.y * 24 + index.x].color == color)
                {
                    continue;
                }
            }
            moves.Add(index);
        }
        if (amountMoved == 0)
        {
            int index = boardPosition.y * 24 + boardPosition.x;
            if (board[index - 1] == null && board[index - 2] == null && board[index - 3] == null && board[index - 4] != null)
            {
                if (board[index - 4].amountMoved == 0)
                {
                    moves.Add(new Vector2Int(boardPosition.x - 2, boardPosition.y));
                }
            }
            if (board[index + 1] == null && board[index + 2] == null && board[index + 3] != null)
            {
                if (board[index + 3].amountMoved == 0)
                {
                    moves.Add(new Vector2Int(boardPosition.x + 2, boardPosition.y));
                }
            }
        }
        return moves;
    }

}


