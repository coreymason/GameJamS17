using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 1.0f;
    [SerializeField] private float gravityScale = 1.0f;

    private Rigidbody2D rb2d;

    // Use this for initialization
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetButtonDown("Jump"))
        {

        }
	}
}
