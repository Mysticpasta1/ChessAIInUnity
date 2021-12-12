using System;
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
    private PieceList[] none = new PieceList[] { new PieceList(0), new PieceList(0) };
    private PieceList[] knight = new PieceList[] { new PieceList(26), new PieceList(26) };
    private PieceList[] pawn = new PieceList[] { new PieceList(24), new PieceList(24) };
    private PieceList[] rook = new PieceList[] { new PieceList(26), new PieceList(26) };
    private PieceList[] bishop = new PieceList[] { new PieceList(26), new PieceList(26) };
    private PieceList[] queen = new PieceList[] { new PieceList(25), new PieceList(25) };
    private PieceList[] amazon = new PieceList[] { new PieceList(4), new PieceList(4) };
    private PieceList[] gold_general = new PieceList[] { new PieceList(1), new PieceList(1) };
    private PieceList[] silver_general = new PieceList[] { new PieceList(1), new PieceList(1) };
    private PieceList[] ultima_pawn = new PieceList[] { new PieceList(4), new PieceList(4) };
    private PieceList[] cannon = new PieceList[] { new PieceList(2), new PieceList(2) };
    private PieceList[] archbishop = new PieceList[] { new PieceList(2), new PieceList(2) };
    private PieceList[] lance = new PieceList[] { new PieceList(2), new PieceList(2) };
    private PieceList[] mao = new PieceList[] { new PieceList(2), new PieceList(2) };
    private PieceList[] elephant = new PieceList[] { new PieceList(2), new PieceList(2) };
    private PieceList[] guard = new PieceList[] { new PieceList(2), new PieceList(2) };
    private PieceList[] falcon = new PieceList[] { new PieceList(1), new PieceList(1) };
    private PieceList[] hunter = new PieceList[] { new PieceList(1), new PieceList(1) };
    private PieceList[] dragon_king = new PieceList[] { new PieceList(1), new PieceList(1) };
    private PieceList[] dragon_horse = new PieceList[] { new PieceList(1), new PieceList(1) };
    private PieceList[] chancellor = new PieceList[] { new PieceList(2), new PieceList(2) };
    private PieceList[] long_leaper = new PieceList[] { new PieceList(2), new PieceList(2) };
    private PieceList[] wizard = new PieceList[] { new PieceList(2), new PieceList(2) };
    private PieceList[] king = new PieceList[] { new PieceList(1), new PieceList(1) };
    private PieceList[] champion = new PieceList[] { new PieceList(2), new PieceList(2) };


    public string getPieceText(int pieceTypeText, bool colorText)
    {
        if (colorText)
        {
            return pieceTypeText switch
            {
                0 => "none",
                1 => "pawn",
                2 => "rook",
                3 => "knight",
                4 => "bishop",
                5 => "king",
                6 => "queen",
                7 => "amazon",
                8 => "wizard",
                9 => "champion",
                10 => "archbishop",
                11 => "chancellor",
                12 => "falcon",
                13 => "hunter",
                14 => "dragon horse",
                15 => "dragon king",
                16 => "gold general",
                17 => "silver general",
                18 => "lance",
                19 => "ultima_pawn",
                20 => "long leaper",
                21 => "cannon",
                22 => "guard",
                23 => "mao",
                24 => "elephant",
                _ => "none",
            };
        }
        else
        {
            return pieceTypeText switch
            {
                0 => "none",
                1 => "pawn",
                2 => "rook",
                3 => "knight",
                4 => "bishop",
                5 => "king",
                6 => "queen",
                7 => "amazon",
                8 => "wizard",
                9 => "champion",
                10 => "archbishop",
                11 => "chancellor",
                12 => "falcon",
                13 => "hunter",
                14 => "dragon horse",
                15 => "dragon king",
                16 => "gold general",
                17 => "silver general",
                18 => "lance",
                19 => "ultima_pawn",
                20 => "long leaper",
                21 => "cannon",
                22 => "guard",
                23 => "mao",
                24 => "elephant",
                _ => "none",
            };
        }
    }

    public void Initialize(String fen)
    {
        //boardPosition = new Vector2Int((int)xy.x, (int)xy.y);
        //startingBoardPosition = boardPosition;
        SpriteRenderer sprRenderer = gameObject.AddComponent<SpriteRenderer>();
        // int index = (int)((xy.y * 24) + xy.x);

        var loadedPosition = FenUtility.PositionFromFen(fen);
        for (int squareIndex = 0; squareIndex < 576; squareIndex++)
        {
            int piece = loadedPosition.squares[squareIndex];
            GameManager gameManager = GameManager.current;
            int pieceColorIndex = color ? 0 : 1 ;
            
            switch(pieceType)
            {
                default:
                    break;
                case 0:
                    break;
                case 1:
                    pawn[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 2:
                    rook[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 3:
                    knight[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 4:
                    bishop[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 5:
                    king[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 6:
                    queen[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 7:
                    amazon[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 8:
                    wizard[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 9:
                    champion[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 10:
                    archbishop[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 11:
                    chancellor[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 12:
                    falcon[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 13:
                    hunter[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 14:
                    dragon_horse[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 15:
                    dragon_king[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 16:
                    gold_general[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 17:
                    silver_general[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 18:
                    lance[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 19:
                    ultima_pawn[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 20:
                    long_leaper[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 21:
                    cannon[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 22:
                    guard[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 23:
                    mao[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
                case 24:
                    elephant[pieceColorIndex].AddPieceAtSquare(squareIndex);
                    break;
            }
        }
        /* if (xy.y < 3)
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
         }*/

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

    public PieceList[] getPieceFromInt(int piece, bool color)
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
                    return amazon;
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
                    return amazon;
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


