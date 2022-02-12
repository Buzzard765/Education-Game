using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility;

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

    [SerializeField] private Image gambarSoal;
    [SerializeField] private Text questiontext;
    [SerializeField] private Text[] answerText = new Text[4];
    [FormerlySerializedAs("Victory")] [SerializeField] private GameObject victory;

    public List<ImgQuestion> QuestionList = new List<ImgQuestion>();
    public List<QuestionObject> Questions = new List<QuestionObject>();
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
                
        index = Random.Range(0, Questions.Count);
                        
        victory.SetActive(false);
        
        NextRandomQuestion();
    }


    public void AnswerCheck(int answer)
    {
        bool isCorrect = Questions[index].answerIndex == answer;
        
        if (isCorrect)
        {
            FindObjectOfType<AudioManager>().PlaySound("Correct");
            StartCoroutine(ButtonTransition(2f));
            
        }
        else 
        {
            Debug.Log("Wrong Answer");
            FindObjectOfType<AudioManager>().PlaySound("Wrong");
        }       
    }    
        
    void NextRandomQuestion()
    {
        if (Questions.Count <= 1)
        {
            StopQuiz();
            return;
        }
        
        Questions.RemoveAt(index);
        
        index = Random.Range(0, Questions.Count);

        SetQuestion();
    }

    void SetQuestion()
    {
        var question = Questions[index];
        questiontext.text = question.QuestionContent;

        if (question is ImageQuestionObject imageQuestion)
        {
            gambarSoal.sprite = imageQuestion.gambar;
        }
        else if (question is SoundQuestionObject soundQuestion)
        {
            FindObjectOfType<AudioManager>().PlaySound(soundQuestion.soundId);
        }
        else Debug.Log("Proceeding Without Image");

        for (int i = 0; i < answerText.Length; i++) {
            answerText[i].text = question.answers[i].content;
        }
    }

    void StopQuiz()
    {
        Debug.Log("Quiz Over");
        FindObjectOfType<AudioManager>().StopMusic("Level Music");
        FindObjectOfType<AudioManager>().PlayMusic("Stage Clear");  
        victory.SetActive(true);  
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
        NextRandomQuestion();
    }
}
