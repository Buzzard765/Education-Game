using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] Image Fadings;
    [SerializeField] Color StartColor;
    float FadingTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
        StartColor = Fadings.color;
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator FadeIn() {
        FadingTime = Fadings.color.a;
        //float FadeTime;
        print("test");
        
        while (FadingTime > 0)
        {
            FadingTime -= Time.deltaTime /10;
            Fadings.color = new Color(Fadings.color.r, Fadings.color.g, Fadings.color.b, FadingTime);
            yield return null;
        }
    }
}
