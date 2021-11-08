using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public static int width = 6;
    public static int heigth = 20;
    // Start is called before the first frame update
    void Start()
    {

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
        GameObject NextTtmn = (GameObject)Instantiate(
            Resources.Load(randomName(), typeof(GameObject)),  new Vector2(5, 20), Quaternion.identity
            );
    }

    string randomName(){
        int ttmnIndex = Random.Range(1, 8);
        string ttmnName = "T";
        switch (ttmnIndex) {
            case 1:
                ttmnName = "I";
                break;
            case 2:
                ttmnName = "O";
                break;
            case 3:
                ttmnName = "S";
                break;
            case 4:
                ttmnName = "Z";
                break;
            case 5:
                ttmnName = "L";
                break;
            case 6:
                ttmnName = "J";
                break;
            case 7:
                ttmnName = "T";
                break;            
        }
        return ttmnName;
        
    }
}
