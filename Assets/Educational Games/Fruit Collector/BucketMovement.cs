using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class BucketMovement : MonoBehaviour
{

    private Rigidbody2D bucketRB;
    private BoxCollider2D bucketCollider;
    [SerializeField]
    private float speed;
    float direction;
    bool flip;
    Animator allAni;

    AudioSource allAudio;
    AudioClip TroupeSFX;

    // Start is called before the first frame update
    void Start()
    {
        allAni = GetComponent<Animator>();
        bucketRB = GetComponent<Rigidbody2D>();
        bucketCollider = GameObject.Find("Bucket").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = CrossPlatformInputManager.GetAxis("Horizontal") * speed;
        Movement();
        OneWayMovement();
        allAni.SetFloat("Speed", 0);
    }
    private void LateUpdate()
    {
        Flip();        
    }

    void Movement() {

        bucketRB.velocity = Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //transform.position += new Vector3(speed, 0);
            bucketRB.velocity += new Vector2(speed, 0);
            allAni.SetFloat("Speed", 2);
            Debug.Log(bucketRB.velocity);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            // transform.position += new Vector3(-speed, 0);
            bucketRB.velocity += new Vector2(-speed, 0);
            allAni.SetFloat("Speed", 2);
        }
        Debug.Log(bucketRB.velocity);
    }

    private void FixedUpdate()
    {
        
    }

    public void OneWayMovement() {
        bucketRB.velocity = new Vector2(direction * speed, bucketRB.velocity.y);
        int rotation = (int)direction;
        allAni.SetFloat("Speed", 2);
        //allAudio.PlayOneShot(TroupeSFX);
        //bucketRB.MovePosition(bucketRB.position + direction * speed * Time.deltaTime);
    }
    public void Flip() {
        if (direction >= 1)
        {
            flip = false;
        }
        else
        {

            if (direction <= -1)
            {
                flip = true;
            }
        }
        if (flip == true)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Fruit")) {
        //    Destroy(collision.gameObject);
        //}
    }
}
