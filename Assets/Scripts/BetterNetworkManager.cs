using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BetterNetworkManager : NetworkManager
{
    private int playersAdded = 0;
    private int startPosIndex = 0;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        playersAdded += 1;
        GameObject gameObject = Instantiate(playerPrefab);
        Player lm = gameObject.GetComponent<Player>();
        lm.playerIsRed = playersAdded > 1;
        print($"new client, we now have {playersAdded} players");
        print(conn.connectionId);
        gameObject.transform.position = NetworkManager.startPositions[startPosIndex].position;
        startPosIndex = (startPosIndex + 1) % 2;
        NetworkServer.AddPlayerForConnection(conn, gameObject);
    }
}