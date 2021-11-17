using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mole : MonoBehaviour
{
    private float OutGroundTime;
    private float UnderGround;
    public float startUG, startOG;
    // Start is called before the first frame update
    void Start()
    {
        OutGroundTime = Random.Range(0.5f, 3);
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
    }

    
    void DeSpawn() {
        gameObject.SetActive(false);
        OutGroundTime = Random.Range(0.5f, 3);
        
    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Gotcha");
            DeSpawn();
            FindObjectOfType<MoleSpawning>().score_get += 1;
            FindObjectOfType<MoleSpawning>().time_get += 1;
        }
    }

    IEnumerator KnockOut() {
        yield return new WaitForSeconds(1);
    }
}
