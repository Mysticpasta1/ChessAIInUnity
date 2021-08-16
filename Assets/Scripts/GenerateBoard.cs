using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoard : MonoBehaviour
{
    public Sprite blackTile;
    public Sprite whiteTile;

    // Start is called before the first frame update
    void Start()
    {
        GenerateTiles();
        GeneratePieces();
    }

    void GenerateTiles() //Generates board tiles
    {
        GameObject board = new GameObject("Board tiles");
        board.transform.parent = transform;

        for (int y=0; y<8; y++)
        {
            for (int x=0; x<8; x++)
            {
                SpriteRenderer newPiece = new GameObject("tile " + (y*8+x)).AddComponent<SpriteRenderer>();
                newPiece.transform.parent = board.transform;
                newPiece.sprite = (((y+x)%2)==0) ? blackTile : whiteTile;
                newPiece.transform.localScale = .2f*newPiece.transform.localScale;
                newPiece.transform.localPosition = new Vector3(x*1f-3.5f, y*1f-3.5f, 1);
                GameManager.current.tiles[y*8+x] = newPiece.gameObject;
            }
        }
    }

    void GeneratePieces()
    {
        GameObject board = new GameObject("Board Pieces");
        board.transform.parent = transform;
        for (int y=0; y<8; y++)
        {
            for (int x=0; x<8; x++)
            {
                if (y > 1 && y < 6)
                {
                    //add piece as null to gamemanager pieces
                    GameManager.current.pieces[y*8+x] = null;
                    continue;
                }
                
                Piece piece = new GameObject("piece").AddComponent<Piece>();
                piece.Initialize(new Vector2(x, y));
                piece.transform.parent = board.transform;
                piece.transform.localScale = 1.2f*piece.transform.localScale;
                piece.transform.localPosition = new Vector3(x*1f-3.5f, y*1f-3.5f, -1);
                GameManager.current.pieces[y*8+x] = piece;
            }
        }
    }
}
