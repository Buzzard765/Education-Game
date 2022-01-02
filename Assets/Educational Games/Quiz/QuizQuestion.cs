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
    [SerializeField] Button[] AnswerChoices;

    private AudioSource allAudio, BGM;
    [SerializeField] AudioClip SFX_Wrong, SFX_Correct, SFX_Clear;
    // Start is called before the first frame update
    void Start()
    {
        //allAudio = GetComponent<AudioSource>();
        //BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        FindObjectOfType<AudioManager>().PlayMusic("Level Music");
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

            //BGM.clip = SFX_Clear;
            /*if (BGM.isPlaying == true) {
                BGM.Stop();
            }      */     
            Clear();
            //timeText.gameObject.SetActive(false);
            Debug.Log("Quiz Over");
        }
        
    }

    

    public void AnswerCheck(int answer) {
        if (QuestionList[index].answerIndex == answer)
        {
            allAudio.PlayOneShot(SFX_Correct);
            StartCoroutine(nextRandomQuestion());           
        }
        else {
            Debug.Log("Wrong Answer");
            allAudio.PlayOneShot(SFX_Wrong);
        }
    }
    void Clear() {
        Victory.SetActive(true);      
        BGM.clip = SFX_Clear;
        if (BGM.isPlaying == false)
        {
            BGM.Play();
        }       
    }
        
    IEnumerator nextRandomQuestion()
    {       
        /*yield return new WaitForSeconds(0);
        QuestionList.RemoveAt(index);
        index = Random.Range(0, QuestionList.Count);*/

        
        ButtonTransition(false);
        yield return new WaitForSeconds(0.5f);
       
        ButtonTransition(true);
        QuestionList.RemoveAt(index);
        index = Random.Range(0, QuestionList.Count);
    }

    void ButtonTransition(bool setActive)
    {
        for (int i = 0; i < AnswerChoices.Length; i++)
        {
            AnswerChoices[i].interactable = setActive;
        }
    }
}
