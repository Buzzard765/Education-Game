using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;
public class ObjectSpawner : MonoBehaviour
{
    public GameObject fruits, trash;
    public GameObject[] trees;
    [SerializeField]private float spawnrate;
    private float setSpawnRate;
    public Core GameManager;
    
    // Start is called before the first frame update    
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<Core>();
        setSpawnRate = 1;
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
            if (spawnrate > 0.5f) {
                spawnrate -= Time.deltaTime / 100;
            }
            if (setSpawnRate <= 0)
            {

                drops = Random.Range(0, 3);
                if (drops < 3)
                {
                    Instantiate(fruits, trees[Random.Range(0, trees.Length - 1)].transform.position, Quaternion.identity);
                }
                else if (drops >= 3)
                {
                    Instantiate(trash, trees[Random.Range(0, trees.Length - 1)].transform.position, Quaternion.identity);
                }
                setSpawnRate = Random.Range(spawnrate / 2, spawnrate + 1);
            }
            else
            {
                setSpawnRate -= Time.deltaTime;
            }
        }
              
    }

    
}
