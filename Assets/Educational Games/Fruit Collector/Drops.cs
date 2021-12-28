using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    public Sprite[] DropsSprite;
    private SpriteRenderer sprtrndr;
    public float spin;
    public int points, minValue, maxValue;
    public Core GameManager;

    AudioSource AllAudio;
    [SerializeField]AudioClip FruitSFX, TrashSFX, WasteSFX;

    // Start is called before the first frame update

    private void Awake()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<Core>();
        AllAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        sprtrndr = GetComponent<SpriteRenderer>();
        int randomsprite = Random.Range(0, DropsSprite.Length - 1);
        sprtrndr.sprite = DropsSprite[randomsprite];
        points = Random.Range(minValue, maxValue);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,spin);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (gameObject.CompareTag("Fruit") || gameObject.CompareTag("Trash"))
            {
                Destroy(gameObject);
            }           
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Fruit")) {
                Debug.Log("fruit Colletcted");
                Destroy(gameObject);
                GameManager.score += points;
                GameManager.TimeLimit += points;
                AllAudio.PlayOneShot(FruitSFX);
            }
            else if (gameObject.CompareTag("Trash"))
            {
                Debug.Log("trash Colletcted");
                Destroy(gameObject);
                GameManager.score -= points;
                AllAudio.PlayOneShot(TrashSFX);
            }
            
        }

        if (GameManager.onPlay == false) {
            Destroy(gameObject);
        }
    }
}
