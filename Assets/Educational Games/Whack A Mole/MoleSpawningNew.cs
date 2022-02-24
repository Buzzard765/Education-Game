using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class MoleSpawningNew : MonoBehaviour
{
    [System.Serializable]public class Holes
    {
        public GameObject Mole, Rock;
    }
    public List<Holes> HoleList = new List<Holes>();

    [SerializeField] private Timer timer;

    [SerializeField] private float spawnRate;
    private float currentspawnRate;
    private int randomSpot;

    [SerializeField] private int score, highscore;
    [SerializeField] private float timeLimit;
    [SerializeField] RectTransform Panel;
    [SerializeField] Text ScoreText, TimeText, ResultText;

    public float time_get
    {
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
        randomSpot = Random.Range(0, HoleList.Count);
        timer.startTimer(timeLimit);
        highscore = PlayerPrefs.GetInt("highscore", highscore);
        Panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.Limit > 0)
        {
            RandomizeSpawn();
        }
        TimeText.text = timer.Limit.ToString("F0");
        ScoreText.text = score.ToString();  
    }

    void RandomizeSpawn()
    {
        bool hasSpawned_Mole = HoleList[randomSpot].Mole.activeSelf;
        bool hasSpawned_Rock = HoleList[randomSpot].Rock.activeSelf;
        bool SpawnedOne_ = hasSpawned_Mole || hasSpawned_Rock;
        
        if (currentspawnRate <= 0) 
        {
            if (SpawnedOne_ == false) {
                spawnMole(randomSpot);
                FindObjectOfType<AudioManager>().PlaySound("Pop Up");
                randomSpot = Random.Range(0, HoleList.Count);
                currentspawnRate = spawnRate;
            }
            else if (SpawnedOne_ == true)
            {
                randomSpot = Random.Range(0, HoleList.Count);
            }
        }       
        else
        {
            currentspawnRate -= Time.deltaTime;
        }
    }
    void spawnMole(int index)
    {
        int rand = Random.Range(0, 5);
        Debug.Log(rand);
        if (rand < 2)
        {
            HoleList[index].Rock.SetActive(true);
            Debug.Log("Rock", HoleList[index].Rock);
        }
        else if (rand >= 2)
        {
            HoleList[index].Mole.SetActive(true);
            Debug.Log("Mole", HoleList[index].Mole);
        }       
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

    IEnumerator ResultBGMs()
    {
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
