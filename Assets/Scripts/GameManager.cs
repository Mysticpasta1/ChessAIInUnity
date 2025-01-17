﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    public Piece []pieces;
    public GameObject []tiles;

    Piece selectedPiece;
    List<Vector2Int> possiblePlayerMoves;
    bool isPlayerTurn;
    bool isPlayerWhite; //0 is white

    Stack<Move> undoMoves;
    Move bestNegamaxMove;
    float boardEvaluationForPlayer = 0;
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        pieces = new Piece[64];
        tiles = new GameObject[64];
        isPlayerWhite = true;
        isPlayerTurn = isPlayerWhite;
        undoMoves = new Stack<Move>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerTurn)
        {
            if (!HasPlayableMoves(isPlayerWhite) && IsCheck(isPlayerWhite))
            {
                Debug.Log("CheckMate on Player");
            }
            else if (!HasPlayableMoves(isPlayerWhite))
            {
                Debug.Log("StaleMate");
            }
            PlayerMove(isPlayerWhite);
        }
        else
        {
            if (!HasPlayableMoves(!isPlayerWhite) && IsCheck(!isPlayerWhite))
            {
                Debug.Log("CheckMate on AI");
            }
            else if (!HasPlayableMoves(!isPlayerWhite))
            {
                Debug.Log("StaleMate");
            }
            AIMove(!isPlayerWhite);
            //PlayerMove(!isPlayerWhite);
            isPlayerTurn = !isPlayerTurn;
        }
    }
    
    void PlayerMove(bool whoseTurn)
    {
        PickUpPiece(whoseTurn);
        if (selectedPiece)
        {
            selectedPiece.gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0,0,-1f);
        }
    }

    void AIMove(bool aiColor)
    {
        Negamax(4, aiColor, Mathf.NegativeInfinity, Mathf.Infinity);
        //GenerateAllMovesForColor(aiColor);
        List<Vector2Int> listOfMovesTemp = new List<Vector2Int>();
        listOfMovesTemp.Add(bestNegamaxMove.attackedPieceBP);
        PlayMove(true, bestNegamaxMove, listOfMovesTemp, false);
    }

    void PickUpPiece(bool whoseTurn) //pickup piece player
    {
        if (selectedPiece == null)
        {
            if (Input.GetMouseButtonDown(0)) //player selects piece
            {
                Vector2Int mousePosition = GetMousePosition();
                if (mousePosition.x < 0 || mousePosition.x > 7 || mousePosition.y < 0 || mousePosition.y > 7)
                {
                    return;
                }
                selectedPiece = pieces[mousePosition.y * 8 + mousePosition.x];
                if (selectedPiece != null)
                {
                    if (selectedPiece.color != whoseTurn)
                    {
                        selectedPiece = null;
                        return;
                    }
                    possiblePlayerMoves = selectedPiece.GenerateMoves(pieces);
                    RemoveIllegalMoves(whoseTurn, possiblePlayerMoves, selectedPiece);
                    if(possiblePlayerMoves.Count ==0)
                    {
                        selectedPiece = null;
                        return;
                    }
                    ColorTiles(possiblePlayerMoves, false);
                }
            }
           
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {

                Vector2Int mousePosition = GetMousePosition();
                if (mousePosition.x < 0 || mousePosition.x > 7 || mousePosition.y < 0 || mousePosition.y > 7)
                {
                    return;
                }
                Piece temp = GetPieceViaPosition(mousePosition);
                Move move = new Move(selectedPiece, temp, mousePosition);
                PlayMove(true, move, possiblePlayerMoves, true);
                
            }
            else if (Input.GetMouseButtonDown(1)) //player deselects piece
            {
                DeselectPiece();
            }
        }
    }

    Vector2Int GetMousePosition() //returns mouse position
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int positionX = Mathf.CeilToInt(mousePosition.x) + 3;
        int positionY = Mathf.CeilToInt(mousePosition.y) + 3;
        return new Vector2Int(positionX, positionY);
    }

    void ColorTiles(List<Vector2Int> selectedTiles, bool isColored) //color tiles based on supplied Vector2Int
    {
        foreach (Vector2Int tile in selectedTiles)
        {
            if (!isColored)
            {
                SpriteRenderer sprRenderer = tiles[tile.y*8+tile.x].GetComponent<SpriteRenderer>();
                sprRenderer.sprite = Resources.Load<Sprite>("selectedTile");
            }
            else
            {
                SpriteRenderer sprRenderer = tiles[tile.y*8+tile.x].GetComponent<SpriteRenderer>();
                sprRenderer.sprite = (((tile.y*7+tile.x)%2)==0) ? Resources.Load<Sprite>("blackTile") : Resources.Load<Sprite>("whiteTile");
            }
        }
    }
    void DeselectPiece() //deselect players selected piece
    {
        Vector2Int mousePosition = GetMousePosition();
        if (mousePosition.x < 0 || mousePosition.x > 7 || mousePosition.y < 0 || mousePosition.y > 7)
        {
            return;
        }
        selectedPiece.gameObject.transform.position = new Vector3(selectedPiece.boardPosition.x*1f-3.5f, selectedPiece.boardPosition.y*1f-3.5f, -1f);
        ColorTiles(possiblePlayerMoves, true);
        selectedPiece = null;
    }

    Piece GetPieceViaPosition(Vector2Int position)
    {
        if (position.x < 0 || position.x > 7 || position.y < 0 || position.y > 7)
        {
            return null;
        }
        Piece temp = pieces[position.y * 8 + position.x];
        return temp;
    }
    void PlayMove(bool isPermanent, Move move, List<Vector2Int> possibleMoves, bool IsPlayersPurposefulMove) //plays move,
    {
        //checks if move can be played
        bool isPossibleMove = false;
        foreach(Vector2Int possibleMove in possibleMoves)
        {
            if (possibleMove==move.attackedPieceBP)
            {
                isPossibleMove = true;
                break;
            }
        }
        if (!isPossibleMove && IsPlayersPurposefulMove)
        {
            Debug.Log("exited");
            return;
        }

        Piece temp = move.attackedPiece;
        Piece attackingPiece = move.movingPiece;
        if (attackingPiece == null && isPermanent && temp == null)
        {
            Debug.Log("THIS ONLY HAPPENS IF A MOVE HAPPENS TO BE SKIPPED, WHICH SHOULD NEVER HAPPEN");
        }
        if (temp != null)
        {
            float pieceValue = BoardPieceValuation.pieceVal[temp.pieceType];
            if (temp.color == isPlayerWhite)
            {
                boardEvaluationForPlayer -= pieceValue;
            }
            else
            {
                boardEvaluationForPlayer += pieceValue;
            }
        }

        if (temp != null && isPermanent==true)
        {
            Destroy(temp.gameObject);
            temp = null;
        }
        
        if (!isPermanent)
        {
            undoMoves.Push(move);
        }
        else if (IsPlayersPurposefulMove)
        {
            //undo selected tiles;
            ColorTiles(possibleMoves, true);
        }
        attackingPiece.hasMoved = true;
        attackingPiece.amountMoved += 1;
        
        if (attackingPiece.pieceType == 5 && Mathf.Abs(move.movingPieceBP.x-move.attackedPieceBP.x) >1)
        {
            if (move.attackedPieceBP.x > move.movingPieceBP.x)
            {
                Piece rook = pieces[move.movingPieceBP.y * 8 + move.movingPieceBP.x + 3];
                rook.gameObject.transform.position = new Vector3(move.attackedPieceBP.x - 3.5f-1, move.attackedPieceBP.y - 3.5f, -1);
                //set index position
                pieces[move.attackedPieceBP.y * 8 + move.attackedPieceBP.x-1] = rook;//set index position
                pieces[move.movingPieceBP.y * 8 + move.movingPieceBP.x + 3] = null;
                //set board Position
                rook.boardPosition = new Vector2Int(move.attackedPieceBP.x-1, move.attackedPieceBP.y);
                rook.amountMoved +=1;
            }
            else if (move.attackedPieceBP.x < move.movingPieceBP.x)
            {
                Piece rook = pieces[move.movingPieceBP.y * 8 + move.movingPieceBP.x -4];
                rook.gameObject.transform.position = new Vector3(move.attackedPieceBP.x - 3.5f+1, move.attackedPieceBP.y - 3.5f, -1);
                //set index position
                pieces[move.attackedPieceBP.y * 8 + move.attackedPieceBP.x+1] = rook;//set index position
                pieces[move.movingPieceBP.y * 8 + move.movingPieceBP.x -4] = null;
                //set board Position
                rook.boardPosition = new Vector2Int(move.attackedPieceBP.x+1, move.attackedPieceBP.y);
                rook.amountMoved +=1;
            }
        }

        //set actual position
        attackingPiece.gameObject.transform.position = new Vector3(move.attackedPieceBP.x - 3.5f, move.attackedPieceBP.y - 3.5f, -1);//new Vector3(mousePosition.x-3.5f, mousePosition.y-3.5f, -1);
        //set index position
        pieces[move.attackedPieceBP.y * 8 + move.attackedPieceBP.x] = attackingPiece;//set index position
        pieces[move.movingPieceBP.y * 8 + move.movingPieceBP.x] = null;
        //set board Position
        attackingPiece.boardPosition = new Vector2Int(move.attackedPieceBP.x, move.attackedPieceBP.y);
        

        if (isPermanent && IsPlayersPurposefulMove)
        {
            selectedPiece = null;
            isPlayerTurn = !isPlayerTurn;
        }

        
    }

    float Negamax(int depth, bool color, float alpha, float beta)
    {
        float score = Mathf.NegativeInfinity;
        Move tempBestMove = new Move();
        if (depth == 0)
        {
            score = EvaluateBoard(color);

            return score;
        }
        List<Move> possibleMoves = GenerateAllMovesForColor(color);

        foreach(Move move in possibleMoves)
        {
            if (move.movingPiece == null)
            {
                continue;
            }
            float currentScore;
            List<Vector2Int> listOfMovesTemp = new List<Vector2Int>();
            listOfMovesTemp.Add(move.attackedPieceBP);
            PlayMove(false, move, listOfMovesTemp, false);
            currentScore = -Negamax(depth-1, !color, -beta, -alpha);
            if (currentScore > score)
            {
                score = currentScore;
                tempBestMove = move;
            }
            UndoPlayMove();
            if (currentScore >= beta)
            {
                bestNegamaxMove = tempBestMove;
                return beta;
            }
            if (score > alpha)
            {
                alpha = currentScore;
            }
        }
        bestNegamaxMove = tempBestMove;
        return alpha;
    }

    List<Move> GenerateAllMovesForColor(bool whichColor) //generates
    {
        //generate all moves
        List<Move> possibleMoves = new List<Move>();
        foreach (Piece piece in pieces)
        {
            if (piece == null || piece.color != whichColor)
            {
                continue;
            }
            List<Vector2Int> pieceMoves = piece.GenerateMoves(pieces);
            RemoveIllegalMoves(whichColor, pieceMoves, piece);
            foreach(Vector2Int move in pieceMoves)
            {
                Move newMove = new Move(piece, pieces[move.y * 8 + move.x], move);
                possibleMoves.Add(newMove);
            }
        }
        return possibleMoves;

    }

    bool IsCheck(bool colorToCheck) //checks inputted color is in check
    {
        List<Vector2Int> possibleEnemyMoves = new List<Vector2Int>();
        Vector2Int kingPosition = Vector2Int.zero;
        foreach (Piece piece in pieces)
        {
            if (piece==null)
            {
                continue;
            }
            else if (piece.color == colorToCheck)
            {
                if (piece.pieceType == 5) // if piece is a king
                {
                    kingPosition = piece.boardPosition;
                    continue;
                }
            }
            List<Vector2Int> pieceMoves = piece.GenerateMoves(pieces);
            foreach(Vector2Int move in pieceMoves)
            {
                possibleEnemyMoves.Add(move);
            }
        }
        foreach(Vector2Int move in possibleEnemyMoves)
        {
            if (kingPosition == move)
            {
                return true;
            }
            
        }
        return false;
    }

    bool HasPlayableMoves(bool colorToCheck) // check if this color has playable moves
    //if player in check and returns true they are checkmate
    {
        List<Vector2Int> possibleEnemyMoves = new List<Vector2Int>();
        Vector2Int kingPosition = Vector2Int.zero;
        foreach (Piece piece in pieces)
        {
            if (piece==null || piece.color != colorToCheck)
            {
                continue;
            }
            List<Vector2Int> pieceMoves = piece.GenerateMoves(pieces);
            RemoveIllegalMoves(colorToCheck, pieceMoves, piece);
            if (pieceMoves.Count != 0)
            {
                return true;
            }
        }
        return false;
    }
    void RemoveIllegalMoves(bool colorToCheck, List<Vector2Int> movesList, Piece attackingPiece)
    //removes moves that put player in check 
    {
        if (movesList.Count == 0)
        {
            return;
        }
        for (int i=movesList.Count-1; i>=0; i--)
        {
            Vector2Int moveVecInt = movesList[i];
            Piece temp = GetPieceViaPosition(moveVecInt);
            Move move = new Move(attackingPiece, temp, moveVecInt);
            PlayMove(false, move, movesList, false);
            if (IsCheck(colorToCheck))
            {
                movesList.RemoveAt(i);
            }
            UndoPlayMove();
        }
    }
    void UndoPlayMove()
    {
        Move move = undoMoves.Pop();
        
        Piece temp = move.attackedPiece;
        Piece attackingPiece = move.movingPiece;
        attackingPiece.amountMoved -= 1;
        if (attackingPiece.pieceType == 5 && Mathf.Abs(move.movingPieceBP.x-move.attackedPieceBP.x) > 1)
        {
            if (move.attackedPieceBP.x > move.movingPieceBP.x)
            {
                Piece rook = pieces[move.attackedPieceBP.y * 8 + move.attackedPieceBP.x - 1];
                rook.gameObject.transform.position = new Vector3(move.attackedPieceBP.x - 3.5f + 1, move.attackedPieceBP.y - 3.5f, -1);
                //set index position
                pieces[move.attackedPieceBP.y * 8 + move.attackedPieceBP.x - 1] = null;//set index position
                pieces[move.movingPieceBP.y * 8 + move.movingPieceBP.x + 3] = rook;
                //set board Position
                rook.boardPosition = new Vector2Int(move.movingPieceBP.x + 3, move.movingPieceBP.y);
                rook.amountMoved -= 1;
            }
            else if (move.attackedPieceBP.x < move.movingPieceBP.x)
            {
                Piece rook = pieces[move.attackedPieceBP.y * 8 + move.attackedPieceBP.x + 1];
                rook.gameObject.transform.position = new Vector3(move.attackedPieceBP.x - 3.5f - 2, move.attackedPieceBP.y - 3.5f, -1);
                //set index position
                pieces[move.attackedPieceBP.y * 8 + move.attackedPieceBP.x + 1] = null;//set index position
                pieces[move.movingPieceBP.y * 8 + move.movingPieceBP.x - 4] = rook;
                //set board Position
                rook.boardPosition = new Vector2Int(move.movingPieceBP.x - 4, move.movingPieceBP.y);
                rook.amountMoved -= 1;
            }
        }
        attackingPiece.gameObject.transform.position = new Vector3(move.movingPieceBP.x - 3.5f, move.movingPieceBP.y - 3.5f, -1);//new Vector3(mousePosition.x-3.5f, mousePosition.y-3.5f, -1);
        //set index position
        pieces[move.movingPieceBP.y * 8 + move.movingPieceBP.x] = attackingPiece;//set index position
        pieces[move.attackedPieceBP.y * 8 + move.attackedPieceBP.x] = temp;
        //set board Position
        attackingPiece.boardPosition = new Vector2Int(move.movingPieceBP.x, move.movingPieceBP.y);
        if (attackingPiece.boardPosition.y == attackingPiece.startingBoardPosition.y)
        {
            attackingPiece.hasMoved = false;
        }
        if (temp != null)
        {
            //set actual position
            temp.gameObject.transform.position = new Vector3(move.attackedPieceBP.x-3.5f,
                move.attackedPieceBP.y-3.5f, -1);
            
        }


        if (temp != null)
        {
            float pieceValue = -BoardPieceValuation.pieceVal[temp.pieceType];
            if (temp.color == isPlayerWhite)
            {
                boardEvaluationForPlayer -= pieceValue;
            }
            else
            {
                boardEvaluationForPlayer += pieceValue;
            }
        }
    }

    float EvaluateBoard(bool color)
    {
        if (color != isPlayerWhite)
        {
            return -boardEvaluationForPlayer - PieceSquareEvaluation()/100f;
        }
        return boardEvaluationForPlayer + PieceSquareEvaluation()/100f;
    }

    float PieceSquareEvaluation() //positive is white
    {
        float evaluation=0;
        for (int y=0; y<8; y++)
        {
            for (int x=0; x<8; x++)
            {
                Piece piece = pieces[y*8+x];
                if (piece != null)
                {
                    int pType = piece.pieceType;
                    float scoreFromTable = 0;
                    if (piece.color == true)
                    {
                        scoreFromTable = BoardPieceValuation.pieceTableWhite[pType, y*8+x];
                        if (isPlayerWhite)
                        {
                            evaluation += scoreFromTable;
                        }
                        else
                        {
                            evaluation -= scoreFromTable;
                        }
                    }
                    else
                    {
                        scoreFromTable = BoardPieceValuation.pieceTableWhite[pType, (7-y)*8+(7-x)];
                        if (!isPlayerWhite)
                        {
                            evaluation += scoreFromTable;
                        }
                        else
                        {
                            evaluation -= scoreFromTable;
                        }
                    }
                }
                continue;
            }
        }
        return evaluation;
    }
}


struct Move
{
    public Piece movingPiece;
    public Vector2Int movingPieceBP; //boardPosition
    
    public Piece attackedPiece;
    public Vector2Int attackedPieceBP;//board Position
    public Move(Piece movingPiece, Piece attackedPiece, Vector2Int attackedPieceBP)
    {
        this.movingPiece = movingPiece;
        this.attackedPiece = attackedPiece;
        movingPieceBP = movingPiece.boardPosition;
        if (!attackedPiece)
        {
            this.attackedPieceBP = attackedPieceBP;
        }
        else
        {
            this.attackedPieceBP = attackedPiece.boardPosition;
        }
    }
}
