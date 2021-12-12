using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{ 
    public GameObject zoom;
    public GameObject square;
    public GameObject text;
    public GameObject textSquare;
    // Start is called before the first frame update
   
    void Start()
    {
        zoom.SetActive(false);
        square.SetActive(false);
        text.SetActive(false);
        textSquare.SetActive(false);
    }
    public void OnMouseOver()
    {
        GameManager gm = GameManager.current;
        if (gm.toolTipsEnabled)
        {
            mouseOverToolTip();
        }
    }

    public void OnMouseExit()
    {
        zoom.SetActive(false);
        square.SetActive(false);
        text.SetActive(false);
        textSquare.SetActive(false);
    }

    public void mouseOverToolTip()
    {
        
        GameManager gm = GameManager.current;
        Vector2Int mousePosition = gm.GetMousePosition();
        if (mousePosition.x < 0 || mousePosition.x > 23 || mousePosition.y < 0 || mousePosition.y > 23)
        {
            return;
        }
        if (gm.pieces[(mousePosition.y * 24) + mousePosition.x] == null || gm.pieces[(mousePosition.y * 24) + mousePosition.x].pieceType == 0)
        {
            return;
        }
        zoom.SetActive(true);
        square.SetActive(true);
        text.SetActive(true);
        textSquare.SetActive(true);
        zoom.GetComponent<SpriteRenderer>().sprite = gm.pieces[(mousePosition.y * 24) + mousePosition.x].getPieceSprite(gm.pieces[(mousePosition.y * 24) + mousePosition.x].pieceType, gm.isPlayerWhite, zoom.GetComponent<SpriteRenderer>());
        zoom.GetComponent<SpriteRenderer>().transform.localScale = new Vector2(3.5f, 3.5f);
        text.GetComponent<Text>().text = gm.pieces[(mousePosition.y * 24) + mousePosition.x].getPieceText(gm.pieces[(mousePosition.y * 24) + mousePosition.x].pieceType, gm.isPlayerWhite);
        if (mousePosition.y > 12)
        {
            zoom.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(1f, -1f, 1f);
            square.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(1f, -1f, 2f);
            text.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(1f, -2f, 1f);
            textSquare.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(1f, -2f, 1f);
        }
        else
        {
            zoom.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(1f, 1f, 1f);
            square.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(1f, 1f, 2f);
            text.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(1f, 2f, 1f);
            textSquare.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(1f, 2f, 1f);
        }

    }
}
