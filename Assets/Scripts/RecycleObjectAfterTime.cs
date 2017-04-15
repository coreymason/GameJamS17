using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Recycle an object after some time
public class RecycleObjectAfterTime : RecycleObject
{
    [SerializeField] float timeToRecycle = 10.0f;

    private void OnEnable()
    {
        Invoke("CallRecycle", timeToRecycle);
    }   // OnEnable

    void CallRecycle()
    {
        Recycle(gameObject);
    }
}
