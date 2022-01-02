using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class AllAudio
{
    public string name;
    public AudioClip clip;

    [Range(0,1f)]public float volume;
    [Range(0.1f, 3f)] public float pitch;
    public bool Loop;

    [HideInInspector]public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
