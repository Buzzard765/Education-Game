﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeController : MonoBehaviour
{
    private int PlayerTurn;
    private int turnCount;

    public GameObject[] TurnIcon = new GameObject[2];
    public Sprite[] PlayerIcons = new Sprite[2];
    public Button[] TTTSpace = new Button[9];
    public int[] MarkedSpace;
    public GameObject[] WinningLine = new GameObject[8];
    public Button XPlayer, OPlayer;

    int XScore, OScore;
    int drawCondiLine, drawCondiSpace;

    [SerializeField] private Image WinPanel, DrawPanel;
    [SerializeField] private Text PanelText, XScoreText, OScoreText, WinOrDraw;

    private AudioSource AllAudio;
    private AudioSource BGM;
    [SerializeField] private AudioClip SFX_X, SFX_O, SFX_Win, SFX_Draw;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayMusic("Level Music");

        XScore = OScore = 0;
        //PlayerTurn = 0;
        turnCount = 0;
        drawCondiLine = 0;
        drawCondiSpace = 0;
        TurnIcon[0].SetActive(true);
        TurnIcon[1].SetActive(true);
        //Turn On All TTTSpaces
        for (int i = 0; i < TTTSpace.Length; i++) {
            TTTSpace[i].interactable = false;
            TTTSpace[i].GetComponent<Image>().sprite = null;
        }
        MarkedSpace = new int[TTTSpace.Length];
        for (int i = 0; i < MarkedSpace.Length; i++) {
            MarkedSpace[i] = -100;
        }
        //Turn Off all Winning Line
        for (int i = 0; i < WinningLine.Length; i++)
        {
            WinningLine[i].SetActive(false);
        }
        AllAudio = GetComponent<AudioSource>();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TTTButton(int slot) {

        XPlayer.interactable = false;
        OPlayer.interactable = false;

        TTTSpace[slot].image.sprite = PlayerIcons[PlayerTurn];
        TTTSpace[slot].interactable = false;
        MarkedSpace[slot] = PlayerTurn+1;
        turnCount++;
        drawCondiSpace++;
        if (turnCount > 4)
        {
            bool isWinner = winnerCheck();
            if (turnCount == 9 && isWinner == false) {
                BGM.Stop();
                AllAudio.PlayOneShot(SFX_Draw);
                DrawCheck();
            }

        }
       
        if (PlayerTurn == 0)
        {
            AllAudio.PlayOneShot(SFX_O);
            PlayerTurn = 1;
            TurnIcon[0].SetActive(false);
            TurnIcon[1].SetActive(true);
        }
        else {
            AllAudio.PlayOneShot(SFX_X);
            PlayerTurn = 0;
            TurnIcon[0].SetActive(true);
            TurnIcon[1].SetActive(false);
        }
    }

    bool winnerCheck() {
        
        int line1 = MarkedSpace[0] + MarkedSpace[1] + MarkedSpace[2];
        int line2 = MarkedSpace[3] + MarkedSpace[4] + MarkedSpace[5];
        int line3 = MarkedSpace[6] + MarkedSpace[7] + MarkedSpace[8];
        int line4 = MarkedSpace[0] + MarkedSpace[3] + MarkedSpace[6];
        int line5 = MarkedSpace[1] + MarkedSpace[4] + MarkedSpace[7];
        int line6 = MarkedSpace[2] + MarkedSpace[5] + MarkedSpace[8];
        int line7 = MarkedSpace[0] + MarkedSpace[4] + MarkedSpace[8];
        int line8 = MarkedSpace[2] + MarkedSpace[4] + MarkedSpace[6];
        var solutions = new int[] { line1, line2, line3, line4, line5, line6, line7, line8 };
        bool[] Not3 = new bool[solutions.Length]; 
        for (int i = 0; i < solutions.Length; i++) {
            if (solutions[i] == (PlayerTurn + 1) * 3)
            {
                
                Debug.Log("We Have a Winner: Player " + (PlayerTurn + 1) + "!");
                WinOrDraw.text = "We Have a Winner: Player " + (PlayerTurn + 1) + "!";
                WinOrDraw.gameObject.SetActive(true);
                DisplayWinner(i);
                return true;
            }
            else {
                drawCondiLine++;
            }           
        }
        return false;
    }

    void DrawCheck() {
        /*if (drawCondiSpace > 8 && drawCondiLine == 40) {
            
        }*/
        DrawPanel.gameObject.SetActive(true);  
    }

    void DisplayWinner(int LineIndex) {
        Vector3 PanelPos = WinPanel.transform.position;
        FindObjectOfType<AudioManager>().StopMusic("Level Music");
        FindObjectOfType<AudioManager>().PlayMusic("Stage Clear");

        WinningLine[LineIndex].SetActive(true);
        WinPanel.gameObject.SetActive(true);
        if (PlayerTurn == 0)
        {
            Color Red = new Color(1, 0.8f, 0.8f, 1);
            OScore++;
            OScoreText.text = OScore.ToString();
            WinPanel.color = Red;
        }
        else if (PlayerTurn == 1)
        {
            Color Blue = new Color(0.8f, 0.8f, 1f, 1f);
            XScore++;
            XScoreText.text = OScore.ToString();                      
            WinPanel.color = Blue;
        }
        LeanTween.move
               (WinPanel.gameObject, new Vector3(PanelPos.x, PanelPos.y - 1100, PanelPos.z), 2f)
               .setEaseOutBounce();
    }

    public void SwitchPlayer(int whichPlayer) {
        if (whichPlayer == 0)
        {
            PlayerTurn = 0;
            TurnIcon[0].SetActive(true);
            TurnIcon[1].SetActive(false);
        }
        else
        {
            PlayerTurn = 1;
            TurnIcon[0].SetActive(false);
            TurnIcon[1].SetActive(true);
        }
        for (int i = 0; i < TTTSpace.Length; i++)
        {
            TTTSpace[i].interactable = true;
            //TTTSpace[i].GetComponent<Image>().sprite = null;
        }
    }

    public void Rematch(GameObject Panel) {
        
        FindObjectOfType<AudioManager>().PlayMusic("Level Music");
        
        PlayerTurn = 0;
        turnCount = 0;
        TurnIcon[0].SetActive(true);
        TurnIcon[1].SetActive(true);
        drawCondiLine = 0;
        drawCondiSpace = 0;
        //Turn On All TTTSpaces
        for (int i = 0; i < TTTSpace.Length; i++)
        {
            TTTSpace[i].interactable = true;
            TTTSpace[i].GetComponent<Image>().sprite = null;
        }
        MarkedSpace = new int[TTTSpace.Length];
        for (int i = 0; i < MarkedSpace.Length; i++)
        {
            MarkedSpace[i] = -100;
        }
        //Turn Off all Winning Line
        for (int i = 0; i < WinningLine.Length; i++)
        {
            WinningLine[i].SetActive(false);
        }

        XPlayer.interactable = true;
        OPlayer.interactable = true;
        StartCoroutine(ReturnPanel(Panel));
    }

    IEnumerator ReturnPanel(GameObject Panel) {
        
        Vector3 PanelPos = Panel.transform.position;
        LeanTween.move
                (Panel.gameObject, new Vector3(PanelPos.x, PanelPos.y + 1100, PanelPos.z), 1f);
        yield return new WaitForSeconds(2f);
        Panel.SetActive(false);
        StopCoroutine(ReturnPanel(Panel));
    }
}
