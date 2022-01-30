using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LeafBlower : NetworkBehaviour
{
    string leafblowerKey = "r";
    private bool leafBlowerEnabled;
    [SyncVar(hook = nameof(HandleLeafBlowerEnabled))]
    bool LeafBlowerEnabled = false;

    void HandleLeafBlowerEnabled(bool prevEnabled, bool enabled)
    {
        if (enabled)
        {
            //TODO: enable leaf blower sprite, animation, sound
        }
        else
        {
            // todo: disable animation
        }
    }

    void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(leafblowerKey))
        {
            print("blowing");
            LeafBlowerEnabled = true;
            // RaycastHit hit;
            Physics2D.Raycast(transform.position, Vector2.up);
            Debug.DrawRay(transform.position, Vector2.up, new Color(255, 0, 0), 1f, false);
        }
    }
}
