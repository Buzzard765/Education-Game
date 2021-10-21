using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropCase : MonoBehaviour
{
    public int points, pointslimit;
    public ItemDragAndDrop[] pieces;       
    
    float position;
    // Start is called before the first frame update
    void Start()
    {
        GroupDivide();
    }

    public void GroupDivide() {
        Vector2 CameraMinPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 CameraMaxPos = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));

        var copy = new List<GameObject>();

        float divideX = (CameraMaxPos.x - CameraMinPos.x) / pieces.Length;
        float divideY = (CameraMaxPos.y - CameraMinPos.y) / 2;
        position = CameraMinPos.x + (divideX / 2);

        for (int i = 0; i < pieces.Length; i++)
        {
            int rand = Random.Range(0, pieces.Length);
            var temp = pieces[i];
            pieces[i] = pieces[rand];
            pieces[rand] = temp;
        }

        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].transform.position = new Vector2(position, CameraMaxPos.y - (divideY / 2));
            position += divideX;
            pieces[i].Origin = pieces[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(points);
        Debug.Log(pointslimit);
        if (points == pointslimit) {
            Destroy(gameObject);
        }
    }

    
}
