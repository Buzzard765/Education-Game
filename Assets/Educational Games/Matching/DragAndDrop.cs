using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [System.Serializable]
    public class pieces {
        [HideInInspector] public bool isSelected = false;
        public GameObject piece;
        [HideInInspector]public float startPosX, startPosY;
        public Vector2 origin;
    }

    public pieces[] puzzlepieces;
    float position;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector2 CameraMinPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 CameraMaxPos = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));

        //var copy = new List<GameObject>();
        
        float divideX = (CameraMaxPos.x - CameraMinPos.x) / puzzlepieces.Length;
        float divideY = (CameraMaxPos.y - CameraMinPos.y) / 2;
        position = CameraMinPos.x + (divideX/2);

        /*foreach (var item in puzzlepieces)
        {
            copy.Add(Instantiate(item.piece));
        }*/

        for (int i = 0; i < puzzlepieces.Length; i++) {
            int rand = Random.Range(0, puzzlepieces.Length);
            var temp = puzzlepieces[i];
            puzzlepieces[i] = puzzlepieces[rand];
            puzzlepieces[rand] = temp;
        }

        for (int i = 0; i < puzzlepieces.Length; i++) {
            puzzlepieces[i].piece.transform.position = new Vector2(position, CameraMaxPos.y-(divideY/2));
            position += divideX;
            puzzlepieces[i].origin = puzzlepieces[i].piece.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            detection();
        }

        foreach(var item in puzzlepieces) {

            if (item.isSelected == true)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                item.piece.transform.position = new Vector2(pos.x - item.startPosX, pos.y - item.startPosY);
            }

            if (Input.GetMouseButtonUp(0))
            {
               Vector2 TargetPos = GameObject.Find(item.piece.name + "(Slot)").transform.position;                
                Vector2 ObjectSlot = item.piece.transform.position;

                if (Mathf.Abs((ObjectSlot.x - TargetPos.x ))< 0.8 && Mathf.Abs((ObjectSlot.y - TargetPos.y)) < 0.8) {
                    item.piece.transform.position = TargetPos;
                }
                else {
                    item.piece.transform.position = item.origin;
                }
                //item.piece.transform.position = item.origin;
                item.isSelected = false;
            }           
        }
        
    }

    void detection() {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D ray = Physics2D.Raycast(pos, pos * 0);
        
        if (ray) {
            foreach (var item in puzzlepieces)
            {
                if (item.piece.name == ray.collider.name) {
                    item.isSelected = true;
                    item.startPosX = pos.x - item.piece.transform.position.x;
                    item.startPosY = pos.y - item.piece.transform.position.y;
                }
            }    
        }
    }
}
