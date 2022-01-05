using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public enum state {
        left = 1,
        right = 2,
        up = 3,
        down = 4,
    }
    public Rigidbody2D rb2d;
    private Vector2 direction;
    private int hasKey;
    public static bool cleared = false;
    public static bool lose = false;
    public Joystick js;
    public int speed;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        js = FindObjectOfType<Joystick>();
        animator = GetComponent<Animator>();
        lose = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("test");
        Debug.Log(lose);
        //direction.x = Input.GetAxisRaw("Horizontal");
        //direction.y = Input.GetAxisRaw("Vertical");
        if (lose == false) {
            PlayerMovement();
        }        
    }

    void PlayerMovement() {
        direction.x = js.Horizontal * speed;
        direction.y = js.Vertical * speed;
        rb2d.MovePosition(rb2d.position + direction * speed * Time.deltaTime);
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Key")) {
            hasKey ++;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Wrong")) {
            lose = true;
        }
        if(collision.gameObject.name.Contains("Gate") && hasKey != 0) {
            hasKey --;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("Goal")) {
            Debug.Log("Cleared");
            cleared = true;            
        }
    }
}
