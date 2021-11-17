using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSpawning : MonoBehaviour
{
    [System.Serializable] public class MoleHole {
        public Transform hole;
        public bool hasMole;
    }

    public GameObject Mole;
    //public Transform[] hole;
    //public bool hasMole;
    public List<GameObject> AllHoles = new List<GameObject>();
    public float spawnRate;
    private float currentspawnRate;
    private int randomSpot;
    private int score;
    [SerializeField]private float time;

    public float time_get {
        get { return time; }
        set { time = value; }
    }
    public int score_get
    {
        get { return score; }
        set { score = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentspawnRate = spawnRate;
        randomSpot = Random.Range(0, AllHoles.Count);
        foreach (GameObject holes in AllHoles) {
            holes.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0) {
            RandomizeSpawn();
        }
        time -= Time.deltaTime;
        
    }

    void RandomizeSpawn() {
        if (currentspawnRate <= 0 && AllHoles[randomSpot].activeSelf == false)
        {
            spawnMole(randomSpot);
            randomSpot = Random.Range(0, AllHoles.Count);
            currentspawnRate = spawnRate;
        }
        else if (AllHoles[randomSpot].activeSelf == true)
        {
            randomSpot = Random.Range(0, AllHoles.Count);
        }
        else
        {
            currentspawnRate -= Time.deltaTime;
        }
    }

    void spawnMole(int index) {       
        AllHoles[index].SetActive(true);
    }
}
