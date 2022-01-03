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
        foreach (AllAudio sound in SFX) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            
        }

        foreach (AllAudio music in BGM)
        {
            music.source = gameObject.AddComponent<AudioSource>();
            music.source.clip = music.clip;
            music.source.loop = music.Loop;
            music.source.volume = music.volume;
        }
    }

    public void PlaySound(string name)
    {
        AllAudio selectedSound = Array.Find(SFX, music => music.name == name);
        selectedSound.source.Play();
    }

    public void PlayMusic(string name) {
        AllAudio selectedMusic = Array.Find(BGM, music => music.name == name);
        selectedMusic.source.Play();
    }

    public void StopMusic(string name)
    {
        AllAudio selectedMusic = Array.Find(BGM, music => music.name == name);
        selectedMusic.source.Stop();
    }

    public void OneShotMusic(string name) {
        AllAudio selectedMusic = Array.Find(BGM, music => music.name == name);
        selectedMusic.source.PlayOneShot(selectedMusic.clip);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
