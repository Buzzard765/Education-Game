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

    public void leanTweenSlide(GameObject Panel) {
        LeanTween.cancel(Panel);
        Vector3 StartPos = Panel.transform.position;
        if (Panel.activeSelf == false)
        {
            Panel.SetActive(true);
            LeanTween.move
                (Panel, new Vector3(StartPos.x, StartPos.y - 1200f, Panel.transform.position.z), 0.5f)
                .setEaseOutBounce();
           
        }
        else {
            LeanTween.move(Panel, new Vector3(Panel.transform.position.x, Panel.transform.position.y + 1200f, Panel.transform.position.z), 0.5f);
            Panel.SetActive(false);
            //Panel.LeanMove(new Vector3(StartPos.x, StartPos.y + 1200f, Panel.transform.position.z), 0.5f);

        }
        
    }
}
