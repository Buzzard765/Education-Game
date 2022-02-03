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
        [SerializeField]private RectTransform Panel;
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
            highscore = PlayerPrefs.GetInt("highscore", highscore);
            Panel.gameObject.SetActive(false);
        }
        
        // Update is called once per frame
        void Update()
        {
            ScoreText.text = "Skor : " + score.ToString();
            if (score < 0) {
                score = 0;
            }
        }

        private void rewriteScore()
        {
            onPlay = false;
            Vector3 PanelPos = Panel.GetComponent<RectTransform>().anchoredPosition;
            float startPosY = PanelPos.y;
            FindObjectOfType<AudioManager>().StopMusic("Level Music");
            Panel.gameObject.SetActive(true);
            LeanTween.move(Panel, new Vector3(PanelPos.x, PanelPos.y - (startPosY), PanelPos.z), 1f).setEaseOutBounce();
            if (score > highscore)
            {
                highscore = score;
                PlayerPrefs.SetInt("highscore", score);
                FindObjectOfType<AudioManager>().PlayMusic("Stage Clear");
                HighscoreText.text = "Rekor Baru!\n Nilai yang didapat: \n" + highscore.ToString();
            }
            else
            {
                FindObjectOfType<AudioManager>().PlayMusic("Stage Failed");
               
                HighscoreText.text = "Nilai terakhir:\n" + highscore.ToString();
            }
        }
    }

}
