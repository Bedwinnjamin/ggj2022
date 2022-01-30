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

    Vector2 movement;

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
            if(movement.x < 0)
            {
            	sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
