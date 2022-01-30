using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TTTGameScript : NetworkBehaviour
{

    private readonly SyncList<int> tttBoard = new SyncList<int>();

    public List<GameObject> tttObjects = new List<GameObject>();

    [SyncVar]
    public int redScore = 0;
    [SyncVar]
    public int blueScore = 0;

    private void resetBoard()
    {
        tttBoard.Clear();
        for (int i = 0; i < 9; i++)
        {
            tttBoard.Add(0);
        }
    }

    public override void OnStartClient()
    {
        tttBoard.Callback += OnBoardUpdated;
    }

    void OnBoardUpdated(SyncList<int>.Operation op, int index, int oldVal, int newVal)
    {
        switch (op)
        {
            case SyncList<int>.Operation.OP_ADD:
            case SyncList<int>.Operation.OP_SET:
                // index is of the item that was changed
                // oldItem is the previous value for the item at the index
                // newItem is the new value for the item at the index
                tttObjects[index].GetComponent<SpaceScript>().State = newVal;
                break;
        }
    }

    public TTTGameScript()
    {
        resetBoard();
    }

    private int flattenCoords(int x, int y)
    {
        return y * 3 + x;
    }

    [Command(requiresAuthority = false)]
    public void ClaimSquare(int x, int y, int marking, Player player)
    {
        tttBoard[flattenCoords(x, y)] = marking;

        if (CheckForWin())
        {
            // give player a point
            if (player.playerIsRed)
            {
                redScore += 1;
            }
            else
            {
                blueScore += 1;
            }
            ResetGame();
        }
        else if (CheckForDraw())
        {
            ResetGame();
        }
    }

    bool CheckForWin()
    {
        //TODO: Actually serve the win

        //Horizontal Wins
        for (int i = 0; i < 3; i++)
        {
            if (tttBoard[flattenCoords(i, 0)] == tttBoard[flattenCoords(i, 1)] && tttBoard[flattenCoords(i, 1)] == tttBoard[flattenCoords(i, 2)] && tttBoard[flattenCoords(i, 0)] != 0)
            {
                return true;
            }
        }

        //Vertical Wins
        for (int i = 0; i < 3; i++)
        {
            if (tttBoard[flattenCoords(0, i)] == tttBoard[flattenCoords(1, i)] && tttBoard[flattenCoords(1, i)] == tttBoard[flattenCoords(2, i)] && tttBoard[flattenCoords(0, i)] != 0)
            {
                return true;
            }
        }

        //Diagonal Wins
        if (tttBoard[flattenCoords(0, 0)] == tttBoard[flattenCoords(1, 1)] && tttBoard[flattenCoords(1, 1)] == tttBoard[flattenCoords(2, 2)] && tttBoard[flattenCoords(0, 0)] != 0)
        {
            return true;
        }
        else if (tttBoard[flattenCoords(0, 2)] == tttBoard[flattenCoords(1, 1)] && tttBoard[flattenCoords(1, 1)] == tttBoard[flattenCoords(2, 0)] && tttBoard[flattenCoords(0, 2)] != 0)
        {
            return true;
        }
        return false;
    }

    bool CheckForDraw()
    {
        bool spaceRemaining = false;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (tttBoard[flattenCoords(i, j)] == 0)
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

        resetBoard();
    }
}
