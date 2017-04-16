using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Follow an object with dampening
public class FollowObject : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    
	// Update is called once per frame
	void Update ()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }   // Update
}
