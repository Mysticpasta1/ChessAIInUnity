using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    const int None = 0;
    const int PAWN = 1;
    const int ROOK = 2;
    const int KNIGHT = 3;
    const int BISHOP = 4;
    const int KING = 5;
    const int QUEEN = 6;
    
    public int pieceType = 0;
    public bool color = true; //true is white, false is black
    public bool hasMoved = false;
    public int amountMoved = 0;

    public Vector2Int boardPosition;
    public Vector2Int startingBoardPosition;
    public void Initialize(Vector2 xy)
    {
        boardPosition = new Vector2Int((int)xy.x, (int)xy.y);
        startingBoardPosition = boardPosition;
        SpriteRenderer sprRenderer = gameObject.AddComponent<SpriteRenderer>();
        int index = (int)(xy.y*8 + xy.x);
        if (xy.y < 2)
        {
            color = true;
        }
        else{
            color = false;
        }

        if (xy.y == 1 || xy.y == 6)
        {
            pieceType = 1;
        }
        else
        {
            if (xy.x == 0 || xy.x == 7)
            {
                pieceType = 2;
            }
            else if (xy.x == 1 || xy.x == 6)
            {
                pieceType = 3;

            }
            else if (xy.x == 2 || xy.x == 5)
            {
                pieceType = 4;

            }
            else if (xy.x == 4)
            {
                pieceType = 5;

            }
            else if (xy.x == 3)
            {
                pieceType = 6;

            }
        }
        

        if (color)
        {
           switch (pieceType)
            {
                case 0:
                    return;
                case 1:
                    sprRenderer.sprite = Resources.Load<Sprite>("PawnW");
                    return;
                case 2:
                    sprRenderer.sprite = Resources.Load<Sprite>("RookW");
                    return;
                case 3:
                    sprRenderer.sprite = Resources.Load<Sprite>("KnightW");
                    return;
                case 4:
                    sprRenderer.sprite = Resources.Load<Sprite>("BishopW");
                    return;
                case 5:
                    sprRenderer.sprite = Resources.Load<Sprite>("KingW");
                    return;
                case 6:
                    sprRenderer.sprite = Resources.Load<Sprite>("QueenW");
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
                    sprRenderer.sprite = Resources.Load<Sprite>("PawnB");
                    return;
                case 2:
                    sprRenderer.sprite = Resources.Load<Sprite>("RookB");
                    return;
                case 3:
                    sprRenderer.sprite = Resources.Load<Sprite>("KnightB");
                    return;
                case 4:
                    sprRenderer.sprite = Resources.Load<Sprite>("BishopB");
                    return;
                case 5:
                    sprRenderer.sprite = Resources.Load<Sprite>("KingB");
                    return;
                case 6:
                    sprRenderer.sprite = Resources.Load<Sprite>("QueenB");
                    return;
            }
        }
    }

    public List<Vector2Int> GenerateMoves(Piece[] board)
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        if (pieceType ==1) //pawn
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

        return moves;
    }

    List<Vector2Int> PawnMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(-1, 0), new Vector2Int(1, 0)};
        List<Vector2Int> moves = new List<Vector2Int>();
        if (boardPosition.y ==7 || boardPosition.y == 0)
        {
            return moves;
        }
        int colorType = (color) ? 1 : -1;
        Vector2Int pawnMove = new Vector2Int(0,1);
        Vector2Int index;
        foreach(Vector2Int offset in offsets)
        {
            index = boardPosition + pawnMove*colorType + offset;
            if (index.x > 7 || index.y > 7 || index.x < 0 || index.y < 0)
            {
                continue;
            }
            else if (board[index.y*8+index.x] !=null && board[index.y*8+index.x].color != color)
            {
                moves.Add(index);
            }
        }

        index = boardPosition + pawnMove*colorType;
        if (board[index.y*8+index.x])
        {
            return moves;
        }
        moves.Add(index);
        
        if (amountMoved == 0)
        {
            index = boardPosition + 2*pawnMove*colorType;
            if (board[index.y*8+index.x])
            {
                return moves;
            }
            moves.Add(index);
        }
        return moves;
    }

    List<Vector2Int> RookMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(0, 1), new Vector2Int(0, -1),
            new Vector2Int(1, 0), new Vector2Int(-1, 0)};
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach(Vector2Int offset in offsets)
        {
            for (int i=1; i<8; i++)
            {
                Vector2Int index = boardPosition + offset*i;
                if (index.x > 7 || index.y > 7 || index.x < 0 || index.y < 0)
                {
                    break;
                }
                else if (board[index.y*8+index.x] !=null)
                {
                    if (board[index.y*8+index.x].color != color)
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

    List<Vector2Int> KnightMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 2), new Vector2Int(-1, 2),
            new Vector2Int(2, 1), new Vector2Int(2, -1),
            new Vector2Int(-2, 1), new Vector2Int(-2, -1),
            new Vector2Int(1, -2), new Vector2Int(-1, -2)};
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach(Vector2Int offset in offsets)
        {
            Vector2Int index = boardPosition + offset;
            if (index.x > 7 || index.y > 7 || index.x < 0 || index.y < 0)
            {
                continue;
            }
            else if (board[index.y*8+index.x] !=null && board[index.y*8+index.x].color == color)
            {
                continue;
            }
            moves.Add(index);
        }
        return moves;
    }

    List<Vector2Int> BishopMoves(Piece[] board)
    {
        Vector2Int[] offsets = new Vector2Int[]{new Vector2Int(1, 1), new Vector2Int(1, -1),
            new Vector2Int(-1, -1), new Vector2Int(-1, 1)};
        List<Vector2Int> moves = new List<Vector2Int>();
        foreach(Vector2Int offset in offsets)
        {
            for (int i=1; i<8; i++)
            {
                Vector2Int index = boardPosition + offset*i;
                if (index.x > 7 || index.y > 7 || index.x < 0 || index.y < 0)
                {
                    break;
                }
                else if (board[index.y*8+index.x] !=null)
                {
                    if (board[index.y*8+index.x].color != color)
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
        foreach(Vector2Int offset in offsets)
        {
            Vector2Int index = boardPosition + offset;
            if (index.x > 7 || index.y > 7 || index.x < 0 || index.y < 0)
            {
                continue;
            }
            else if (board[index.y*8+index.x] !=null)
            {
                if (board[index.y*8+index.x].color == color)
                {
                    continue;
                }
            }
            moves.Add(index);
        }
        if (amountMoved==0)
        {
            int index = boardPosition.y*8+boardPosition.x;
            if (board[index - 1] == null && board[index - 2] == null && board[index - 3] == null && board[index - 4] != null)
            {
                if (board[index-4].amountMoved == 0)
                {
                    moves.Add(new Vector2Int(boardPosition.x - 2, boardPosition.y));
                }
            }
            if (board[index + 1] == null && board[index + 2] == null && board[index+3] != null)
            {
                if (board[index+3].amountMoved == 0)
                {
                    moves.Add(new Vector2Int(boardPosition.x + 2, boardPosition.y));
                }
            }
        }
        return moves;
    }

}
