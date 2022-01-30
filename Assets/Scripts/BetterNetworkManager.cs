using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BetterNetworkManager : NetworkManager
{
    public int playersAdded = 0;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        playersAdded += 1;
        GameObject gameObject = Instantiate(playerPrefab);
        LetterManager lm = gameObject.GetComponent<LetterManager>();
        lm.playerIsRed = playersAdded > 1;
        Animator animator = this.GetComponent<Animator>();
        print($"new client, we now have {playersAdded} players");
        NetworkServer.AddPlayerForConnection(conn, gameObject);
    }
}