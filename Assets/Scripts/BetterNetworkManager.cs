using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BetterNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<SpawnCharacterMessage>(OnCreateCharacter);
    }

    public override void OnClientConnect(NetworkConnection conn) {
        base.OnClientConnect(conn);

        if (numPlayers == 1)
        {
            conn.Send(
                new SpawnCharacterMessage
                {
                    isRed = false
                }
            );
        } 
        else
        {
            conn.Send(
                new SpawnCharacterMessage
                {
                    isRed = true
                }
            );
        }
    }

    void OnCreateCharacter(NetworkConnection conn, SpawnCharacterMessage message)
    {
        GameObject gameObject = Instantiate(playerPrefab);

        LetterManager lm = gameObject.GetComponent<LetterManager>();
        lm.playerIsRed = message.isRed;

        NetworkServer.AddPlayerForConnection(conn, gameObject);
    }

    public struct SpawnCharacterMessage : NetworkMessage
    {
        public bool isRed;
    }
}
