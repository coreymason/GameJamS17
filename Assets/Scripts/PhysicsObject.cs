using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move objects downward due to gravity
public class PhysicsObject : MonoBehaviour
{
    [SerializeField] float gravityModifier = 1f;

    protected Rigidbody2D rb2d;
    protected Vector2 velocity;

    protected const float minMoveDistance = 0.001f;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }   // OnEnable



	// Use this for initialization
	void Start () {
		
	}
	


	// Update is called once per frame
	void Update ()
    {
		
	}



    void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 move = Vector2.up * deltaPosition.y;

        Movement(move);
    }   // FixedUpdate



    // Move the object
    void Movement(Vector2 move)
    {
        float distance = move.magnitude;

        rb2d.position = rb2d.position + move;
    }   // Movement
}
