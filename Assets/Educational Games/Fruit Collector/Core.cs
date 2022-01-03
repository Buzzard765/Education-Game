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
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayMusic("Level Music");
        TimeLimit.ToString("F0");
        Panel = GameObject.Find("Panel");
        HighscoreText = GameObject.Find("Highscore").GetComponent<Text>();
        TimeText = GameObject.Find("Timer").GetComponent<Text>();
        ScoreText = GameObject.Find("Score").GetComponent<Text>();
        highscore = PlayerPrefs.GetInt("highscore", highscore) ;
        Panel.SetActive(false);       
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
            rewriteScore();
        }
    }

    void rewriteScore()
    {
        FindObjectOfType<AudioManager>().StopMusic("Level Music");       
        Panel.SetActive(true);
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", score);
            FindObjectOfType<AudioManager>().PlayMusic("Stage Cleared");
            HighscoreText.text = "New Highscore!\n" + highscore.ToString();
        }
        else {
            FindObjectOfType<AudioManager>().PlayMusic("Stage Failed");
            HighscoreText.text = "Highscore:\n" + highscore.ToString();
        }
    }
}
