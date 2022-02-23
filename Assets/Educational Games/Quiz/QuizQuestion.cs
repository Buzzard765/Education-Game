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

    [SerializeField] private Timer timer;

    [SerializeField] private Image gambarSoal;
    [SerializeField] private Text questiontext, HighScoreText, ScoreText;
    [SerializeField] private Text[] answerText = new Text[4];
    [FormerlySerializedAs("Victory")] [SerializeField] private GameObject victory;

    public List<ImgQuestion> QuestionList = new List<ImgQuestion>();
    public List<QuestionObject> Questions = new List<QuestionObject>();
    private int index;
    private float TimeLimit;
    public float SetTimeLimit;
    [SerializeField] int Score;
    [SerializeField] Button[] AnswerChoices;

    private AudioSource allAudio, BGM;
    [SerializeField] AudioClip SFX_Wrong, SFX_Correct, SFX_Clear;
    // Start is called before the first frame update
    private void OnEnable()
    {
        timer.onTimerExpired += SkipQuestion;
    }
    private void OnDisable()
    {
        timer.onTimerExpired -= SkipQuestion;
    }
    void Start()
    {
        
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
        timer.startTimer(SetTimeLimit);                
        victory.SetActive(false);

        SetQuestion();
    }

    private void Update()
    {
        Debug.Log(timer.Limit);
    }


    public void AnswerCheck(int answer)
    {
        bool isCorrect = Questions[index].answerIndex == answer;

        if (isCorrect == true)
        {
            FindObjectOfType<AudioManager>().PlaySound("Correct");
            Score++;
            StartCoroutine(ButtonTransition(2f));

            AnswerChoices[answer].image.color = Color.blue;
        }
        else if (isCorrect == false || TimeLimit <= 0)
        {
            Debug.Log("Wrong Answer");
            FindObjectOfType<AudioManager>().PlaySound("Wrong");
            StartCoroutine(ButtonTransition(2f));
            AnswerChoices[answer].image.color = Color.red;
        }
        ScoreText.text = "Skor : " + Score.ToString();
    }    
        
    void NextRandomQuestion()
    {
        if (Questions.Count <= 1)
        {
            StopQuiz();
            return;
        }        
        Questions.RemoveAt(index);
        timer.startTimer(SetTimeLimit);
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
        victory.SetActive(true);
        HighScoreText.text = "Skor yang didapat:\n" + Score.ToString();
        if (Score > 6) {
            FindObjectOfType<AudioManager>().PlayMusic("Stage Clear");
        }
        else {
            FindObjectOfType<AudioManager>().PlayMusic("Stage Failed");
        }
    }

    void SkipQuestion() {
        FindObjectOfType<AudioManager>().PlaySound("Wrong");
        StartCoroutine(ButtonTransition(2f));
    }

    IEnumerator ButtonTransition(float delay)
    {
        timer.HaltTimer(timer.Limit);
        for (int i = 0; i < AnswerChoices.Length; i++)
        {
            AnswerChoices[i].interactable = false;
            
        }
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < AnswerChoices.Length; i++)
        {
            AnswerChoices[i].interactable = true;
            AnswerChoices[i].image.color = Color.white;
        }
        
        NextRandomQuestion();
    }
}
