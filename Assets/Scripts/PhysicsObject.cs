using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move objects downward due to gravity
public class PhysicsObject : MonoBehaviour
{
    [SerializeField] float minGroundNormalY = 0.65f;
    [SerializeField] float gravityModifier = 1f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;      // Modify distance only if distance is smaller than shell size, which could cause in getting stuck in a wall

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }   // OnEnable



	// Use this for initialization
	void Start ()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
	}   // Start
	


	// Update is called once per frame
	void Update ()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
	}   // Update



    protected virtual void ComputeVelocity()
    {   

    }   // ComputeVelocity



    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }   // FixedUpdate



    // Move the object
    void Movement(Vector2 move, bool yMovement)
    {   // yMovement is used only for movement on the y-axis
        float distance = move.magnitude;

        // If the collider will overlap in the next frame
        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;

                // Determine if player is grounded if colliding
                if(currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    // If moving along the y-axis
                    if (yMovement == true)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    // Cancel out velocity that would be stopped by collision
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }   // Movement
}
