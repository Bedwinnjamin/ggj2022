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
    public float leafBlowerCooldown;
    float timeSinceLastBlow;
    private float blowTime = 0;


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
        if (isLocalPlayer && Input.GetKeyDown(leafblowerKey) && timeSinceLastBlow > leafBlowerCooldown)
        {
            Debug.Log("Blow Bro Activated!");
            LeafBlowerEnabled = true;

            timeSinceLastBlow = 0.0f;

            Vector2 pointingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            RaycastHit2D rayHit = Physics2D.Raycast(transform.position, pointingDirection, 5);
            Debug.DrawRay(transform.position, pointingDirection * 5, Color.white, 100.0f);

            if (rayHit.collider != null && rayHit.rigidbody.gameObject.tag == "Player" && rayHit.rigidbody.gameObject != this.gameObject)
            {
                Debug.Log("Player Hit!");
                NetworkIdentity opponentIdentity = rayHit.rigidbody.gameObject.GetComponent<NetworkIdentity>();
                BlowOtherPlayer(rayHit.rigidbody.gameObject);
            }
        }
    }

    void FixedUpdate() {
        timeSinceLastBlow += Time.fixedDeltaTime;
    }

    [Command]
    void BlowOtherPlayer(GameObject target)
    {
        NetworkIdentity opponentIdentity = target.GetComponent<NetworkIdentity>();
        target.GetComponent<PlayerMovement>().GetBlown(opponentIdentity.connectionToClient);
    }

    void playLeafBlow()
    {
        if (SfxManager.sfxInstance && (Time.time - blowTime) >= SfxManager.sfxInstance.Blow.length + .08)
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Blow, .6f);
            blowTime = Time.time;
        }
    }
}
