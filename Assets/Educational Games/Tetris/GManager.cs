using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public GameObject[] Tetriminos;

    public static int width = 6;
    public static int heigth = 20;

    
    // Start is called before the first frame update
    void Start()
    {
        spawnTetrimino();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool insideGrid(Vector2 pos) {
        return ((int)pos.x >= -6 && (int)pos.x < width && (int)pos.y >= -10);
    }

    public Vector2 Round(Vector2 pos) {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    public void spawnTetrimino() {
        GameObject currentMino = Tetriminos[Random.Range(0, Tetriminos.Length)];
        Vector2 spawnPos = new Vector2(0, 20);
        GameObject spawnMino = Instantiate(currentMino, spawnPos, Quaternion.identity);
        Debug.Log("Drop");
    }

   
}
