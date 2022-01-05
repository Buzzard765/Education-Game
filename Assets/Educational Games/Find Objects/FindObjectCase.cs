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
    [SerializeField] Button[] AnswerChoices;

    private int index, randomCase;

    private AudioSource allAudio;
    [SerializeField] AudioClip SFX_Wrong, SFX_Correct;
    // Start is called before the first frame update
    void Start()
    {
        allAudio = GetComponent<AudioSource>();
        FindObjectOfType<AudioManager>().PlayMusic("Level Music");
        //gambarSoal = GameObject.Find("Question Image").GetComponent<Image>();
        questiontext = GameObject.Find("Question").GetComponent<Text>();       
        //gambarSoal = Scenery;

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
            allAudio.PlayOneShot(SFX_Correct);
            StartCoroutine(ButtonTransition(2f));
        }
        else
        {
            Debug.Log("Wrong Answer");
            allAudio.PlayOneShot(SFX_Wrong);
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
