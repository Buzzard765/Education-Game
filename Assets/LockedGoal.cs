using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedGoal : MonoBehaviour
{
    public Movement Player;
    public int remainingKeys;
    public RectTransform Panel_Continue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && remainingKeys == 0)
        {
            Destroy(gameObject);
        }
        else {
            ReturnMaze(true);
        }
    }

    public void ReturnMaze(bool isActive) {
        Vector3 PanelPos = Panel_Continue.GetComponent<RectTransform>().anchoredPosition;
        float startPosY = PanelPos.y;
        Panel_Continue.gameObject.SetActive(isActive);
        if (Panel_Continue.gameObject.activeSelf == true) {
            LeanTween.move
                (Panel_Continue, new Vector3(PanelPos.x, PanelPos.y - startPosY, PanelPos.z), 1f)
                .setEaseOutBounce();
        }
        else if (Panel_Continue.gameObject.activeSelf == false){
            LeanTween.move
                (Panel_Continue, new Vector3(PanelPos.x, PanelPos.y + startPosY, PanelPos.z), 1f);
                
        }
    }
}
