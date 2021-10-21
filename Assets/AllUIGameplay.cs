using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUIGameplay : MonoBehaviour
{
    public Movement Player;
    public GameObject PanelVtr, PanelDft;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Movement>();
        PanelVtr = GameObject.Find("Victory");
        PanelDft = GameObject.Find("Defeat");
        PanelVtr.SetActive(false);
        PanelDft.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Movement.cleared == true && Movement.lose == false) {
            PanelActive(true, PanelVtr);
        }
        if (Movement.lose == true && Movement.cleared == false) {
            PanelActive(true, PanelDft);
        }
    }

    bool PanelActive(bool Clear, GameObject Panel) {

        Panel.SetActive(Clear);
        return Clear;
    }
}
