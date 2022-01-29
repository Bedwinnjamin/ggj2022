using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterManager : MonoBehaviour
{
    public TextMesh textMesh;
    public Color LetterColor;

    private static string[] possibleLetters = {"X", "O"};

    // Start is called before the first frame update
    void Start()
    {
        textMesh.color = LetterColor;
        textMesh.text = possibleLetters[Random.Range(0,2)];
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
    }

    public void OfferClaim()
    {
        Debug.Log("Can claim this spot!");
        textMesh.color = new Color();
    }
}
