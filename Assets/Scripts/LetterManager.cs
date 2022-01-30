using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LetterManager : NetworkBehaviour
{
    private GameObject space;
    public GameObject letterSprite;
    private SpriteRenderer letterRenderer;
    public RuntimeAnimatorController blueDino;
    public RuntimeAnimatorController redDino;
    public Sprite blueX;
    public Sprite blueO;
    public Sprite redX;
    public Sprite redO;

    [SyncVar]
    public string currentLetter;

    [SyncVar]
    public bool playerIsRed;

    private static string[] possibleLetters = { "X", "O" };

    void Start()
    {
        letterRenderer = letterSprite.GetComponent<SpriteRenderer>();

        Animator animator = this.GetComponent<Animator>();

        if (playerIsRed)
        {
            playerIsRed = true;
            animator.runtimeAnimatorController = redDino as RuntimeAnimatorController;
        }
        else
        {
            playerIsRed = false;
            animator.runtimeAnimatorController = blueDino as RuntimeAnimatorController;
        }

        currentLetter = possibleLetters[Random.Range(0, 2)];
        Debug.Log(currentLetter);
        SwitchSprite();
    }

    void SwitchLetter()
    {
        currentLetter = (currentLetter == "X") ? "O" : "X";
        Debug.Log(currentLetter);
        SwitchSprite();
    }

    void SwitchSprite()
    {
        if (playerIsRed)
        {
            if (currentLetter == "X")
            {
                letterRenderer.sprite = redX;
            }
            else
            {
                letterRenderer.sprite = redO;
            }
        }
        else
        {
            if (currentLetter == "X")
            {
                letterRenderer.sprite = blueX;
            }
            else
            {
                letterRenderer.sprite = blueO;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown("space"))
            {
                SwitchLetter();
            }
            if (Input.GetKeyDown("f") && space != null)
            {
                if (space.GetComponent<SpaceScript>().isFree)
                {
                    int playerID = (currentLetter == "X") ? 1 : 2;

                    Debug.Log("Placing Letter");

                    space.GetComponent<SpaceScript>().ClaimSquare(playerID);

                    Debug.Log("Placed Your Letter!");

                    //TODO: Respawn the player
                }
            }
        }
    }

    public void OfferClaim()
    {
        //TODO: Show that the space is claimable
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "space")
        {
            space = other.gameObject;

            //TODO: Highlight the square that you are occupying
            //TODO: Remove your highlight from other squares
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == space)
        {
            Debug.Log("Leaving Square");
            space = null;
        }
    }
}
