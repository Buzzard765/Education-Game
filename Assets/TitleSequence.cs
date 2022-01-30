using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSequence : MonoBehaviour
{
    [SerializeField] private GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SequenceFade(Panel));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SequenceFade(GameObject Panel) {
        yield return new WaitForSeconds(5f);
        Panel.SetActive(true);
    }
}
