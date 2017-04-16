using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMove : MonoBehaviour
{
    [SerializeField]
    float amplitude = 0.06f;
    float i;

	// Update is called once per frame
	void Update ()
    {
        i += amplitude;
        transform.localPosition += new Vector3( Mathf.Sin(i) * 0.04f, 0, 0);
	}
}
