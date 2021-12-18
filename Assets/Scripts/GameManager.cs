using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using static System.Math;
using System;

public class GameManager : MonoBehaviour
{
    public enum Result { Playing, WhiteIsMated, BlackIsMated, Stalemate, FiftyMoveRule, InsufficientMaterial }
    public static GameManager current;
    public Piece []pieces;
    public GameObject []tiles;
    public Piece lastSeletedPiece;
    Piece selectedPiece;
    List<Vector2Int> possiblePlayerMoves;
    bool isPlayerTurn;
    public bool isPlayerWhite = true; //0 is white
    public bool isAiVsMode; // true = ai vs ai
    public bool isPlayerVsPlayer;
    Stack<Move> undoMoves;
    Move bestNegamaxMove;
    float boardEvaluationForPlayer = 0;
    bool booleanHasChange = false;
    int currentIterativeSearchDepth;
    bool abortSearch;
    private static int immediateMateScore = 100000;
    private float bestEval;
    public GameObject canvas;
    public bool isPlayerAI;
    private Move lastNegamaxMove;
    Result gameResult;
    private Piece lastMovedPiece;
    public int fiftyMoveCounter;
    public bool isPlayerIsBlackMode;
    public bool toolTipsEnabled;
    public int plyCount = 1;
    public int[] Square;

    private event System.Action<Move> onSearchComplete;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        pieces = new Piece[576];
        tiles = new GameObject[576];
        isPlayerTurn = isPlayerWhite;
        undoMoves = new Stack<Move>();
        Square = new int[576];
    }

    public void toolTipsEnable()
    {
         toolTipsEnabled = true;   
    }
    public void toolTipsDisable()
    {
        toolTipsEnabled = false;
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayerVsPlayer(bool playerVsPlayer)
    {
        if(playerVsPlayer)
        {
            isAiVsMode = false;
            isPlayerVsPlayer = true;
            isPlayerWhite = true;
            booleanHasChange = true;
            isPlayerIsBlackMode = false;
            GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = false;
        }
    }

    public void AIvsAI(bool isAiVsAiOn)
    {
        if (isAiVsAiOn)
        {
            isAiVsMode = true;
            isPlayerVsPlayer = false;
            booleanHasChange = true;
            isPlayerIsBlackMode = false;
            GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = false;
        }
    }

    public void PlayerPlaysWhite(bool playerWhite)
    {
        if (playerWhite)
        {
            isPlayerWhite = true;
            isAiVsMode = false;
            isPlayerVsPlayer = false;
            booleanHasChange = true;
            isPlayerIsBlackMode = false;
            GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = false;
        }
    }

    public void PlayerPlaysBlack(bool playerBlack)
    {
        if (playerBlack)
        {
            isPlayerWhite = false;
            isAiVsMode = false;
            isPlayerVsPlayer = false;
            booleanHasChange = true;
            isPlayerIsBlackMode = true;
            GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = false;
        }
    }

    public string getColor()
    {
        if(isPlayerWhite)
        {
            return "white";
        }
        else
        {
            return "black";
        }
    }

    Result GetGameState(bool color)
    {
        var possibleMoves = GenerateAllMovesForColor(color);

        if (possibleMoves.Count == 0 || bestNegamaxMove.Equals(possibleMoves[UnityEngine.Random.Range(0, 0)]))
        {
            // Look for mate/stalemate
            if (IsCheck(color))
            {
                return color ? Result.WhiteIsMated : Result.BlackIsMated;
            }
            return Result.Stalemate;
        }

        int colorIndex = color ? 0 : 1; 


        // Fifty move rule
        if (fiftyMoveCounter >= 100)
        {
            return Result.FiftyMoveRule;
        }

        return Result.Playing;
    }

    public void printGameResult(Result result)
    {
        if (!isPlayerVsPlayer)
        {
            if (isAiVsMode)
            {
                if (isPlayerTurn)
                {
                    if (Result.WhiteIsMated == result || Result.BlackIsMated == result)
                    {
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = true;
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().text = getColor() + " AI is the winner";
                        Debug.Log("Game Over");
                    }
                    else if (Result.FiftyMoveRule == result|| Result.Stalemate == result)
                    {
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = true;
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().text = "Ai vs AI stalemate";
                        Debug.Log("Game Over");
                    }
                }
                else
                {
                    if (Result.WhiteIsMated == result || Result.BlackIsMated == result)
                    {
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = true;
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().text = getColor() + " AI is the winner";
                        Debug.Log("Game Over");
                    }
                    else if (Result.FiftyMoveRule == result || Result.Stalemate == result)
                    {
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = true;
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().text = "Ai vs AI stalemate";
                        Debug.Log("Game Over");
                    }
                }
            }
            else
            {
                if (isPlayerTurn)
                {
                    if (Result.WhiteIsMated == result || Result.BlackIsMated == result)
                    {
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = true;
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().text = getColor() + " player is the winner";
                        Debug.Log("Game Over");
                    }
                    else if (Result.FiftyMoveRule == result || Result.Stalemate == result)
                    {
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = true;
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().text = "player vs AI stalemate";
                        Debug.Log("Game Over");
                    }
                }
                else
                {
                    if (Result.WhiteIsMated == result || Result.BlackIsMated == result)
                    {
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = true;
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().text = getColor() + " AI is the winner";
                        Debug.Log("Game Over");
                    }
                    else if (Result.FiftyMoveRule == result || Result.Stalemate == result)
                    {
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = true;
                        GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().text = "player vs AI stalemate";
                        Debug.Log("Game Over");

                    }
                }
            }
        }
        else
        {
            if (isPlayerTurn)
            {
                if (Result.WhiteIsMated == result || Result.BlackIsMated == result)
                {
                    GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = true;
                    GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().text = getColor() + " player is the winner";
                    Debug.Log("Game Over");
                    
                }
                else if (Result.FiftyMoveRule == result || Result.Stalemate == result)
                {
                    GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = true;
                    GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().text = "player vs player stalemate";
                    Debug.Log("Game Over");
                }
            }
            else
            {
                if (Result.WhiteIsMated == result || Result.BlackIsMated == result)
                {
                    GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = true;
                    GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().text = getColor() + " player is the winner";
                    Debug.Log("Game Over");
                }
                else if (Result.FiftyMoveRule == result || Result.Stalemate == result)
                {
                    GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().enabled = true;
                    GameObject.FindGameObjectWithTag("Win Text").GetComponent<Text>().text = "player vs player stalemate";
                    Debug.Log("Game Over");
                }
            }
        }
    }

    public bool isGameOver(Result result)
    {
        if(Result.FiftyMoveRule == result || Result.InsufficientMaterial == result || Result.Stalemate == result || Result.WhiteIsMated == result || Result.BlackIsMated == result)
        {
            return true;
        } else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameResult = GetGameState(isPlayerWhite);
        if (booleanHasChange)
        {
            GameObject[] pieces = GameObject.FindGameObjectsWithTag("pieces");
            for (int i = 0; i < pieces.Length; i++)
            {
                Destroy(pieces[i]);
            }
            GameObject boardPiece = GameObject.FindGameObjectWithTag("board pieces");
            Destroy(boardPiece);
            GameObject board = GameObject.FindGameObjectWithTag("Board");
            GenerateBoard gb = board.GetComponent<GenerateBoard>();
            gb.GeneratePieces();
            booleanHasChange = false;

        }

        if (!isPlayerVsPlayer)
        {
            if (isAiVsMode)
            {
                if (isPlayerTurn)
                {
                    isPlayerAI = true;
                    AIMove(isPlayerWhite);
                    printGameResult(gameResult);
                    //PlayerMove(!isPlayerWhite);
                    isPlayerTurn = !isPlayerTurn;
                }
                else
                {
                    isPlayerAI = true;
                    AIMove(!isPlayerWhite);
                    printGameResult(gameResult);
                    //PlayerMove(!isPlayerWhite);
                    isPlayerTurn = !isPlayerTurn;
                    plyCount++;
                }
            }
            else
            {
                if (isPlayerTurn)
                {
                    printGameResult(gameResult);
                    isPlayerAI = false;
                    PlayerMove(isPlayerWhite);
                }
                else
                {
                    printGameResult(gameResult);
                    isPlayerAI = true;
                    AIMove(!isPlayerWhite);
                    //PlayerMove(!isPlayerWhite);
                    isPlayerTurn = !isPlayerTurn;
                    plyCount++;
                }
            }
        }
        else
        {
            if (isPlayerTurn)
            {
                printGameResult(gameResult);
                isPlayerAI = false;
                PlayerMove(isPlayerWhite);
            }
            else
            {
                printGameResult(gameResult);
                isPlayerAI = false;
                PlayerMove(!isPlayerWhite);
                plyCount++;
            }
        }
    }

    public void promotePawnToQueen()
    {
        Vector2Int mousePosition = GetMousePosition();

        if (mousePosition.x < 0 || mousePosition.x > 24 || mousePosition.y < 0 || mousePosition.y > 24)
        {
            return;
        }
        
        if(lastSeletedPiece == null)
        {
            return;
        }
        if (lastSeletedPiece.pieceType == 1 && (lastSeletedPiece.boardPosition.y == 0 || lastSeletedPiece.boardPosition.y == 23))
        {
            SpriteRenderer sprRenderer = lastSeletedPiece.GetComponent<SpriteRenderer>();
            if ((bool) lastSeletedPiece.color)
            {
                sprRenderer.sprite = Resources.Load<Sprite>("queen 1");
                lastSeletedPiece.pieceType = 6;
            }
            else
            {
                sprRenderer.sprite = Resources.Load<Sprite>("queen");
                lastSeletedPiece.pieceType = 6;
            }

        }
    }

    public void promotePawnToKnight()
    {
        Vector2Int mousePosition = GetMousePosition();
        if (mousePosition.x < 0 || mousePosition.x > 24 || mousePosition.y < 0 || mousePosition.y > 24)
        {
            return;
        }
        if (lastSeletedPiece == null)
        {
            return;
        }
        if (lastSeletedPiece.pieceType == 1 && (lastSeletedPiece.boardPosition.y == 0 || lastSeletedPiece.boardPosition.y == 23))
        {
            SpriteRenderer sprRenderer = lastSeletedPiece.GetComponent<SpriteRenderer>();
            if ((bool)lastSeletedPiece.color)
            {
                sprRenderer.sprite = Resources.Load<Sprite>("knight 1");
                lastSeletedPiece.pieceType = 3;
            }
            else
            {
                sprRenderer.sprite = Resources.Load<Sprite>("knight");
                lastSeletedPiece.pieceType = 3;
            }

        }
    }

    public void promotePawnToBishop()
    {
        Vector2Int mousePosition = GetMousePosition();
        if (mousePosition.x < 0 || mousePosition.x > 24 || mousePosition.y < 0 || mousePosition.y > 24)
        {
            return;
        }
        if (lastSeletedPiece == null)
        {
            return;
        }
        if (lastSeletedPiece.pieceType == 1 && (lastSeletedPiece.boardPosition.y == 0 || lastSeletedPiece.boardPosition.y == 23)) {
                SpriteRenderer sprRenderer = lastSeletedPiece.GetComponent<SpriteRenderer>();
                if ((bool)lastSeletedPiece.color)
                {
                    sprRenderer.sprite = Resources.Load<Sprite>("bishop 1");
                    lastSeletedPiece.pieceType = 4;
                }
                else
                {
                    sprRenderer.sprite = Resources.Load<Sprite>("bishop");
                    lastSeletedPiece.pieceType = 4;
                }
            
        }
    }
    public void promotePawnToRook()
    {
        Vector2Int mousePosition = GetMousePosition();
        if (mousePosition.x < 0 || mousePosition.x > 24 || mousePosition.y < 0 || mousePosition.y > 24)
        {
            return;
        }
        if (lastSeletedPiece == null)
        {
            return;
        }
        if (lastSeletedPiece.pieceType == 1 && (lastSeletedPiece.boardPosition.y == 0 || lastSeletedPiece.boardPosition.y == 23)) {
            SpriteRenderer sprRenderer = lastSeletedPiece.GetComponent<SpriteRenderer>();
            if ((bool)lastSeletedPiece.color)
            {
                sprRenderer.sprite = Resources.Load<Sprite>("rook 1");
                lastSeletedPiece.pieceType = 2;
            }
            else
            {
                sprRenderer.sprite = Resources.Load<Sprite>("rook");
                lastSeletedPiece.pieceType = 2;
            }
        }
    }

    public void promotePawnToQueenAI()
    {
        Vector2Int mousePosition = GetMousePosition();



        if (mousePosition.x < 0 || mousePosition.x > 24 || mousePosition.y < 0 || mousePosition.y > 24)
        {
            return;
        }

        Piece piece = lastMovedPiece;

        if (lastMovedPiece.pieceType == 1 && (lastMovedPiece.boardPosition.y == 0 || lastMovedPiece.boardPosition.y == 23))
        {
            SpriteRenderer sprRenderer = lastMovedPiece.GetComponent<SpriteRenderer>();
            if ((bool) lastMovedPiece.color)
            {
                sprRenderer.sprite = Resources.Load<Sprite>("queen 1");
                lastMovedPiece.pieceType = 6;
            }
            else
            {
                sprRenderer.sprite = Resources.Load<Sprite>("queen");
                lastMovedPiece.pieceType = 6;
            }
        }
    }

    public void promotePawnToBishopAI()
    {
        Vector2Int mousePosition = GetMousePosition();


        if (mousePosition.x < 0 || mousePosition.x > 24 || mousePosition.y < 0 || mousePosition.y > 24)
        {
            return;
        }

        Piece piece = lastMovedPiece;

        if (lastMovedPiece.pieceType == 1 && (lastMovedPiece.boardPosition.y == 0 || lastMovedPiece.boardPosition.y == 23))
        {
            SpriteRenderer sprRenderer = lastMovedPiece.GetComponent<SpriteRenderer>();
            if ((bool)lastMovedPiece.color)
            {
                sprRenderer.sprite = Resources.Load<Sprite>("bishop 1");
                lastMovedPiece.pieceType = 4;
            }
            else
            {
                sprRenderer.sprite = Resources.Load<Sprite>("bishop");
                lastMovedPiece.pieceType = 4;
            }
        }
    }


    public void promotePawnToRookAI()
    {
        Vector2Int mousePosition = GetMousePosition();

        if (mousePosition.x < 0 || mousePosition.x > 24 || mousePosition.y < 0 || mousePosition.y > 24)
        {
            return;
        }

        Piece piece = lastMovedPiece;

        if (lastMovedPiece.pieceType == 1 && (lastMovedPiece.boardPosition.y == 0 || lastMovedPiece.boardPosition.y == 23))
        {
            SpriteRenderer sprRenderer = lastMovedPiece.GetComponent<SpriteRenderer>();
            if ((bool)lastMovedPiece.color)
            {
                sprRenderer.sprite = Resources.Load<Sprite>("rook 1");
                lastMovedPiece.pieceType = 2;
            }
            else
            {
                sprRenderer.sprite = Resources.Load<Sprite>("rook");
                lastMovedPiece.pieceType = 2;
            }
        }
    }

    public void promotePawnToKnightAI()
    {
        Vector2Int mousePosition = GetMousePosition();

        if (mousePosition.x < 0 || mousePosition.x > 24 || mousePosition.y < 0 || mousePosition.y > 24)
        {
            return;
        }

        Piece piece = lastMovedPiece;

        if (lastMovedPiece.pieceType == 1 && (lastMovedPiece.boardPosition.y == 0 || lastMovedPiece.boardPosition.y == 23))
        {
            SpriteRenderer sprRenderer = lastMovedPiece.GetComponent<SpriteRenderer>();
            if (lastMovedPiece.color)
            {
                sprRenderer.sprite = Resources.Load<Sprite>("knight 1");
                lastMovedPiece.pieceType = 3;
            }
            else
            {
                sprRenderer.sprite = Resources.Load<Sprite>("knight");
                lastMovedPiece.pieceType = 3;
            }
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
        int numberOfTurns = 0; 
        if ((numberOfTurns % 3) >= 0)
        {
            if (aiColor)
            {
                if ((numberOfTurns % 3) == 0)
                {
                    RandomMoveGenerator(aiColor, 0);
                    List<Vector2Int> listOfMovesTemp = new List<Vector2Int>();
                    listOfMovesTemp.Add(bestNegamaxMove.attackedPieceBP);
                    PlayMove(true, bestNegamaxMove, listOfMovesTemp, false);
                    fiftyMoveCounter++;
                    numberOfTurns++;
                }
                else
                {
                    StartSearch(aiColor);
                    List<Vector2Int> listOfMovesTemp = new List<Vector2Int>();
                    listOfMovesTemp.Add(bestNegamaxMove.attackedPieceBP);
                    PlayMove(true, bestNegamaxMove, listOfMovesTemp, false);
                    fiftyMoveCounter++;
                    numberOfTurns++;

                }
            }
            else
            {
                if ((numberOfTurns % 3) == 0)
                {
                    RandomMoveGenerator(aiColor, 0);
                    List<Vector2Int> listOfMovesTemp = new List<Vector2Int>();
                    listOfMovesTemp.Add(bestNegamaxMove.attackedPieceBP);
                    PlayMove(true, bestNegamaxMove, listOfMovesTemp, false);
                    fiftyMoveCounter++;
                    numberOfTurns++;

                }
                else
                {
                    StartSearch(aiColor);
                    List<Vector2Int> listOfMovesTemp = new List<Vector2Int>();
                    listOfMovesTemp.Add(bestNegamaxMove.attackedPieceBP);
                    PlayMove(true, bestNegamaxMove, listOfMovesTemp, false);
                    fiftyMoveCounter++;
                    numberOfTurns++;
                }
            }
        }
    }

    void PickUpPiece(bool whoseTurn) //pickup piece player
    {
        if (selectedPiece == null)
        {
            if (Input.GetMouseButtonDown(0)) //player selects piece
            {
                Vector2Int mousePosition = GetMousePosition();
                if (mousePosition.x < 0 || mousePosition.x > 23 || mousePosition.y < 0 || mousePosition.y > 23)
                {
                    return;
                }
                selectedPiece = pieces[mousePosition.y * 24 + mousePosition.x];
                if (selectedPiece != null)
                {
                    if (selectedPiece.color != whoseTurn)
                    {
                        lastSeletedPiece = selectedPiece;
                        selectedPiece = null;
                        return;
                    }
                    possiblePlayerMoves = selectedPiece.GenerateMoves(pieces);
                    RemoveIllegalMoves(whoseTurn, possiblePlayerMoves, selectedPiece);
                    if(possiblePlayerMoves.Count ==0)
                    {
                        lastSeletedPiece = selectedPiece;
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
                if (mousePosition.x < 0 || mousePosition.x > 23 || mousePosition.y < 0 || mousePosition.y > 23)
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

    public Vector2Int GetMousePosition() //returns mouse position
    {
        Vector3 mousePosition =  new Vector3((Camera.main.ScreenToWorldPoint(Input.mousePosition).x * 2.5f) + 13.5f, (Camera.main.ScreenToWorldPoint(Input.mousePosition).y * 2.5f) + 12.5f, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
        int positionX = (int)mousePosition.x; 
        int positionY = (int)mousePosition.y;
        return new Vector2Int(positionX, positionY);
    }

    void ColorTiles(List<Vector2Int> selectedTiles, bool isColored) //color tiles based on supplied Vector2Int
    {
        foreach (Vector2Int tile in selectedTiles)
        {
            if (!isColored)
            {
                SpriteRenderer sprRenderer = tiles[tile.y*24+tile.x].GetComponent<SpriteRenderer>();
                sprRenderer.color = new Color(0, 76, 255, 0.5f);
            }
            else
            {
                SpriteRenderer sprRenderer = tiles[tile.y*24+tile.x].GetComponent<SpriteRenderer>();
                sprRenderer.sprite = ((((tile.y)+tile.x)%2)==0) ? Resources.Load<Sprite>("blackTile") : Resources.Load<Sprite>("whiteTile");
                sprRenderer.color = (((tile.y) + tile.x) % 2) == 0 ? new Color(1,1,1,1) : new Color(1, 1, 1, 1);
            }
        }
    }
    void DeselectPiece() //deselect players selected piece
    {
        Vector2Int mousePosition = GetMousePosition();
        if (mousePosition.x < 0 || mousePosition.x > 23 || mousePosition.y < 0 || mousePosition.y > 23)
        {
            return;
        }
        selectedPiece.gameObject.transform.position = 0.4f * new Vector3(selectedPiece.boardPosition.x*1f-12.7f, selectedPiece.boardPosition.y*1f-11.7f, -1f);
        ColorTiles(possiblePlayerMoves, true);
        lastSeletedPiece = selectedPiece;
        selectedPiece = null;
    }

    Piece GetPieceViaPosition(Vector2Int position)
    {
        if (position.x < 0 || position.x > 23 || position.y < 0 || position.y > 23)
        {
            return null;
        }
        Piece temp = pieces[position.y * 24 + position.x];
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
        Piece captured = move.attackedPiece;
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

        if (captured != null)
        {
            if (attackingPiece.pieceType == 1 || captured.pieceType != 0)
            {
                fiftyMoveCounter = 0;
            }
        }

        if (attackingPiece.pieceType == 5 && Mathf.Abs(move.movingPieceBP.x - move.attackedPieceBP.x) > 1)
        {
            if (move.attackedPieceBP.x > move.movingPieceBP.x)
            {
                Piece rook = pieces[move.movingPieceBP.y * 24 + move.movingPieceBP.x + 3];
                rook.gameObject.transform.position = 0.4f * new Vector3(move.attackedPieceBP.x - 12.7f - 1, move.attackedPieceBP.y - 11.7f, -1);
                //set index position
                pieces[move.attackedPieceBP.y * 24 + move.attackedPieceBP.x - 1] = rook;//set index position
                pieces[move.movingPieceBP.y * 24 + move.movingPieceBP.x + 3] = null;
                //set board Position
                rook.boardPosition = new Vector2Int(move.attackedPieceBP.x - 1, move.attackedPieceBP.y);
                rook.amountMoved += 1;
            }
            else if (move.attackedPieceBP.x < move.movingPieceBP.x)
            {
                Piece rook = pieces[move.movingPieceBP.y * 24 + move.movingPieceBP.x - 4];
                rook.gameObject.transform.position = 0.4f * new Vector3(move.attackedPieceBP.x - 12.7f + 1, move.attackedPieceBP.y - 11.7f, -1);
                //set index position
                pieces[move.attackedPieceBP.y * 24 + move.attackedPieceBP.x + 1] = rook;//set index position
                pieces[move.movingPieceBP.y * 24 + move.movingPieceBP.x - 4] = null;
                //set board Position
                rook.boardPosition = new Vector2Int(move.attackedPieceBP.x + 1, move.attackedPieceBP.y);
                rook.amountMoved += 1;
            }
        }

        //set actual position
        attackingPiece.gameObject.transform.position = 0.4f * new Vector3(move.attackedPieceBP.x - 12.7f, move.attackedPieceBP.y - 11.7f, -1);
        //set index position
        pieces[move.attackedPieceBP.y * 24 + move.attackedPieceBP.x] = attackingPiece;//set index position
        pieces[move.movingPieceBP.y * 24 + move.movingPieceBP.x] = null;
        //set board Position
        attackingPiece.boardPosition = new Vector2Int(move.attackedPieceBP.x, move.attackedPieceBP.y);
        lastMovedPiece = attackingPiece;

        if (isPermanent && IsPlayersPurposefulMove)
        {
            lastSeletedPiece = selectedPiece;
            selectedPiece = null;
            fiftyMoveCounter++;
            isPlayerTurn = !isPlayerTurn;
        }
    }

    public void StartSearch(bool color)
    {
        Move bestMove = new Move();
        List<Move> possibleMoves = GenerateAllMovesForColor(color);
        // Initialize search settings
        float bestEvalThisIteration = 0.0f;
        Move bestMoveThisIteration = bestMove;

        currentIterativeSearchDepth = 0;
        abortSearch = false;

        // Iterative deepening. This means doing a full search with a depth of 1, then with a depth of 2, and so on.
        // This allows the search to be aborted at any time, while still yielding a useful result from the last search.

        int targetDepth = int.MaxValue;

        for (int searchDepth = 1; searchDepth <= targetDepth; searchDepth++)
        {   
            SearchMoves(1, 0, color, -999999999, 999999999);
            if (abortSearch)
            {
                break;
            }
            else
            {
                currentIterativeSearchDepth = searchDepth;
                bestMove = bestMoveThisIteration;
                bestEval = bestEvalThisIteration;

                // Exit search if found a mate
                if (IsMateScore((int) bestEval))
                {
                    break;
                }
            }
        }
        onSearchComplete?.Invoke(bestMove);
    }

    public int RandomMoveGenerator(bool color, int plyFromRoot)
    {
        List<Move> possibleMoves = GenerateAllMovesForColor(color);
        if (possibleMoves.Count == 0)
        {
            if (IsCheck(color))
            {
                int mateScore = immediateMateScore - plyFromRoot;
                bestNegamaxMove = possibleMoves[UnityEngine.Random.Range(0, possibleMoves.Count)];
                return -mateScore;
            }
            else
            {
                return 0;
            }
        } else
        {
            plyFromRoot = plyFromRoot + 1;
            bestNegamaxMove = possibleMoves[UnityEngine.Random.Range(0, possibleMoves.Count)];
            return UnityEngine.Random.Range(0, possibleMoves.Count);
        }
    }

    public static bool IsMateScore(int score)
    {
        const int maxMateDepth = 1000;
        return System.Math.Abs(score) > immediateMateScore - maxMateDepth;
    }

    int SearchMoves(int depth, int plyFromRoot, bool color, int alpha, int beta)
    {
        float score = Mathf.NegativeInfinity;
        if (depth != 0)
        {
            Move tempBestMove = new Move();
            int eval;
            List<Move> possibleMoves = GenerateAllMovesForColor(color);
            if (abortSearch)
            {
                return 0;
            }

            if (plyFromRoot > 0)
            {
                // Skip this position if a mating sequence has already been found earlier in
                // the search, which would be shorter than any mate we could find from here.
                // This is done by observing that alpha can't possibly be worse (and likewise
                // beta can't  possibly be better) than being mated in the current position.
                alpha = Max(alpha, -immediateMateScore + plyFromRoot);
                beta = Min(beta, immediateMateScore - plyFromRoot);
                if (alpha >= beta)
                {
                    return alpha;
                }
            }

            // Detect checkmate and stalemate when no legal moves are available
            if (possibleMoves.Count == 0)
            {
                if (IsCheck(color))
                {
                    int mateScore = immediateMateScore - plyFromRoot;
                    bestNegamaxMove = possibleMoves[UnityEngine.Random.Range(0, possibleMoves.Count)];
                    return -mateScore;
                }
                else
                {
                    return 0;
                }
            }

            foreach (Move move in possibleMoves)
            {
                List<Vector2Int> listOfMovesTemp = new List<Vector2Int>();
                listOfMovesTemp.Add(move.attackedPieceBP);
                PlayMove(false, move, listOfMovesTemp, false);
                eval = -SearchMoves(depth - 1, plyFromRoot + 1, !color, -beta, -alpha);
                if (eval > score)
                {
                    score = eval;
                    tempBestMove = move;
                }
                UndoPlayMove();
                // Move was *too* good, so opponent won't allow this position to be reached
                // (by choosing a different move earlier on). Skip remaining moves.
                if (eval >= beta)
                {
                    bestNegamaxMove = tempBestMove;
                    return beta;
                }

                // Found a new best move in this position
                if (eval > alpha)
                {
                    alpha = eval;
                }
            }
            bestNegamaxMove = tempBestMove;
            return alpha;
        }
        else
        {
            score = Negamax(depth, color, alpha, beta);
            lastNegamaxMove = bestNegamaxMove;
            return (int)score;
        }
    }

    float Negamax(int depth, bool color, float alpha, float beta)
    {
        float score = Mathf.NegativeInfinity;
        if (depth == 0)
        {
            score = EvaluateBoard(color);

            return score;
        }
        List<Move> possibleMoves = GenerateAllMovesForColor(color);

        foreach (Move move in possibleMoves)
        {
            if (move.movingPiece == null)
            {
                continue;
            }
            float currentScore;
            List<Vector2Int> listOfMovesTemp = new List<Vector2Int>();
            listOfMovesTemp.Add(move.attackedPieceBP);
            PlayMove(false, move, listOfMovesTemp, false);
            currentScore = -Negamax(depth - 1, !color, -beta, -alpha);
            if (currentScore > score)
            {
                score = currentScore;
            }
            UndoPlayMove();
            if (currentScore >= beta)
            {
                return beta;
            }
            if (score > alpha)
            {
                alpha = currentScore;
            }
        }
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
                Move newMove = new Move(piece, pieces[move.y * 24 + move.x], move);
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
            } else
            {
                return false;
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
                Piece rook = pieces[move.attackedPieceBP.y * 24 + move.attackedPieceBP.x - 1];
                rook.gameObject.transform.position = 0.4f * new Vector3(move.attackedPieceBP.x - 12.7f + 1, move.attackedPieceBP.y - 11.7f, -1);
                //set index position
                pieces[move.attackedPieceBP.y * 24 + move.attackedPieceBP.x - 1] = null;//set index position
                pieces[move.movingPieceBP.y * 24 + move.movingPieceBP.x + 3] = rook;
                //set board Position
                rook.boardPosition = new Vector2Int(move.movingPieceBP.x + 3, move.movingPieceBP.y);
                rook.amountMoved -= 1;
            }
            else if (move.attackedPieceBP.x < move.movingPieceBP.x)
            {
                Piece rook = pieces[move.attackedPieceBP.y * 24 + move.attackedPieceBP.x + 1];
                rook.gameObject.transform.position = 0.4f * new Vector3(move.attackedPieceBP.x - 12.7f - 2, move.attackedPieceBP.y - 11.7f, -1);
                //set index position
                pieces[move.attackedPieceBP.y * 24 + move.attackedPieceBP.x + 1] = null;//set index position
                pieces[move.movingPieceBP.y * 24 + move.movingPieceBP.x - 4] = rook;
                //set board Position
                rook.boardPosition = new Vector2Int(move.movingPieceBP.x - 4, move.movingPieceBP.y);
                rook.amountMoved -= 1;
            }
        }
        attackingPiece.gameObject.transform.position = 0.4f * new Vector3(move.movingPieceBP.x - 12.7f, move.movingPieceBP.y - 11.7f, -1);
        //set index position
        pieces[move.movingPieceBP.y * 24 + move.movingPieceBP.x] = attackingPiece;//set index position
        pieces[move.attackedPieceBP.y * 24 + move.attackedPieceBP.x] = temp;
        //set board Position
        attackingPiece.boardPosition = new Vector2Int(move.movingPieceBP.x, move.movingPieceBP.y);
        if (attackingPiece.boardPosition.y == attackingPiece.startingBoardPosition.y)
        {
            attackingPiece.hasMoved = false;
        }
        if (temp != null)
        {
            //set actual position
            temp.gameObject.transform.position = 0.4f * new Vector3(move.attackedPieceBP.x-12.7f,
                move.attackedPieceBP.y-11.7f, -1);
            
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

    public float PieceSquareEvaluation() //positive is white
    {
        float evaluation=0;
        for (int y=0; y<24; y++)
        {
            for (int x=0; x<24; x++)
            {
                Piece piece = pieces[y*24+x];
                if (piece != null)
                {
                    int pType = piece.pieceType;
                    float scoreFromTable = 0;
                    if (piece.color == true)
                    {
                        scoreFromTable = BoardPieceValuation.pieceTableWhite[pType, y*24+x];
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
                        scoreFromTable = BoardPieceValuation.pieceTableWhite[pType, (23-y)*24+(23-x)];
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
