using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject fruits, trash;
    public GameObject[] trees;
    public float spawnrate;
    public Core GameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<Core>();
    }

    // Update is called once per frame
    void Update()
    {

       
        /*for (float i = timeLimit - 1; i >= 0; i--)
        {
            spawnFruit();
        }*/

        spawnFruit();
    }

    void spawnFruit() {

        int drops;

        if (GameManager.onPlay == true) {
            if (spawnrate <= 0)
            {

                drops = Random.Range(0, 4);
                if (drops < 3)
                {
                    Instantiate(fruits, trees[Random.Range(0, trees.Length - 1)].transform.position + new Vector3(Random.Range(-5, 5), 0, 0), Quaternion.identity);
                }
                else if (drops >= 3)
                {
                    Instantiate(trash, trees[Random.Range(0, trees.Length - 1)].transform.position + new Vector3(Random.Range(-5, 5), 0, 0), Quaternion.identity);
                }
                spawnrate = Random.Range(spawnrate, spawnrate + 4);
            }
            else
            {
                spawnrate -= Time.deltaTime;
            }
        }
              
    }

    
}
