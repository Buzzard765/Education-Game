using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{

    private BucketMovement Player;
    public int score, highscore;
    public float TimeLimit;
    public Text TimeText, ScoreText, HighscoreText;
    private GameObject Panel;
    public bool onPlay = true;
    AudioSource BGM;
    [SerializeField] AudioClip LevelBGM, ClearBGM;
    // Start is called before the first frame update
    void Start()
    {
        
        TimeLimit.ToString("F0");
        Panel = GameObject.Find("Panel");
        HighscoreText = GameObject.Find("Highscore").GetComponent<Text>();
        TimeText = GameObject.Find("Timer").GetComponent<Text>();
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
        highscore = PlayerPrefs.GetInt("highscore", highscore) ;
        Panel.SetActive(false);
        BGM = GetComponent<AudioSource>();
        BGM.loop = true;
        BGM.PlayOneShot(LevelBGM);
    }

    // Update is called once per frame
    void Update()
    {
        
        TimeLimit -= 1* Time.deltaTime;
        TimeText.text = TimeLimit.ToString("0");
        if (TimeLimit <= 0) {
            TimeLimit = 0;
            TimeText.text = ("Time's Up!").ToString();
        }
        ScoreText.text = "Score: " + score.ToString();

        if (TimeLimit <= 0)
        {
            onPlay = false;
            BGM.loop = false;
            BGM.PlayOneShot(ClearBGM);
            rewriteScore();
        }
    }

    void rewriteScore()
    {
        Panel.SetActive(true);
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", score);
            HighscoreText.text = "New Highscore!\n" + highscore.ToString();
        }
        else {
            HighscoreText.text = "Highscore:\n" + highscore.ToString();
        }
    }
}
