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
    [SerializeField] private Text questiontext;  
    [SerializeField] private GameObject Victory;
    [SerializeField] Button[] AnswerChoices;

    private int index, randomCase;
    
    // Start is called before the first frame update
    void Start()
    {      
        FindObjectOfType<AudioManager>().PlayMusic("Level Music");
        //gambarSoal = GameObject.Find("Question Image").GetComponent<Image>();          
        //gambarSoal = Scenery;

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
            FindObjectOfType<AudioManager>().PlaySound("Correct");
            StartCoroutine(ButtonTransition(2f));
        }
        else
        {
            Debug.Log("Wrong Answer");
            FindObjectOfType<AudioManager>().PlaySound("Wrong");
        }
    }

    void nextRandomQuestion()
    {
        if (QuestionList.Count > 0)
        {
            QuestionList.RemoveAt(index);
            if (QuestionList.Count <= 0)
            {
                Debug.Log("Quiz Over");
                FindObjectOfType<AudioManager>().StopMusic("Level Music");
                FindObjectOfType<AudioManager>().PlayMusic("Stage Clear");
            }
        }
        index = Random.Range(0, QuestionList.Count);
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
        nextRandomQuestion();
    }
}
