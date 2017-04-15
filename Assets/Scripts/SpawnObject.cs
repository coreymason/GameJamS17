using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn an object
public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;

	// Spawn an object
	public void Spawn()
    {
        EZ_Pooling.EZ_PoolManager.Spawn(objectToSpawn.transform, transform.position, transform.rotation);
	}   // Spawn
}
