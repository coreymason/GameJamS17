using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Shake
public class Shake : MonoBehaviour
{
    [Tooltip("How fast it shakes")]
    [SerializeField]
    float speed = 1.0f;
    [Tooltip("How much it shakes")]
    [SerializeField]
    float amount = 1.0f;

	// Update is called once per frame
	void Update ()
    {
        //transform.position.x = Mathf.Sin(Time.time * speed);
	}   // Update
}
