using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Manager {
    public class Core : MonoBehaviour
    {
        [SerializeField]private Timer timer;
        public float limit;
        private BucketMovement Player;
        public int score, highscore;

        public Text TimeText, ScoreText, HighscoreText;
        [SerializeField]private GameObject Panel;
        public bool onPlay = true;
        private void OnEnable()
        {
            timer.onTimerExpired += rewriteScore;
        }
        private void OnDisable()
        {
            timer.onTimerExpired -= rewriteScore;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            
            timer.startTimer(limit);
            FindObjectOfType<AudioManager>().PlayMusic("Level Music");         
            Panel.SetActive(false);
        }
        
        // Update is called once per frame
        void Update()
        {


        }

        private void rewriteScore()
        {
            onPlay = false;
            FindObjectOfType<AudioManager>().StopMusic("Level Music");
            Panel.SetActive(true);
            if (score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt("highscore", score);
                FindObjectOfType<AudioManager>().PlayMusic("Stage Cleared");
                HighscoreText.text = "New Highscore!\n" + highscore.ToString();
            }
            else
            {
                FindObjectOfType<AudioManager>().PlayMusic("Stage Failed");
                HighscoreText.text = "Highscore:\n" + highscore.ToString();
            }
        }
    }

}
