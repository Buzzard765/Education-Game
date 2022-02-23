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
    [SerializeField] private float spawnRate;   
    private float currentspawnRate;
    private int randomSpot;

    [SerializeField] private int score, highscore;
    [SerializeField] private float timeLimit;
    [SerializeField] RectTransform Panel;
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
        currentspawnRate = 1;
        randomSpot = Random.Range(0, AllHoles.Count);
        foreach (Mole holes in AllHoles) {
            holes.MoleState = Mole.states.inGround;
        }
        timer.startTimer(timeLimit);
        highscore = PlayerPrefs.GetInt("highscore", highscore);
        Panel.gameObject.SetActive(false);
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
            FindObjectOfType<AudioManager>().PlaySound("Pop Up");
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

    void Result()
    {

        StartCoroutine(ResultBGMs());
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", score);
            ResultText.text = "Rekor baru! \n Nilai Tertinggi: \n" + score.ToString();
        }
        else
        {
            ResultText.text = "Nilai yang Terakhir: \n" + score.ToString();
        }
    }

     IEnumerator ResultBGMs() {
        Panel.gameObject.SetActive(true);
        Vector3 PanelPos = Panel.GetComponent<RectTransform>().anchoredPosition;
        float startPosY = PanelPos.y;
        FindObjectOfType<AudioManager>().StopMusic("Level Music");
        FindObjectOfType<AudioManager>().PlayMusic("Whistle");       
        yield return new WaitForSeconds(2f);

        LeanTween.move(Panel, new Vector3(PanelPos.x, PanelPos.y - (startPosY), PanelPos.z), 1f).setEaseOutBounce();
        
        FindObjectOfType<AudioManager>().PlayMusic("Stage Clear");

    }


}
