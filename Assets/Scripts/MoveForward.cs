using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Very basic move forward script
public class MoveForward : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;

	// Update is called once per frame
	void Update()
    {
        transform.position += transform.right * speed;
	}
}
