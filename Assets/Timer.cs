using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utility {
    public class Timer : MonoBehaviour
    {
        public Action onTimerExpired;

        public float Limit;
        private bool onTick;

        [SerializeField]private Text Text_Timer;
        // Start is called before the first frame update

        public void startTimer(float duration)
        {
            onTick = true;
            Limit = duration;
        }

        public void HaltTimer(float duration)
        {
            onTick = false;
            Limit = duration;
        }

        public void Add(float amount) {
            Limit += amount;
        }

        void Start()
        {
            //Text_Timer = GameObject.Find("Timer").GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            Text_Timer.text = Limit.ToString("F0");
            if (onTick == true)
            {
                Limit -= Time.deltaTime;
                if (Limit <= 0)
                {
                    TimeOut();
                }
            }
        }

        private void TimeOut() {
            onTick = false;
            Limit = 0;
            onTimerExpired.Invoke();
        }
    }
}

