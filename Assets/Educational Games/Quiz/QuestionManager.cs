using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [System.Serializable]public class question {       
        [Header("Soal")]
        [TextArea] public string soal;

        [Header("Pilihan Jawaban")]
        public string pilA, pilB, pilC, pilD;

        [Header("Jawaban yang Benar")]
        public bool A, B, C, D;

        public float waktu;
    }
    public List<question> QuestionList = new List<question>();

    public int score;
    private float currentLimit;
    private int index;
    public GameObject Victory;
    Text questiontext, textA, textB, textC, textD, timeText;
    // Start is called before the first frame update
    void Start()
    {
        questiontext = GameObject.Find("Question").GetComponent<Text>();
        textA = GameObject.Find("A").GetComponent<Text>();
        textB = GameObject.Find("B").GetComponent<Text>();
        textC = GameObject.Find("C").GetComponent<Text>();
        textD = GameObject.Find("D").GetComponent<Text>();
        index = Random.Range(0, QuestionList.Count);
        currentLimit = QuestionList[index].waktu;
        timeText = GameObject.Find("Time Limit").GetComponent<Text>();
        Victory = GameObject.Find("Victory");
        Victory.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = currentLimit.ToString("0");
        //currentLimit -= Time.deltaTime;
        /*if (currentLimit <= 0) {
            nextRandomQuestion();
        }*/

        if (QuestionList.Count > 0)
        {
            questiontext.text = QuestionList[index].soal;
            textA.text = QuestionList[index].pilA;
            textB.text = QuestionList[index].pilB;
            textC.text = QuestionList[index].pilC;
            textD.text = QuestionList[index].pilD;
        }
        else {
            //currentLimit = 0;
            //timeText.gameObject.SetActive(false);
            Victory.SetActive(true);
            Debug.Log("Quiz Over");
        }
        
    }   

    public void answercheck(string answer) {
        if (QuestionList[index].A == true && answer == "A") {
            nextRandomQuestion();
        }
        else
        {
            Debug.Log("Wrong Answer");
        }
        if (QuestionList[index].B == true && answer == "B")
        {
            nextRandomQuestion();
        }
        else
        {
            Debug.Log("Wrong Answer");
        }
        if (QuestionList[index].C == true && answer == "C")
        {
            nextRandomQuestion();
        }
        else
        {
            Debug.Log("Wrong Answer");
        }
        if (QuestionList[index].D == true && answer == "D")
        {
            nextRandomQuestion();
        }
        else {
            Debug.Log("Wrong Answer");
        }
        //nextRandomQuestion();
        
    }
    public void nextRandomQuestion()
    {
        QuestionList.RemoveAt(index);
        index = Random.Range(0, QuestionList.Count);
        //currentLimit = QuestionList[index].waktu;
    }
}
