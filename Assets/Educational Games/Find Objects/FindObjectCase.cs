using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FindObjectCase : MonoBehaviour
{
    [System.Serializable]
    public class ImgQuestion
    {
        [Header("Soal")]
        [TextArea] public string QuestionContent;

        public int answerIndex;
    }

    public Image Scenery;

    public List<ImgQuestion> QuestionList = new List<ImgQuestion>();   
    private Image gambarSoal;
    private Text questiontext;
    private Text[] answerText = new Text[4];
    private GameObject Victory;

    private int index, randomCase;

    //private AudioSource allAudio;
    //[SerializeField] AudioClip SFX_Wrong, SFX_Correct;
    // Start is called before the first frame update
    void Start()
    {        
        //gambarSoal = GameObject.Find("Question Image").GetComponent<Image>();
        questiontext = GameObject.Find("Question").GetComponent<Text>();
        answerText[0] = GameObject.Find("A").GetComponent<Text>();
        answerText[1] = GameObject.Find("B").GetComponent<Text>();
        answerText[2] = GameObject.Find("C").GetComponent<Text>();
        answerText[3] = GameObject.Find("D").GetComponent<Text>();
        gambarSoal = Scenery;

        index = Random.Range(0, QuestionList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestionList.Count > 0)
        {
            questiontext.text = QuestionList[index].QuestionContent;                    
        }
        else
        {
            //currentLimit = 0;
            Victory.SetActive(true);
            //timeText.gameObject.SetActive(false);
            Debug.Log("Quiz Over");
        }
    }

    public void AnswerCheck(int answer)
    {
        if (QuestionList[index].answerIndex == answer)
        {
            //allAudio.PlayOneShot(SFX_Correct);
            StartCoroutine(nextRandomQuestion());
        }
        else
        {
            Debug.Log("Wrong Answer");
            //allAudio.PlayOneShot(SFX_Wrong);
        }
    }

    IEnumerator nextRandomQuestion()
    {
        yield return new WaitForSeconds(0);
        QuestionList.RemoveAt(index);
        index = Random.Range(0, QuestionList.Count);
    }
}
