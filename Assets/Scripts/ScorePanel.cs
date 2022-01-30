using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    public Text redScore;
    public Text blueScore;
    public TTTGameScript gameManager;
    void Update()
    {
        redScore.text = gameManager.redScore.ToString();
        blueScore.text = gameManager.blueScore.ToString();
    }
}
