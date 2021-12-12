using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{ 
    public GameObject zoom;
    public GameObject square;
    // Start is called before the first frame update
   
    void Start()
    {
        zoom.SetActive(false);
        square.SetActive(false);
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
        zoom.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(1f, 1f, 1f);
        square.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(1f, 1f, 2f);
        zoom.GetComponent<SpriteRenderer>().sprite = gm.pieces[(mousePosition.y * 24) + mousePosition.x].getPieceSprite(gm.pieces[(mousePosition.y * 24) + mousePosition.x].pieceType, gm.isPlayerWhite, zoom.GetComponent<SpriteRenderer>());
    }
}
