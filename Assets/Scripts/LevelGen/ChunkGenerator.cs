﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour {
    private const int width = 50;
    private const int height = 30;
    private const int gapMinWidth = 1;
    private const int gapMaxWidth = 5;

    private int startHeight;
    private int raise;
    private int diff;

    private int endHeight;

    private int[,] map;
    private Transform chunk;

    //TODO: check if ramp height is working
    //TODO: clouds
    public Transform MakeLevel(string holderName, int difficulty, int startHeight, int rampHeight, GameObject[] GMs, int offset, ref int newEndHeight) {
        this.startHeight = startHeight;
        diff = difficulty;
        raise = rampHeight;
        GenerateMap();
        CreateChunk(holderName, GMs, offset);
        newEndHeight = endHeight;
        return chunk;
    }

    void GenerateMap() {
        map = new int[width, height]; //why not swapped?!
        for(int i=0;i<width;i++) {
            for(int j=0;j<height;j++) {
                map[i, j] = 0;
            }
        }

        int numEggs = Random.Range(1, 4);

        if (diff == 1) {
            DrawGround();
            DrawRandomBasket(5);
            DrawRandomEgg(numEggs);
        }

        if (diff == 2) {
            DrawGround();
            DrawGaps(GetGaps(0+raise, 8, 2, 1));
            DrawRandomBasket(5);
            DrawRandomEgg(numEggs);

        }

        if (diff == 3) {
            DrawGround();
            DrawGaps(GetGaps(0+raise, 20, 3, 2));
            DrawRandomBasket(5);
            DrawRandomEgg(numEggs);
        }

        if (diff == 4) {
            DrawGround();
            DrawGaps(GetGaps(0+raise, 20, 3, 2));
            DrawPlatforms(GetPlatforms(0 + raise, 5, 7));
            DrawRandomBasket(10);
            DrawRandomEgg(numEggs);
        }

        if (diff == 5) {
            DrawGround();
            DrawGaps(GetGaps(0 + raise, 20, 3, 2));
            DrawPlatforms(GetPlatforms(0 + raise, 10, 7));
            DrawRandomBasket(10);
            DrawRandomEgg(numEggs);
        }

        if (diff == 6) {
            DrawGround();
            DrawGaps(GetGaps(0 + raise, 20, 4, 3));
            DrawRandomBasket(10);
            DrawRandomEgg(numEggs);
        }

        if (diff == 7) {
            DrawGround();
            DrawGaps(GetGaps(0+raise, 20,4, 3));
            DrawPlatforms(GetPlatforms(0 + raise, 5, 7));
            DrawRandomBasket(10);
            DrawRandomEgg(numEggs);
        }

        if (diff == 8) {
            DrawGround();
            DrawGaps(GetGaps(0 + raise, 20, 4, 3));
            DrawPlatforms(GetPlatforms(0 + raise, 10, 7));
            DrawRandomBasket(10);
            DrawRandomEgg(numEggs);
        }

        if (diff == 9) {
            DrawPlatforms(GetPlatforms(0 + raise, 25, 15));
            DrawRandomBasket(10);
            DrawRandomEgg(numEggs);
            endHeight = startHeight;
            return;
        }

        //set endHeight
        for (int i=0;i<height;i++) {
            if(map[width-1, height-1-i] != 0) { //TODO: careful about platforms in this last spot
                endHeight = height - 1 - i;
                break;
            }
        }
    }

    void DrawRandomEgg(int num) {
        int options = 0;
        for (int i = 0; i < width; i++) {
            for (int j = startHeight + 1; j < height; j++) {
                if(map[i, j - 1] != 0 && map[i, j - 1] != 13 && map[i, j] != 9) {
                    options++;
                }
            }
        }
        int choice = Random.Range(0, options);
        int count = 0;
        for (int i = 0; i < width; i++) {
            for (int j = startHeight + 1; j < height; j++) {
                if (map[i, j - 1] != 0 && map[i, j - 1] != 13 && map[i, j] != 9) {
                    if (count == choice) {
                        map[i, j] = 14;
                    }
                    count++;
                }
            }
        }

        if(num > 1) {
            DrawRandomEgg(num - 1);
        }
    }

    void DrawRandomBasket(int max) {
        int options = 0;
        for (int i = 0; i < width; i++) {
            for (int j = startHeight+1; j < Mathf.Min(height, startHeight+1 + max); j++) {
                options++;
            }
        }
        int choice = Random.Range(0, options);
        int count = 0;
        for (int i = 0; i < width; i++) {
            for (int j = startHeight + 1; j < Mathf.Min(height, startHeight + 1 + max); j++) {
                if(count == choice) {
                    map[i, j] = 13;
                }
                count++;
            }
        }
    }

    void DrawPlatforms(List<int[]> platforms) {
        foreach (int[] plat in platforms) {
            map[plat[1], plat[2]] = 10 + plat[0] - 1;
        }
    }

    List<int[]> GetPlatforms(int curr, int max, int cap) {   //[len, locX, locY]
        List<int[]> platforms = new List<int[]>();

        int numPlats = Random.Range(1, max+1);
        for(int i=0;i<numPlats;i++) {
            int len = Random.Range(1, 3+1);
            int locX = Random.Range(curr, width - len - 1);
            int locY = Random.Range(startHeight + 2, startHeight + cap + 1);
            while(PlatOverlap(platforms, len, locX, locY)) {
                locX = Random.Range(curr, width - len - 1);
                locY = Random.Range(startHeight + 2, startHeight + cap + 1);
            }
            platforms.Add(new int[] {len, locX, locY});
        }


        return platforms;
    }

    bool PlatOverlap(List<int[]> platforms, int len, int locX, int locY) {
        if(platforms.Count != 0) {
            foreach(int[] plat in platforms) {
                if(Mathf.Abs(locY - plat[2]) == 1) {
                    return true;
                }

                int platMin = plat[1];
                int platMax = platMin + plat[0] - 1;
                int locMin = locX;
                int locMax = locX + len - 1;
                if ((locMin <= platMax && locMax >= platMax) || (locMax >= platMin && locMin <= platMin)) {
                    return true;
                }
            }
        }
        return false;
    }

    void DrawGaps(Queue<int> gaps) {
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

    void DrawGround() {
        int raised = 0;
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height-2; j++) { //should ideally be just -1
                if(j <= startHeight) {
                    map[i, j] = 1;
                }
                if (i == width-raise+raised &&  j >= startHeight+raised && raise > raised) {
                    print("heyo"+i+"="+(j+1)+"  ");
                    map[i, j + 1] = 9; //careful with out of bounds
                    //startHeight++;
                    raised++;
                    //break;
                    if (j > startHeight) {
                        for (int k = 0; k < raised; k++) {
                            map[i, j - k] = 1;
                        }
                    }
                }
            }
        }
        UpdateGround();
    }

    void UpdateGround() {
        for (int i = 0; i < width; i++) {
            int counter = 0;
            for (int j = 0; j <= startHeight; j++) {
                if (map[i, j + 1] == 0 && map[i, j] != 9) { //careful with out of bounds
                    if (counter % 2 == 0) {
                        map[i, j] = 3;
                    }
                    else {
                        map[i, j] = 4;
                    }
                    counter++;
                }
            }
        }

        for (int i = 0; i < width; i++) {
            for (int j = 0; j <= startHeight; j++) {
                if (map[i, j] == 3 || map[i, j] == 4) {
                    if (i != 0 && i != width - 1 && map[i - 1, j] == 0 && map[i + 1, j] == 0) {
                        map[i, j] = 2;
                    }
                    else if (i != width - 1 && map[i + 1, j] == 0) {
                        map[i, j] = (map[i, j] == 3) ? 7 : 8;
                    }
                    else if (i != 0 && map[i - 1, j] == 0) {
                        map[i, j] = (map[i, j] == 3) ? 5 : 6;
                    }
                }
            }
        }
    }

    //0 is empty
    //1 is solid grass
    //2 is top grass single
    //3 is top grass 1
    //4 is top grass 2
    //5 is top grass left end grass 1
    //6 is top grass left end grass 2
    //7 is top grass right end grass 1
    //8 is top grass right end grass 2
    //9 is slope up
    //10 is 1x platform
    //11 is 2x platform
    //12 is 3x platform
    //13 is basket
    //14 is egg
    void CreateChunk(string holderName, GameObject[] GMs, int offset) {
        if(map != null) {
            chunk = new GameObject(holderName).transform;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j] != 0)
                    {
                        if(map[i,j] == 9) {
                            print("rekt");
                        }
                        GameObject instance = Instantiate(GMs[map[i, j] - 1], new Vector3(i+offset, j, 0f), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(chunk);
                    }
                }
            }
        }
    }
}
