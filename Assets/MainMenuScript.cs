using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : AllUIButtons
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayMusic("Main Menu");
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

    IEnumerator LoadLevel(string name) {
        FindObjectOfType<AudioManager>().PlaySound("Chime");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(name);
    }

    public void leanTweenSlide(RectTransform Panel) {
        
        Vector3 StartPos = Panel.GetComponent<RectTransform>().anchoredPosition;
        if (Panel.gameObject.activeSelf == false)
        {
            Panel.gameObject.SetActive(true);
            LeanTween.move
                (Panel, new Vector3(StartPos.x, StartPos.y - 1200f, StartPos.z), 0.5f)
                .setEaseOutBounce();
           
        }
        else {
            LeanTween.move(Panel, new Vector3(StartPos.x, StartPos.y + 1200f, StartPos.z), 0.5f);
            Panel.gameObject.SetActive(false);
            //Panel.LeanMove(new Vector3(StartPos.x, StartPos.y + 1200f, Panel.transform.position.z), 0.5f);

        }
        
    }
}
