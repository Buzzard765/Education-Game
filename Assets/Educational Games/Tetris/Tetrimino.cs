using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrimino : MonoBehaviour
{
    float fall = 0;
    public float fallspeed = 0.5f;

    public bool canRotate = false, limitRotaton = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        Drop(1);                  
    }

    //controls for android   
    public void MoveXPos(float x ) {
        transform.position += new Vector3(x, 0, 0);
        if (checkPos())
        {

        }
        else {
            transform.position += new Vector3(-x, 0, 0);
        }
    }

    public void RotatePos() {
        if (canRotate == true) {
            if (limitRotaton == true)
            {
                if (transform.rotation.eulerAngles.z >= 90)
                {
                    transform.Rotate(0, 0, -90);
                }
                else {
                    transform.Rotate(0, 0, 90);
                }
            }
            else {
                transform.Rotate(0, 0, 90);
            }
            if (checkPos())
            {

            }
            else
            {
                if (limitRotaton == true)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else {
                    transform.Rotate(0, 0, -90);
                }         
            }
        }
        //transform.Rotate(0, 0, 90);       
    }

    public void Drop(float y)
    {
        if (Time.time - fall >= fallspeed)
        {
            transform.position += new Vector3(0, -y, 0);
            if (checkPos())
            {

            }
            else
            {
                Debug.Log("next");
                transform.position += new Vector3(0, y, 0);
                enabled = false;
                FindObjectOfType<GManager>().spawnTetrimino();                
            }
            fall = Time.time;
            Debug.Log(fall);
        }        
    }
    bool checkPos() {
        foreach (Transform mino in transform) {
            Vector2 pos = FindObjectOfType<GManager>().Round(mino.position);

            if (FindObjectOfType<GManager>().insideGrid(pos) == false)
            {
                return false;
            }
            else {

            }
            
        }
        return true;
    }

}
