using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    public ChunkGenerator cg;
    public GameObject[] GMs = new GameObject[14];
    public Transform generationPoint;

    private int prevEndHeight;

    // Use this for initialization
    void Start () {
        int prevEndHeight = cg.MakeLevel("First", 1, 10, 0, GMs);
	}
	
	// Update is called once per frame
	void Update () {
        /*if(transform.position.x > generationPoint.position.x ) {
            generationPoint.position = new Vector3();
            prevEndHeight = cg.MakeLevel("First", 0, prevEndHeight, 0, GMs);
        }
        if() {

        }*/
    }
}
