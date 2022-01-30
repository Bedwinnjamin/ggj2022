using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
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

    [SyncVar(hook = nameof(handleLetterChanged))]
    public string currentLetter;
    void handleLetterChanged(string old, string value)
    {
        SwitchSprite();
    }
    [Command]
    void setCurrentLetter(string value)
    {
        currentLetter = value;
    }

    [SyncVar]
    public bool playerIsRed;

    private static string[] possibleLetters = { "X", "O" };

    public override void OnStartClient()
    {
        letterRenderer = letterSprite.GetComponent<SpriteRenderer>();

        Animator animator = this.GetComponent<Animator>();

        if (playerIsRed)
        {
            animator.runtimeAnimatorController = redDino as RuntimeAnimatorController;
        }
        else
        {
            animator.runtimeAnimatorController = blueDino as RuntimeAnimatorController;
        }

        if (isLocalPlayer)
        {
            setCurrentLetter(possibleLetters[Random.Range(0, 2)]);
        }
        SwitchSprite();
    }

    [TargetRpc]
    public void Respawn(Vector3 position)
    {
        transform.position = position;
        setCurrentLetter(possibleLetters[Random.Range(0, 2)]);
    }

    public void SwitchLetter()
    {
        setCurrentLetter((currentLetter == "X") ? "O" : "X");

        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Change, .7f);
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
            if (Input.GetKeyDown("space") && space != null)
            {
                if (space.GetComponent<SpaceScript>().isFree)
                {
                    space.GetComponent<SpaceScript>().ClaimSquare(currentLetter, this);

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
