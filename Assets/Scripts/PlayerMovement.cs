using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    public int leafBlowerStrength;
    Vector2 leafBlowerDelta = Vector3.zero;
    private float footTime = 0;

    Vector2 movement;

    [SyncVar(hook = (nameof(UpdateSpriteRenderer)))]
    bool isLeft;
    void UpdateSpriteRenderer(bool old, bool val)
    {
        sr.flipX = val;
    }
    [Command]
    void SetIsLeft(bool value)
    {
        isLeft = value;
    }

    void Start()
    {
        animator = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
    }

    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Speed", movement.sqrMagnitude);
            if (movement.sqrMagnitude > 0)
            {
                playFootStep();
            }
            if (movement.x < 0)
            {
                SetIsLeft(true);
            }
            else if (movement.x > 0)
            {
                SetIsLeft(false);
            }
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
        rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime) + leafBlowerDelta);
    }

    void playFootStep()
    {
        if (SfxManager.sfxInstance && (Time.time - footTime) >= SfxManager.sfxInstance.Walk.length + .08)
        {
            SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Walk, .6f);
            footTime = Time.time;
        }
    }

    [TargetRpc]
    public void GetBlown(NetworkConnection conn)
    {
        Debug.Log("\"Don't blow me!\"");

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player != gameObject)
            {
                Debug.Log("I'm setting it now");
                Vector2 direction = gameObject.transform.position - player.transform.position;
                Debug.Log(direction * leafBlowerStrength);
                StartCoroutine(setLeafBlowerDelta(direction * leafBlowerStrength / 100));
            }
        }
    }

    IEnumerator setLeafBlowerDelta(Vector2 lbDir)
    {
        leafBlowerDelta = lbDir;
        Debug.Log(leafBlowerDelta);

        yield return new WaitForSeconds(1);

        leafBlowerDelta = Vector2.zero;
    }
}
