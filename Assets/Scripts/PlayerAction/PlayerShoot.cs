using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawns an object when player presses the fire button
public class PlayerShoot : SpawnObject
{
    [Tooltip("If true, player has to release button before firing again")]
    [SerializeField] bool oneShot = false;
    private bool hasShot = false;

	// Update is called once per frame
	void Update ()
    {
        CheckFire();
	}   // Update

    // Check for player input for shooting
    void CheckFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (oneShot == true && hasShot == true)
            {
                return;
            }
            Spawn();
            hasShot = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            hasShot = false;
        }
    }   // CheckFire
}
