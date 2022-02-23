using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utility;
public class Mole : MonoBehaviour
{
    public enum states {
        inGround,
        OutGround,
        KnockedOut
    }

    public states MoleState;

    private float OutGroundTime;
    private float UnderGround;
    public float minTime, maxTime;
    private Animator animator;
    public bool onGround;

    private AudioSource AllAudio;
    [SerializeField] AudioClip SFX_Despawn, SFX_Knock;
    // Start is called before the first frame update
    void Start()
    {
        OutGroundTime = Random.Range(minTime, maxTime);
        animator = GetComponent<Animator>();
        AllAudio = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (MoleState == states.OutGround) {
            animator.SetFloat("state", 1);
            if (OutGroundTime < 0)
            {
                DeSpawn();               
            }
            else {
                OutGroundTime -= Time.deltaTime;
                
            }
        }

        if (minTime > 0.5f) {
            minTime -= Time.deltaTime / 10;
        }
        if (maxTime < 1.5f) {
            maxTime += Time.deltaTime / 100;
        }
    }

    
    void DeSpawn() {

        MoleState = states.inGround;
        animator.SetFloat("state", -1);
        OutGroundTime = Random.Range(minTime, maxTime);
        //animator.SetFloat("state", 1);
            
    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && MoleState == states.OutGround) {
            Debug.Log("Gotcha");
            MoleState = states.KnockedOut;
            
            StartCoroutine(KnockOut());
            FindObjectOfType<MoleSpawning>().score_get += 1;
            FindObjectOfType<Timer>().Add(2);
            onGround = false;            
        }
    }

    IEnumerator KnockOut() {
       
        animator.SetFloat("state", 2);      
        yield return new WaitForSeconds(4.5f);
        animator.SetFloat("state", -1);
        MoleState = states.inGround;
        DeSpawn();
    }
}
