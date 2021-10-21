using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int ID;
    bool isSelected;
    private Camera cam;
    Vector3 Origin;

    private void Awake()
    {
        cam = Camera.main;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected == true) {
            var newPos = cam.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
    }

    private void OnMouseDown()
    {
        Origin = transform.position;
        isSelected = true;
        Debug.Log("OnMouseDown");
    }
    private void OnMouseDrag()
    {
        Debug.Log("OnMouseDrag");
    }
    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
    }
    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
    }
    private void OnMouseUp()
    {
        isSelected = false;
        transform.position = Origin;
        Debug.Log("OnMouseUp");
    }
}
