using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
public class MoleNew : MonoBehaviour
{

    [SerializeField]protected int health, timeBonus;
    public int startHealth;
    private float OutGroundTime;    
    public float minTime, maxTime;
    [SerializeField]private Animator animator;
    [SerializeField]public int health_get
    {
        get { return health; }
        set { health = value; }
    }
    public float OutGroundTime_get
    {
        get { return OutGroundTime; }
        set { OutGroundTime = value; }
    }

    private void OnEnable()
    {
        health = startHealth;
        animator.SetFloat("state", 1);
        DeSpawn();
    }
    // Start is called before the first frame update
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
        if (minTime > 0.5f)
        {
            minTime -= Time.deltaTime / 100;
        }
        if (maxTime < 1.5f)
        {
            maxTime += Time.deltaTime / 100;
        }
    }

    public void DeSpawn()
    {
        health = startHealth;
        OutGroundTime = Random.Range(minTime, maxTime);
        
    }  

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && health > 0)
        {
            FindObjectOfType<AudioManager>().PlaySound("Knock");
            Debug.Log("Gotcha");
            health--;
            if (health <= 0) {
                StartCoroutine(KnockOut());               
                FindObjectOfType<MoleSpawningNew>().score_get += 1;
                FindObjectOfType<Timer>().Add(2);
            }           
        }
    }

    IEnumerator KnockOut()
    {
        animator.SetFloat("state", 2);
        
        yield return new WaitForSeconds(4.5f);
        animator.SetFloat("state", -1);
        gameObject.SetActive(false);
        StopCoroutine(KnockOut());
    }
}
