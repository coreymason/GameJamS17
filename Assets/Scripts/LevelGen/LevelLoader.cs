using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    public ChunkGenerator cg;
    public GameObject[] GMs = new GameObject[14];
    public Transform generationPoint;

    private int prevEndHeight = -1;
    private int offset;
    private int count;
    private int diff;

    Queue<Transform> chunkList = new Queue<Transform>();

    // Use this for initialization
    void Start () {
        offset = 0;
        count = 0;
        diff = 1;
        chunkList.Enqueue(cg.MakeLevel("Next", diff, 10, 0, GMs, offset, ref prevEndHeight));
    }
	
	// Update is called once per frame
	void Update () {
        if(transform.position.x > generationPoint.position.x ) {
            if(chunkList.Count > 2) {
                //delete old chunk
                Destroy(chunkList.Dequeue().gameObject);
            }
            offset += 50;
            if(diff < 8) {
                diff++;
            }
            generationPoint.position = new Vector3(generationPoint.position.x + 50, generationPoint.position.y, 0); //move forward 50 blocks
            chunkList.Enqueue(cg.MakeLevel("Next", diff, prevEndHeight, 0, GMs, offset, ref prevEndHeight));
            print(prevEndHeight);
        }
    }
}
