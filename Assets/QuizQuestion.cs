using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuizQuestion : MonoBehaviour
{

    [System.Serializable] public class ImgQuestion {
        public Sprite gambar;
        [Header("Soal")]
        [TextArea]public string QuestionContent;
        public List<Answer> answers = new List<Answer>();
        public int answerIndex;
    }
    [System.Serializable]
    public class Answer {
        public string content;
    }

    private Image gambarSoal;
    private Text questiontext;
    private Text[] answerText = new Text[4];
    private GameObject Victory;

    public List<ImgQuestion> QuestionList = new List<ImgQuestion>();
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            gambarSoal = GameObject.Find("Question Image").GetComponent<Image>();
        }
        catch
        {
            Debug.Log("Proceeding Without Image");
        }      
        questiontext = GameObject.Find("Question").GetComponent<Text>();
        answerText[0] = GameObject.Find("A").GetComponent<Text>();
        answerText[1] = GameObject.Find("B").GetComponent<Text>();
        answerText[2] = GameObject.Find("C").GetComponent<Text>();
        answerText[3] = GameObject.Find("D").GetComponent<Text>();
        index = Random.Range(0, QuestionList.Count);
                
        Victory = GameObject.Find("Victory");
        Victory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestionList.Count > 0)
        {
            questiontext.text = QuestionList[index].QuestionContent;
            try {
                gambarSoal.sprite = QuestionList[index].gambar;
            } catch {
                Debug.Log("Proceeding Without Image");
            }           
            
            for (int i = 0; i < answerText.Length; i++) {
                answerText[i].text = QuestionList[index].answers[i].content;
            }
        }
        else
        {
            //currentLimit = 0;
            Victory.SetActive(true);
            //timeText.gameObject.SetActive(false);
            Debug.Log("Quiz Over");
        }
    }

    

    public void AnswerCheck(int answer) {
        if (QuestionList[index].answerIndex == answer)
        {
            nextRandomQuestion();
        }
        else {
            Debug.Log("Wrong Answer");
        }
    }

    public void nextRandomQuestion()
    {
        QuestionList.RemoveAt(index);
        index = Random.Range(0, QuestionList.Count);
    }
}
