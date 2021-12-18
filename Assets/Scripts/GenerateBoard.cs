using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    public Sprite blackTile;
    public Sprite whiteTile;
    public GameObject zoom;
    public GameObject square;
    public GameObject text;
    public GameObject textSquare;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateTiles();
        GeneratePieces();
    }

    public void GenerateTiles() //Generates board tiles
    {
        GameObject board = new GameObject("Board tiles");
        board.transform.parent = transform;

        for (int y=0; y<24; y++)
        {
            for (int x=0; x<24; x++)
            {
                SpriteRenderer newPiece = new GameObject("tile " + (y * 24 + x)).AddComponent<SpriteRenderer>();
                newPiece.tag = "tiles";
                newPiece.transform.parent = board.transform;
                newPiece.sprite = (((y + x) % 2) == 0) ? blackTile : whiteTile;
                newPiece.transform.localScale = 4.8f * newPiece.transform.localScale;
                newPiece.transform.localPosition = new Vector3(x - 12.7f, y - 11.7f, 1);
                GameManager.current.tiles[y * 24 + x] = newPiece.gameObject;
            }
        }
        board.transform.localScale = board.transform.localScale * 0.4f;
    }

    public string getFen()
    {
        if (InputFen.customFen != null && InputFen.customFen.Length > 0)
        {
            return InputFen.customFen;
        } else
        {
            return FenUtility.startFen;
        }
    }

    public void GeneratePieces()
    {
        GameObject board = new GameObject("Board Pieces");
        board.tag = "board pieces";
       
        board.transform.parent = transform;
        for (int y=0; y<24; y++)
        {
            for (int x=0; x<24; x++)
            {
                Piece piece = new GameObject("piece").AddComponent<Piece>();
                piece.tag = "pieces";
                piece.colorMath(getFen(), new Vector2(x, y));
                piece.pieceTypeInit(getFen(), new Vector2(x,y));
                piece.getPieceSprites();
                piece.transform.parent = board.transform;
                piece.transform.localPosition = new Vector3(x * 0.4f- 5.1f, y * 0.4f - 4.7f, -1);
                GameManager.current.pieces[y * 24 + x] = piece;
            }
        }
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("pieces");
        foreach (GameObject thing in pieces)
        {
            thing.AddComponent<BoxCollider2D>();
            thing.AddComponent<Zoom>();
            thing.GetComponent<BoxCollider2D>().size = new Vector2(0.4f, 0.4f);
            thing.GetComponent<BoxCollider2D>().offset = new Vector2(0.01f, 0.02f);
            thing.transform.localScale = new Vector2(0.9f, 0.9f);
            thing.GetComponent<Zoom>().zoom = zoom;
            thing.GetComponent<Zoom>().square = square;
            thing.GetComponent<Zoom>().text = text;
            thing.GetComponent<Zoom>().textSquare = textSquare;
        }
    }
}
