using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterManager : MonoBehaviour
{
    //public TextMesh textMesh;
    public Color LetterColor;

    public GameObject space = null;

    public GameObject letterSprite;
    public SpriteRenderer letterRenderer;

    public Sprite blueX;
    public Sprite blueO;
    public Sprite redX;
    public Sprite redO;

    public bool playerIsRed;  //TODO: Set this automatically

    private Color originalColor;

    private string currentLetter;

    public int insideSquareCount;

    private static string[] possibleLetters = { "X", "O" };

    // Start is called before the first frame update
    void Start()
    {
        letterRenderer = letterSprite.GetComponent<SpriteRenderer>();

        //textMesh.color = LetterColor;
        currentLetter = possibleLetters[Random.Range(0, 2)];
        Debug.Log(currentLetter);
        SwitchSprite();
    }

    void SwitchLetter()
    {
        currentLetter = (currentLetter == "X") ? "O" : "X";
        Debug.Log(currentLetter);
        SwitchSprite();
        //textMesh.text = (textMesh.text == "X") ? "O" : "X";
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
            if (Input.GetKeyDown("f") && insideSquareCount > 0)
            {
                if (space.GetComponent<SpaceScript>().isFree())
                {
                    int playerID = (currentLetter == "X") ? 1 : 2;

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
        //textMesh.color = Color.yellow;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "space")
        {
            space = other.gameObject;
            insideSquareCount++;

            //TODO: Highlight the square that you are occupying
            //TODO: Remove your highlight from other squares
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "space")
        {
            //textMesh.color = originalColor;
            Debug.Log("Leaving Square");
            insideSquareCount--;
        }
    }
}
