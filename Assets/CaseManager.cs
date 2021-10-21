using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseManager : MonoBehaviour
{
    public List<DragAndDropCase> CaseList = new List<DragAndDropCase>();
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = Random.Range(0, CaseList.Count);
        Instantiate(CaseList[index].gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Test");
        answerCheck();
        
        //Debug.Log(CaseList.Count);
        Debug.Log(CaseList[index].points);
        Debug.Log(CaseList[index].pointslimit);        
    }

    public void answerCheck()
    {
        if (CaseList[index].points == CaseList[index].pointslimit)
        {
            nextRandomCase();           
        }
    }
    public void nextRandomCase()
    {
        //Destroy(CaseList[index].gameObject);
        CaseList.RemoveAt(index);        
        index = Random.Range(0, CaseList.Count);
        Instantiate(CaseList[index].gameObject);
        
    }
}
