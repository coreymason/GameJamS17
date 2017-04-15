using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour {

    private const int width = 50;
    private const int height = 25;
    private int diff = 2;
    private int startHeight = 4;
    private int raise = 0;

    private const int gapMinWidth = 1;
    private const int gapMaxWidth = 5;
    private int endHeight; //need to return this or something

    int[,] map;

    void Start() {
        GenerateMap();
    }

    void GenerateMap() {
        map = new int[width, height];

        if (diff <= 6) {
            int raised = 0;
            for (int i = 0; i < width; i++) {
                for (int j = 0; j <= startHeight; j++) {
                    map[i, j] = 1;
                    if (j == startHeight && raise > raised) {
                        map[i, j+1] = 2; //careful with out of bounds
                        startHeight++;
                        raised++;
                        break;
                    }
                }
                for (int j = startHeight + 1; j < height; j++) {
                    map[i, j] = 0;
                }
            }
        }

        if (diff == 2) {
            drawGaps(GetGaps(0+raise, 8, 2, 1));
        }

        if (diff == 3) {
            drawGaps(GetGaps(0+raise, 20, 3, 2));
        }

        if (diff == 4) {
            drawGaps(GetGaps(0+raise, 20, 3, 2));
            //platforms
        }

        if (diff == 5) {
            drawGaps(GetGaps(0+raise, 20, 4, 3));
        }

        if (diff == 6) {
            drawGaps(GetGaps(0+raise, 20,4, 3));
            //platforms
        }

        if (diff == 7) {
            //only platforms, no ground
        }

        //set endHeight
    }

    void drawGaps(Queue<int> gaps) {
        for (int i = 0; i < width; i++) {
            bool gapInProg = false;
            if (gaps.Count > 0 && gaps.Peek() == i) {
                gaps.Dequeue();
                gapInProg = true;
            }
            for (int j = 0; j <= startHeight; j++) {
                if (gapInProg) {
                    map[i, j] = 0;
                }
            }
        }
    }

    Queue<int> GetGaps(int curr, int max, int div, int mult) {
        Queue<int> gaps = new Queue<int>();
        while (gaps.Count < max) {
            bool exit = false;
            //Get gap
            int gapStart = Random.Range(curr, width - width/div*mult + curr);
            int gapLen = Random.Range(gapMinWidth, gapMaxWidth);
            curr = gapStart + 1;

            //Add gap to queue
            for (int i = 0; i < gapLen; i++) {
                if (gapStart + i < width - 2) { //end block cannot be gap
                    gaps.Enqueue(gapStart + i);
                }
                else {
                    exit = true;
                    break;
                }
            }

            //Break if going to far
            if (exit) {
                break;
            }
        }
        return gaps;
    }

    void OnDrawGizmos() {
        if(map != null) {
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    if(map[i, j] == 1) {
                        Gizmos.color = Color.black;
                    } else if (map[i, j] == 2) {
                        Gizmos.color = Color.red;
                    } else {
                        Gizmos.color = Color.white;
                    }
                    Vector3 pos = new Vector3(-width / 2 + i + .5f, 0, -height / 2 + j + .5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }
}
