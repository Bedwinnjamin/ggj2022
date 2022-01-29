using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceScript : MonoBehaviour
{
    public int x;
    public int y;

    bool free = true;

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(x.ToString() + "," + y.ToString());
        if (free)
        {
            collider.gameObject.GetComponent<LetterManager>().OfferClaim();
        }
    }

    public void ClaimSquare(int playerID)
    {
        //TODO: Change sprite
        free = false;
        GameObject.Find("TTTGrid").GetComponent<TTTGameScript>().ClaimSquare(x, y, playerID);
    }

    public bool isFree()
    {
        return free;
    }

    public void ResetSquare() 
    {
        //TODO: Change sprite
        free = true;
    }
}
