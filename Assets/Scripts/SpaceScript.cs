using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpaceScript : MonoBehaviour
{
    public int x;
    public int y;

    public Sprite xSprite;
    public Sprite oSprite;
    
    public bool isFree {
        get {
            return state == 0;
        }
    }
    
    private int state = 0;
    public int State {
        get { return state; }
        set {
            state = value;
            switch (state) {
                case 0:
                    gameObject.GetComponent<SpriteRenderer>().sprite = null;
                    break;
                case 1:
                    gameObject.GetComponent<SpriteRenderer>().sprite = xSprite;
                    break;
                case 2:
                    gameObject.GetComponent<SpriteRenderer>().sprite = oSprite;
                    break;
            }
        }
    }

    private void Start() {
        State = 0;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isFree)
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
        GameObject.Find("TTTGameManager").GetComponent<TTTGameScript>().ClaimSquare(x, y, playerID);
    }

    public void ResetSquare() 
    {
        State = 0;
    }
}
