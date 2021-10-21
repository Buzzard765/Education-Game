using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiKeyObject : MonoBehaviour
{
    public LockedGoal Goal;
    // Start is called before the first frame update
    void Start()
    {
        //Goal = FindObjectOfType<LockedGoal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Goal.remainingKeys --;
            Destroy(gameObject);
        }
    }
}
