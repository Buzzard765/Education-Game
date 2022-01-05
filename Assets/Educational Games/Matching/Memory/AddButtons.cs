using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField]private Transform PuzzleField;
    [SerializeField] private GameObject Button;

    public int amount;
    // Start is called before the first frame update

    private void Awake()
    {
        for (int i = 0; i < amount; i++) {
            GameObject btn = Instantiate(Button);
            btn.name = "" + i;
            btn.transform.SetParent(PuzzleField);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
