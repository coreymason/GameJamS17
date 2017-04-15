using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    [Tooltip("Maximum Horizontal running speed")]
    [SerializeField] float maxSpeed = 7;
    [Tooltip("How much force the jump has")]
    [SerializeField] float jumpTakeOffSpeed = 7f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

	// Use this for initialization
	void Awake ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
	}   // Awake
	
	protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp ("Jump"))    // Cancel jump in midair
        {
            // If currently rising, cut velocity by half
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        // If move.x > 0.01f, flip the sprite
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0f) : (move.x < 0f));
        if (flipSprite == true)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }   // Compute Velocity
}
