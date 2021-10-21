using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDragAndDrop : MonoBehaviour
{
    private float StartPosX, StartPosY;
    private bool isSelected = false, isFit = false;
    public Vector2 Origin;
    public GameObject slot;
    private DragAndDropCase CaseScore;

    private void Awake()
    {
        //slot = GameObject.Find(this.gameObject.name + "(Slot)");
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //Debug.Log(this.gameObject.name);
        Origin = transform.position;
        CaseScore = FindObjectOfType<DragAndDropCase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFit == false) {
            if (isSelected == true)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                gameObject.transform.localPosition = new Vector3(mousePos.x - StartPosX, mousePos.y - StartPosY, transform.position.z);
            }
        }        
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);            
            StartPosX = mousePos.x - transform.localPosition.x;
            StartPosY = mousePos.y - transform.localPosition.y;
            isSelected = true;
        }
    }

    private void OnMouseUp()
    {
        isSelected = false;
        Vector2 TargetPos = slot.transform.position;
        Vector2 ObjectSlot = transform.position;
        Debug.Log(TargetPos);
        Debug.Log(ObjectSlot);
        //float jarak = Vector3.Distance(ObjectSlot, TargetPos); 
        //if (jarak < 0.8f) 
        if (Mathf.Abs((ObjectSlot.x - TargetPos.x)) < 0.8f && Mathf.Abs((ObjectSlot.y - TargetPos.y)) < 0.8f)
        {
            transform.position = TargetPos;
            isFit = true;
            CaseScore.points++;
            Debug.Log("fits");
        }
        else
        {
            transform.position = Origin;
            Debug.Log("not fit");
        }
        
    }

    private void OnMouseEnter()
    {
        
    }


}
