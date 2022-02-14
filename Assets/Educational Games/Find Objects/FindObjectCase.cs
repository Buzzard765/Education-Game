using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;
public class FindObjectCase : MonoBehaviour
{
   
    /*public class ImgQuestion
    {
        [Header("Soal")]
        [TextArea] public string QuestionContent;

        public int answerIndex;
    }*/

    public Image Scenery;

    
    private Image gambarSoal;
    [SerializeField] private Text questiontext;  
    [SerializeField] private GameObject Victory;
    [SerializeField] Button[] AnswerChoices;

    
    public List<QuestionObject> QuestionList = new List<QuestionObject>();

    private int index, randomCase;
    
    // Start is called before the first frame update
    void Start()
    {      
        FindObjectOfType<AudioManager>().PlayMusic("Level Music");
        //gambarSoal = GameObject.Find("Question Image").GetComponent<Image>();          
        //gambarSoal = Scenery;

        index = Random.Range(0, QuestionList.Count);
        NextRandomQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetQuestion()
    {
        var question = QuestionList[index];
        questiontext.text = question.QuestionContent;

        if (question is ImageQuestionObject imageQuestion)
        {
            gambarSoal.sprite = imageQuestion.gambar;
        }        
        else Debug.Log("Proceeding Without Image");
       
    }

    public void AnswerCheck(int answer)
    {
        if (QuestionList[index].answerIndex == answer)
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
        if (QuestionList.Count <= 1)
        {
            StopQuiz();
            return;
        }

        QuestionList.RemoveAt(index);
        index = Random.Range(0, QuestionList.Count);
        SetQuestion();
    }

    void StopQuiz()
    {
        Debug.Log("Quiz Over");
        FindObjectOfType<AudioManager>().StopMusic("Level Music");
        FindObjectOfType<AudioManager>().PlayMusic("Stage Clear");
        Victory.SetActive(true);
    }

    IEnumerator ButtonTransition(float delay)
    {
        questiontext.gameObject.SetActive(false);
        for (int i = 0; i < AnswerChoices.Length; i++)
        {
            AnswerChoices[i].interactable = false;
        }
        yield return new WaitForSeconds(delay);
        questiontext.gameObject.SetActive(true);
        for (int i = 0; i < AnswerChoices.Length; i++)
        {
            AnswerChoices[i].interactable = true;
        }
        NextRandomQuestion();
    }
}
