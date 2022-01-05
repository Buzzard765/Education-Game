using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utility {
    public class Timer : MonoBehaviour
    {
        public Action onTimerExpired;

        public float TimeLimit;
        private bool onTick;

        private Text Text_Timer;
        // Start is called before the first frame update

        public void startTimer(float duration)
        {
            onTick = true;
            TimeLimit = duration;
        }

        public void Add(float amount) {
            TimeLimit += amount;
        }

        void Start()
        {
            Text_Timer = GameObject.Find("Timer").GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            Text_Timer.text = TimeLimit.ToString("F0");
            if (onTick == true)
            {
                TimeLimit -= Time.deltaTime;
                if (TimeLimit <= 0)
                {
                    TimeOut();
                }
            }
        }

        private void TimeOut() {
            onTick = false;
            TimeLimit = 0;
            onTimerExpired.Invoke();
        }
    }
}

