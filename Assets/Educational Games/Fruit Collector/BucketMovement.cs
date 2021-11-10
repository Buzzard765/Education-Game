using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketMovement : MonoBehaviour
{

    private Rigidbody2D bucketRB;
    private BoxCollider2D bucketCollider;
    [SerializeField]
    private float speed;
    


    // Start is called before the first frame update
    void Start()
    {
        
        bucketRB = GetComponent<Rigidbody2D>();
        bucketCollider = GameObject.Find("Bucket").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {       
        Movement();
    }
    private void LateUpdate()
    {
        
    }

    void Movement() {

        

        bucketRB.velocity = Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //transform.position += new Vector3(speed, 0);
            bucketRB.velocity += new Vector2(speed, 0);
            Debug.Log(bucketRB.velocity);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            // transform.position += new Vector3(-speed, 0);
            bucketRB.velocity += new Vector2(-speed, 0);
        }
        Debug.Log(bucketRB.velocity);
    }

    public void OneWayMovement(float click) {
        bucketRB.velocity += new Vector2(click, 0);
    }
    public void Flip(float rotation) {
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Fruit")) {
        //    Destroy(collision.gameObject);
        //}
    }
}
