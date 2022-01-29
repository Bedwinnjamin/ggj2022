using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    
    public Animator animator;

    Vector2 movement;

    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            
            
            animator.SetFloat("Speed", movement.sqrMagnitude);
            if(movement.x < 0)
            {
            	animator.SetBool("Left", true);
            }
            else
            {
            	animator.SetBool("Left", false);
            }
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
