using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utility;
public class Mole : MonoBehaviour
{
    private float OutGroundTime;
    private float UnderGround;
    public float minTime, maxTime;
    private Animator animator;

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
        
        if (gameObject.activeSelf == true) {
            if (OutGroundTime < 0)
            {
                DeSpawn();               
            }
            else {
                OutGroundTime -= Time.deltaTime;
            }
        }

        if (minTime > 0.5f) {
            minTime -= Time.deltaTime / 100;
        }
        if (maxTime < 5) {
            maxTime += Time.deltaTime / 100;
        }
    }

    
    void DeSpawn() {
        gameObject.SetActive(false);
        animator.SetFloat("state", 0);
        OutGroundTime = Random.Range(0.5f, 3);
        animator.SetFloat("state", 1);

    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Gotcha");
            AllAudio.PlayOneShot(SFX_Knock);
            StartCoroutine(KnockOut());
            FindObjectOfType<MoleSpawning>().score_get += 1;
            FindObjectOfType<Timer>().Add(3);
        }
    }

    IEnumerator KnockOut() {
        animator.SetFloat("state", 2);      
        yield return new WaitForSeconds(4.5f);
        animator.SetFloat("state", 0);
        DeSpawn();
    }
}
