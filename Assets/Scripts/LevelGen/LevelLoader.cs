using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(transform.position.y < -20) {
            SceneManager.LoadScene("title");
        }
        if(transform.position.x > generationPoint.position.x ) {
            if(chunkList.Count > 2) {
                //delete old chunk
                Destroy(chunkList.Dequeue().gameObject);
            }
            offset += 50;
            int raiseVal = 0;
            if (diff != 9) {
                raiseVal = Random.Range(0, 3); //change to 4 later and make sure carrots work past raise 2
                if (diff == 8) {
                    if(count < 2) {
                        count++;
                    } else {
                        prevEndHeight -= 7;
                        diff++;
                    }
                } else {
                    diff++;
                }
            }
            generationPoint.position = new Vector3(generationPoint.position.x + 50, generationPoint.position.y, 0); //move forward 50 blocks
            chunkList.Enqueue(cg.MakeLevel("Next", diff, prevEndHeight, raiseVal, GMs, offset, ref prevEndHeight));
            print(prevEndHeight);
        }
    }
}
