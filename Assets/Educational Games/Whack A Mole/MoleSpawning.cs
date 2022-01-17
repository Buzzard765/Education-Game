using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class MoleSpawning : MonoBehaviour
{
    [SerializeField] private Timer timer;

    [System.Serializable] public class MoleHole {
        public Transform hole;
        public bool hasMole;
    }

    //public GameObject Mole;
    //public Transform[] hole;
    //public bool hasMole;
    public List<Mole> AllHoles = new List<Mole>();
    public float spawnRate;
    private float currentspawnRate;
    private int randomSpot;

    [SerializeField] private int score;
    [SerializeField] private float timeLimit;
    [SerializeField] GameObject Panel;
    [SerializeField] Text ScoreText, TimeText, ResultText;

    public float time_get {
        get { return timeLimit; }
        set { timeLimit = value; }
    }
    public int score_get
    {
        get { return score; }
        set { score = value; }
    }

    private void OnEnable()
    {
        timer.onTimerExpired += Result;
    }
    private void OnDisable()
    {
        timer.onTimerExpired -= Result;
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayMusic("Level Music");
        currentspawnRate = spawnRate;
        randomSpot = Random.Range(0, AllHoles.Count);
        foreach (Mole holes in AllHoles) {
            holes.MoleState = Mole.states.inGround;
        }
        timer.startTimer(timeLimit);
        Panel.SetActive(false);
        //TimeText = GameObject.Find("Time").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer.Limit > 0){
            RandomizeSpawn();
        }
            

        TimeText.text = timeLimit.ToString("F0");
        ScoreText.text =   score.ToString();
    }

    void RandomizeSpawn() {
        if (currentspawnRate <= 0 && AllHoles[randomSpot].MoleState == Mole.states.inGround)
        {
            spawnMole(randomSpot);
            randomSpot = Random.Range(0, AllHoles.Count);
            currentspawnRate = spawnRate;
        }
        else if (AllHoles[randomSpot].MoleState == Mole.states.OutGround)
        {
            randomSpot = Random.Range(0, AllHoles.Count);
        }
        else
        {
            currentspawnRate -= Time.deltaTime;
        }
    }

    void spawnMole(int index) {
        AllHoles[index].MoleState = Mole.states.OutGround;
    }

    void Result(){

        StartCoroutine(ResultBGMs());
        
    }

    IEnumerator ResultBGMs() {

        FindObjectOfType<AudioManager>().StopMusic("Level Music");
        //FindObjectOfType<AudioManager>().PlayMusic("Whistle");
        yield return new WaitForSeconds(2f);
        Panel.SetActive(true);
        ResultText.text = "Skor yang didapat: \n" + score;
        FindObjectOfType<AudioManager>().PlayMusic("Stage Clear");

    }


}
