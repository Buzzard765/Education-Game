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

    [SerializeField]private Image gambarSoal;
    [SerializeField] private Text questiontext;
    [SerializeField] private Text[] answerText = new Text[4];
    [SerializeField] private GameObject Victory;

    public List<ImgQuestion> QuestionList = new List<ImgQuestion>();
    private int index;
    [SerializeField] Button[] AnswerChoices;

    private AudioSource allAudio, BGM;
    [SerializeField] AudioClip SFX_Wrong, SFX_Correct, SFX_Clear;
    // Start is called before the first frame update
    void Start()
    {
        allAudio = GetComponent<AudioSource>();
        //BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        FindObjectOfType<AudioManager>().PlayMusic("Level Music");

        /*try
        {
            gambarSoal = GameObject.Find("Question Image").GetComponent<Image>();
        }
        catch
        {
            Debug.Log("Proceeding Without Image");
        }*/
                
        index = Random.Range(0, QuestionList.Count);
                        
        Victory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //FindObjectOfType<AudioManager>().PlayMusic("Level Music");
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
            Victory.SetActive(true);          
        }
        
    }


    public void AnswerCheck(int answer) {
        if (QuestionList[index].answerIndex == answer)
        {
            FindObjectOfType<AudioManager>().PlaySound("Correct");
            StartCoroutine(ButtonTransition(2f));
            
        }
        else {
            Debug.Log("Wrong Answer");
            FindObjectOfType<AudioManager>().PlaySound("Wrong");
        }       
    }    
        
    void nextRandomQuestion()
    {                      
        if (QuestionList.Count > 0)
        {
            QuestionList.RemoveAt(index);
            if (QuestionList.Count <= 0) {
                Debug.Log("Quiz Over");
                FindObjectOfType<AudioManager>().StopMusic("Level Music");
                FindObjectOfType<AudioManager>().PlayMusic("Stage Clear");  
            }
        }         
        index = Random.Range(0, QuestionList.Count);
    }

    IEnumerator ButtonTransition(float delay)
    {
        for (int i = 0; i < AnswerChoices.Length; i++)
        {
            AnswerChoices[i].interactable = false;
        }
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < AnswerChoices.Length; i++)
        {
            AnswerChoices[i].interactable = true    ;
        }
        nextRandomQuestion();
    }
}
