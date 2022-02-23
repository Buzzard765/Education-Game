using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
public class Obstacle : MoleNew
{

    public SpriteRenderer sprdr;
    public Sprite[] Condition;
    public float OutGroundTime;
    // Start is called before the first frame update

    private void OnEnable()
    {
           
        DeSpawn();
        sprdr.sprite = Condition[health];
    }
    void Start()
    {
        OutGroundTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (OutGroundTime < 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            OutGroundTime -= Time.deltaTime;
            //animator.SetFloat("state", 1);
        }
        if (minTime > (minTime-2))
        {
            minTime -= Time.deltaTime / 100;
        }
        if (maxTime < (maxTime + 1))
        {
            maxTime += Time.deltaTime / 100;
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && health > 0)
        {
            FindObjectOfType<AudioManager>().PlaySound("Knock");
            Debug.Log("Gotcha");
            health--;
            sprdr.sprite = Condition[health];
            if (health <= 0)
            {
                FindObjectOfType<AudioManager>().PlaySound("Bonk");
                FindObjectOfType<Timer>().Add(1);
                KnockOut();               
            }
        }
    }
   
    void KnockOut()
    {                         
        gameObject.SetActive(false);        
    }
}
