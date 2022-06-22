/**
 Based on JS Studios Games' Terrain Generator:
 Original Code: https://www.dropbox.com/s/osis7k55o6ua7bt/Generate.cs?dl=0
 YouTube Video: https://www.youtube.com/watch?v=j2E5nXzzrhg&list=PL04tHwWlDDmcpymHMSUbz3OOHaF41XBtk&index=5&t=140s
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    public int width;
    public int height;
    private int resizedHeight;
    public int gap = 16;
    public GameObject block;
 
    public float heightpoint;
    public float heightpoint2;
 
    void Start(){
        width *= gap;
        height *= gap;
        Generation();
    }
 
    void Generation() {
        for (int w = 0; w < width; w += gap*2) {
            resizedHeight = height - (int)((Random.Range(0f, 3) * 16));
 
            for (int j = 0; j < resizedHeight; j += gap) {
                Instantiate(block, new Vector3(w, j), Quaternion.identity);
                Instantiate(block, new Vector3(w+gap, j), Quaternion.identity);
            }
        }
    }
}
