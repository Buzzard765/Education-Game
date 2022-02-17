using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    [SerializeField] private Sprite Background;
    [SerializeField] private string spriteName;
    public List<Button> bttnList = new List<Button>();
    public Sprite[] pictures;
    public List<Sprite> pctrList = new List<Sprite>();

    private bool firstPair, secondPair;
    private int CorrectPair, PairLimit, Guesses;

    private string firstPairName, secondPairName;

    private int firstPairIndex, secondPairIndex;
    private AudioSource allAudio;

    [SerializeField] AudioClip SFX_Wrong, SFX_Right;
    [SerializeField] Text HighScore;
    [SerializeField] private RectTransform WinPanel;

    // Start is called before the first frame update
    // 
    private void Awake()
    {
        pictures = Resources.LoadAll<Sprite>("Sprites/"+ spriteName);
        allAudio = GetComponent<AudioSource>();      
        WinPanel.gameObject.SetActive(false);
    }
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayMusic("Level Music");
        getButtons();
        AddListener();
        AddPairs();
        Shuffle(pctrList);
        PairLimit = pctrList.Count / 2;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    void getButtons() {
        GameObject[] bttn = GameObject.FindGameObjectsWithTag("PzzlBttn");
        for (int i = 0; i < bttn.Length; i++) {
            bttnList.Add(bttn[i].GetComponent<Button>());
            bttnList[i].image.sprite = Background;
        }
    }

    void AddPairs() {
        int looper = bttnList.Count;
        int index = 0;
        for (int i = 0; i < looper; i++) {
            Debug.Log(looper);
            if (index == looper / 2) {
                Debug.Log(index);
                index = 0;
            }
            pctrList.Add(pictures[index]);
            index++;
        }
        
    }
    private void AddListener()
    {
        foreach (Button btn in bttnList)
        {
            btn.onClick.AddListener(() => PickAPair());
        }
    }

    public void PickAPair() {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("Selected" + name);
        if (firstPair == false)
        {
            firstPair = true;
            firstPairIndex = int.Parse
                (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstPairName = pctrList[firstPairIndex].name;
            bttnList[firstPairIndex].image.sprite = pctrList[firstPairIndex];
        }
        else if (secondPair == false) {
            secondPair = true;
            secondPairIndex = int.Parse
                (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondPairName = pctrList[secondPairIndex].name;
            bttnList[secondPairIndex].image.sprite = pctrList[secondPairIndex];
            Guesses++;
            StartCoroutine(PairCheck());
        }

        
    }

    IEnumerator PairCheck() {
        yield return new WaitForSeconds(1f);
        if (firstPairName == secondPairName)
        {
            yield return new WaitForSeconds(.5f);
            bttnList[firstPairIndex].interactable = false;
            bttnList[secondPairIndex].interactable = false;

            bttnList[firstPairIndex].image.color = new Color(0, 0, 0, 0);
            bttnList[secondPairIndex].image.color = new Color(0, 0, 0, 0);
            Debug.Log("Perfect Match");
            FindObjectOfType<AudioManager>().PlaySound("Correct");
            LimitCheck();
        }
        else
        {
            bttnList[firstPairIndex].image.sprite = Background;
            bttnList[secondPairIndex].image.sprite = Background;
            FindObjectOfType<AudioManager>().PlaySound("Wrong");
            Debug.Log("Didn't Match");
        }
        yield return new WaitForSeconds(.5f);

        firstPair = secondPair = false;
    }

    void LimitCheck() {
        Vector3 PanelPos = WinPanel.GetComponent<RectTransform>().anchoredPosition;
        float startPosY = PanelPos.y;
        CorrectPair++;
        if (CorrectPair == PairLimit) {
            FindObjectOfType<AudioManager>().StopMusic("Level Music");
            FindObjectOfType<AudioManager>().PlayMusic("Stage Clear");
            WinPanel.gameObject.SetActive(true);
            LeanTween.move(WinPanel, new Vector3(PanelPos.x, PanelPos.y - startPosY, PanelPos.z), 1f).setEaseOutBounce();
            Debug.Log("Game Cleared");
            HighScore.text = "Kamu Selesai dengan " + Guesses.ToString() + " tebakan";
            Debug.Log("you've made" + Guesses + "guesses to finish");
        }
    }

    void Shuffle(List<Sprite> list) {
        for (int i = 0; i < list.Count; i++) {
            Sprite temp = list[i];
            int randomint = Random.Range(0, list.Count);
            list[i] = list[randomint];
            list[randomint] = temp;
        }
        
    }
}
