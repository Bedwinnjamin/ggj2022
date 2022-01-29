using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterManager : MonoBehaviour
{
    public TextMesh textMesh;
    public Color LetterColor;

    public bool insideSpace = false;
    public GameObject space = null;

    private Color originalColor;

    private static string[] possibleLetters = {"X", "O"};

    // Start is called before the first frame update
    void Start()
    {
        textMesh.color = LetterColor;
        textMesh.text = possibleLetters[Random.Range(0,2)];
        originalColor = LetterColor;
    }

    void SwitchLetter()
    {
        textMesh.text = (textMesh.text == "X") ? "O" : "X";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SwitchLetter();
        }
        if (Input.GetKeyDown("f") && insideSpace)
        {
            if (space.GetComponent<SpaceScript>().isFree())
            {
                int playerID = (textMesh.text == "X") ? 1 : 2;

                space.GetComponent<SpaceScript>().ClaimSquare(playerID);

                Debug.Log("Placed Your Letter!");

                //TODO: Respawn the player
            }
        }
    }

    public void OfferClaim()
    {
        //TODO: Show that the space is claimable
        Debug.Log("Can claim this spot!");
        textMesh.color = Color.yellow;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "space")
        {
            insideSpace = true;
            space = other.gameObject;
            Debug.Log("Entering Square");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "space")
        {
            insideSpace = false;
            // Do we need to clear the space here?
            textMesh.color = originalColor;
            Debug.Log("Leaving Square");
        }
    }
}
