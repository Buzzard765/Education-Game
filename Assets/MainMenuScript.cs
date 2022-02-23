using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : AllUIButtons
{
    [SerializeField] List<GameObject> Panel = new List<GameObject>();
    int LastActivePanel;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayMusic("Main Menu");
        LastActivePanel = PlayerPrefs.GetInt("ActivePanel", LastActivePanel);
        SwitchPanel(LastActivePanel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    public void loadlevel(string levelname)
    {
        Debug.Log("Loading Level" + levelname);
        StartCoroutine(LoadLevel(levelname));
    }

    public void SwitchPanel(int index) {
        for (int i = 0; i < Panel.Count; i++) {
            var PanelIndex = Panel[i];
            PanelIndex.SetActive(i == index);
            PlayerPrefs.SetInt("ActivePanel", index);
        }        
    }

    IEnumerator LoadLevel(string name) {
        FindObjectOfType<AudioManager>().PlaySound("Chime");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(name);
    }

    public void leanTweenSlide(RectTransform Panel) {
        
        Vector3 PanelPos = Panel.GetComponent<RectTransform>().anchoredPosition;
        float StartPos = PanelPos.y;
        if (Panel.gameObject.activeSelf == false)
        {
            Panel.gameObject.SetActive(true);
            LeanTween.move
                (Panel, new Vector3(PanelPos.x, PanelPos.y - StartPos, PanelPos.z), 0.5f)
                .setEaseOutBounce();
           
        }
        else {
            LeanTween.move(Panel, new Vector3(PanelPos.x, PanelPos.y + StartPos, PanelPos.z), 0.5f);
            Panel.gameObject.SetActive(false);
            //Panel.LeanMove(new Vector3(StartPos.x, StartPos.y + 1200f, Panel.transform.position.z), 0.5f);

        }
        
    }
}
