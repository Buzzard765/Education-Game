using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AllAudio[] BGM, SFX;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach (AllAudio sound in BGM) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
        }

        foreach (AllAudio music in SFX)
        {
            music.source = gameObject.AddComponent<AudioSource>();
            music.source.clip = music.clip;
        }
    }

    public void PlayMusic(string name) {
        AllAudio selectedMusic = Array.Find(BGM, music => music.name == name);
        selectedMusic.source.Play();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
