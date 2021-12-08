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

        for (int y=0; y<24; y++)
        {
            for (int x=0; x<24; x++)
            {
                SpriteRenderer newPiece = new GameObject("tile " + (y * 24 + x)).AddComponent<SpriteRenderer>();
                newPiece.transform.parent = board.transform;
                newPiece.sprite = (((y + x) % 2) == 0) ? blackTile : whiteTile;
                newPiece.transform.localScale = 4.8f * newPiece.transform.localScale;
                newPiece.transform.localPosition = new Vector3(x - 12.7f, y - 11.7f, 1);
                GameManager.current.tiles[y * 24 + x] = newPiece.gameObject;
            }
        }
        board.transform.localScale = board.transform.localScale * 0.4f;
    }

    void GeneratePieces()
    {
        GameObject board = new GameObject("Board Pieces");
        board.transform.parent = transform;
        for (int y=0; y<24; y++)
        {
            for (int x=0; x<24; x++)
            {
                if (y > 2 && y < 21)
                {
                    //add piece as null to gamemanager pieces
                    GameManager.current.pieces[y*1+x] = null;
                    continue;
                }
                
                Piece piece = new GameObject("piece").AddComponent<Piece>();
                piece.Initialize(new Vector2(x, y));
                piece.transform.parent = board.transform;
                piece.transform.localPosition = new Vector3(x * 0.4f- 5.1f, y * 0.4f - 4.7f, -1);
                GameManager.current.pieces[y*24+x] = piece;
            }
        }
    }
}
