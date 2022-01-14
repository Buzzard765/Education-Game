using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AllUIButtons : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    public AudioSource UISFX;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame(PausePanel);
        }
    }

    public void PauseGame(GameObject panel) {
        if (Time.timeScale == 1 && panel.activeSelf == false) {
            Time.timeScale = 0;
            panel.SetActive(true);
        }
        else if(Time.timeScale == 0 && panel.activeSelf == true) {
            Time.timeScale = 1;
            panel.SetActive(false);
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void MainMenu() {
        Debug.Log("Heading back");
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
