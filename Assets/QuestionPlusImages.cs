using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPlusImages : MonoBehaviour
{
    [System.Serializable]
    public class Imagequestion
    {
        //public enum Jawaban { A, B, C, D}
        public Sprite gambar;
        [Header("Soal")]
        [TextArea] public string soal;

        [Header("Pilihan Jawaban")]
        public string pilA, pilB, pilC, pilD;

        [Header("Jawaban yang Benar")]
        //public Jawaban KunciJawaban;
        public bool A, B, C, D;

        public float waktu;
    }
    public List<Imagequestion> ImgQuestionList = new List<Imagequestion>();
    private Image gambarSoal;
    private float currentLimit;
    private int index;
    public GameObject Victory;
    Text questiontext, textA, textB, textC, textD, timeText;
    // Start is called before the first frame update
    void Start()
    {
        gambarSoal = GameObject.Find("Question Image").GetComponent<Image>();
        questiontext = GameObject.Find("Question").GetComponent<Text>();
        textA = GameObject.Find("A").GetComponent<Text>();
        textB = GameObject.Find("B").GetComponent<Text>();
        textC = GameObject.Find("C").GetComponent<Text>();
        textD = GameObject.Find("D").GetComponent<Text>();
        index = Random.Range(0, ImgQuestionList.Count);
        //currentLimit = ImgQuestionList[index].waktu;
        timeText = GameObject.Find("Time Limit").GetComponent<Text>();
        Victory = GameObject.Find("Victory");
        Victory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("test");
        timeText.text = currentLimit.ToString("0");
        //currentLimit -= Time.deltaTime;
        /*if (currentLimit <= 0)
        {
            nextRandomImgQuestion();
        }*/

        if (ImgQuestionList.Count > 0)
        {
            gambarSoal.sprite = ImgQuestionList[index].gambar;
            questiontext.text = ImgQuestionList[index].soal;
            textA.text = ImgQuestionList[index].pilA;
            textB.text = ImgQuestionList[index].pilB;
            textC.text = ImgQuestionList[index].pilC;
            textD.text = ImgQuestionList[index].pilD;
        }
        else
        {
            //currentLimit = 0;
            Victory.SetActive(true);
            //timeText.gameObject.SetActive(false);
            Debug.Log("Quiz Over");
        }
    }

    public void answercheck(string answer)
    {
        /*if (ImgQuestionList[index].KunciJawaban = pilihan)
        {
            nextRandomImgQuestion();
        }
        else
        {
            Debug.Log("Wrong Answer");
        }*/
        if (ImgQuestionList[index].A == true && answer == "A")
        {
            nextRandomImgQuestion();
        }        
        if (ImgQuestionList[index].B == true && answer == "B")
        {
            nextRandomImgQuestion();
        }        
        if (ImgQuestionList[index].C == true && answer == "C")
        {
            nextRandomImgQuestion();
        }       
        if (ImgQuestionList[index].D == true && answer == "D")
        {
            nextRandomImgQuestion();
        }        
        //nextRandomQuestion();

    }

    public void nextRandomImgQuestion()
    {
        ImgQuestionList.RemoveAt(index);
        index = Random.Range(0, ImgQuestionList.Count);        
    }
}
