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
    public int LeafBlowerDistance;

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
            Debug.Log("Blow Bro Activated!");
            LeafBlowerEnabled = true;

            Vector2 pointingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, pointingDirection, 5);
            Debug.DrawRay(transform.position, pointingDirection * 5, Color.white, 100.0f);

            if (rayHit.collider != null && rayHit.rigidbody.gameObject.tag == "Player" && rayHit.rigidbody.gameObject != this.gameObject)
            {
                Debug.Log("Player Hit!");
                rayHit.rigidbody.gameObject.GetComponent<PlayerMovement>().GetBlown();
            }
        }
    }
}
