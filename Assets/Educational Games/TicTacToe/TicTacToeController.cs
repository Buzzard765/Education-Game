using System.Collections;
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

    int XScore, OScore;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTurn = 0;
        turnCount = 0;
        TurnIcon[0].SetActive(true);
        TurnIcon[1].SetActive(false);
        //Turn On All TTTSpaces
        for (int i = 0; i < TTTSpace.Length; i++) {
            TTTSpace[i].interactable = true;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TTTButton(int slot) {
        TTTSpace[slot].image.sprite = PlayerIcons[PlayerTurn];
        TTTSpace[slot].interactable = false;
        MarkedSpace[slot] = PlayerTurn+1;
        turnCount++;
        if (turnCount > 4)
        {
            winnerCheck();
        }
       
        if (PlayerTurn == 0)
        {
            PlayerTurn = 1;
            TurnIcon[0].SetActive(false);
            TurnIcon[1].SetActive(true);
        }
        else {
            PlayerTurn = 0;
            TurnIcon[0].SetActive(true);
            TurnIcon[1].SetActive(false);
        }
    }

    void winnerCheck() {

        int line1 = MarkedSpace[0] + MarkedSpace[1] + MarkedSpace[2];
        int line2 = MarkedSpace[3] + MarkedSpace[4] + MarkedSpace[5];
        int line3 = MarkedSpace[6] + MarkedSpace[7] + MarkedSpace[8];
        int line4 = MarkedSpace[0] + MarkedSpace[3] + MarkedSpace[6];
        int line5 = MarkedSpace[1] + MarkedSpace[4] + MarkedSpace[7];
        int line6 = MarkedSpace[2] + MarkedSpace[5] + MarkedSpace[8];
        int line7 = MarkedSpace[0] + MarkedSpace[4] + MarkedSpace[8];
        int line8 = MarkedSpace[2] + MarkedSpace[4] + MarkedSpace[6];
        var solutions = new int[] { line1, line2, line3, line4, line5, line6, line7, line8 };
        for (int i = 0; i < solutions.Length; i++) {
            if (solutions[i] == (PlayerTurn + 1) * 3) {
                Debug.Log("We Have a Winner: Player " + (PlayerTurn + 1) + "!");
                DisplayWinner(i);
            }
        }
    }

    void DisplayWinner(int LineIndex) {
        
        WinningLine[LineIndex].SetActive(true);
        if (PlayerTurn == 0)
        {
            OScore++;
        }
        else if (PlayerTurn == 1) {
            XScore++;
        }
    }

    void Tie() {
        Debug.Log("Tie");
    }

    void Rematch() {
        PlayerTurn = 0;
        turnCount = 0;
        TurnIcon[0].SetActive(true);
        TurnIcon[1].SetActive(false);
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
    }
}
