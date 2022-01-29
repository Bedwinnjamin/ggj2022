using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceScript : MonoBehaviour
{
    public int x;
    public int y;

    public Sprite xSprite;
    public Sprite oSprite;

    bool free = true;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (free)
        {
            Debug.Log(x.ToString() + "," + y.ToString() + " [CAN CLAIM]");
            collider.gameObject.GetComponent<LetterManager>().OfferClaim();
        }
        else
        {
            Debug.Log(x.ToString() + "," + y.ToString());

        }
    }

    public void ClaimSquare(int playerID)
    {
        //TODO: Change sprite
        free = false;

        if (playerID == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = xSprite;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = oSprite;

        }

        GameObject.Find("TTTGrid").GetComponent<TTTGameScript>().ClaimSquare(x, y, playerID);
    }

    public bool isFree()
    {
        return free;
    }

    public void ResetSquare() 
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        free = true;
    }
}
