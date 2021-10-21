using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDragAndDrop2 : MonoBehaviour
{
    public GameObject Slot;
    private float StartPosX, StartPosY;
    private bool isSelected, isFit;
    private Vector3 Origin;
    // Start is called before the first frame update
    void Start()
    {
        Slot = GameObject.Find(gameObject.name + "(Slot)");
        Origin = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFit == false) {
            if (isSelected == true)
            {
                Vector3 MousePos;
                MousePos = Input.mousePosition;
                MousePos = Camera.main.ScreenToWorldPoint(MousePos);
                gameObject.transform.localPosition = new Vector3(MousePos.x - StartPosX, MousePos.y - StartPosY, gameObject.transform.localPosition.z);
            }
        }      
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 MousePos;
            MousePos = Input.mousePosition;
            MousePos = Camera.main.ScreenToWorldPoint(MousePos);
            StartPosX = MousePos.x - transform.localPosition.x;
            StartPosY = MousePos.y - transform.localPosition.y;
            isSelected = true;
        }
    }

    private void OnMouseUp()
    {
        isSelected = false;
        if (Mathf.Abs(transform.localPosition.x - Slot.transform.localPosition.x) <= 0.8f &&
            Mathf.Abs(transform.localPosition.y - Slot.transform.localPosition.y) <= 0.8f)
        {
            transform.localPosition = new Vector3(Slot.transform.localPosition.x,
                Slot.transform.localPosition.y,
                Slot.transform.localPosition.z);
            isFit = true;
        }
        else
        {
            transform.localPosition = new Vector3(Origin.x, Origin.y, Origin.z);
        }
    }
}
