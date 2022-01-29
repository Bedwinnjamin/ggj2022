using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TTTGameScript : NetworkBehavior
{
    [SyncVar]
    private int[,] tttBoard = new int[3, 3] {
        {0,0,0},
        {0,0,0},
        {0,0,0}
    };

    public void ClaimSquare(int x, int y, int playerID)
    {
        tttBoard[x, y] = playerID;

        CheckForWin();
    }

    void CheckForWin()
    {
        //TODO: Actually serve the win

        //Horizontal Wins
        for (int i = 0; i < 3; i++)
        {
            if (tttBoard[i, 0] == tttBoard[i, 1] && tttBoard[i, 1] == tttBoard[i, 2] && tttBoard[i, 0] != 0)
            {
                Debug.Log(tttBoard[i, 0] + "Wins!");
                ResetGame();
            }
        }

        //Vertical Wins
        for (int i = 0; i < 3; i++)
        {
            if (tttBoard[0, i] == tttBoard[1, i] && tttBoard[1, i] == tttBoard[2, i] && tttBoard[0, i] != 0)
            {
                Debug.Log(tttBoard[0, i] + "Wins!");
                ResetGame();
            }
        }

        //Diagonal Wins
        if (tttBoard[0, 0] == tttBoard[1, 1] && tttBoard[1, 1] == tttBoard[2, 2] && tttBoard[0, 0] != 0)
        {
            Debug.Log(tttBoard[0, 0] + "Wins!");
            ResetGame();
        }
        else if (tttBoard[0, 2] == tttBoard[1, 1] && tttBoard[1, 1] == tttBoard[2, 0] && tttBoard[0, 2] != 0)
        {
            Debug.Log(tttBoard[0, 2] + "Wins!");
            ResetGame();
        }
    }

    bool CheckForDraw()
    {
        bool spaceRemaining = false;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (tttBoard[i, j] == 0)
                {
                    spaceRemaining = true;
                }
            }
        }

        return !spaceRemaining;
    }

    void ResetGame()
    {
        StartCoroutine(DelayedReset());
    }

    IEnumerator DelayedReset()
    {
        yield return new WaitForSeconds(2);

        GameObject[] squares = GameObject.FindGameObjectsWithTag("space");

        foreach (GameObject square in squares)
        {
            square.GetComponent<SpaceScript>().ResetSquare();
        }

        tttBoard = new int[3, 3] {
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };
    }
}
