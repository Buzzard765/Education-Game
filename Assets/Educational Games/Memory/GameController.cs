using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    [SerializeField] private Sprite Background;
    public List<Button> bttnList = new List<Button>();
    public Sprite[] pictures;
    public List<Sprite> pctrList = new List<Sprite>();

    private bool firstPair, secondPair;
    private int guessAmount, guessLimit, gameGuesses;

    private string firstPairName, secondPairName;

    private int firstPairIndex, secondPairIndex;

    // Start is called before the first frame update
    // 
    private void Awake()
    {
        pictures = Resources.LoadAll<Sprite>("Sprites/Pairing Pictures");
    }
    void Start()
    {
        getButtons();
        AddListener();
        AddPairs();
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
            firstPairIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstPairName = pctrList[firstPairIndex].name;
            bttnList[firstPairIndex].image.sprite = pctrList[firstPairIndex];
        }
        else if (secondPair == false) {
            secondPair = true;
            secondPairIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondPairName = pctrList[secondPairIndex].name;
            bttnList[secondPairIndex].image.sprite = pctrList[secondPairIndex];
        }

        if (firstPairName == secondPairName)
        {
            Debug.Log("Perfect Match");
        }
        else {
            Debug.Log("Didn't Match");
        }
    }
}
