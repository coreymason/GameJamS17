using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Recyle an object and place it back in the object pool
public class RecycleObject : MonoBehaviour
{
    public static void Recycle(GameObject objectToDespawn)
    {
        EZ_Pooling.EZ_PoolManager.Despawn(objectToDespawn.transform);
    }
}
