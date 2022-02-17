using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AllAudio[] BGM, SFX;
    public AudioMixerGroup MasterVol, BGMVol, SFXVol;
    public Slider Slider_BGM, Slider_SFX;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach (AllAudio sound in SFX) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.outputAudioMixerGroup = SFXVol;         
        }

        foreach (AllAudio music in BGM)
        {
            music.source = gameObject.AddComponent<AudioSource>();
            music.source.clip = music.clip;
            music.source.loop = music.Loop;
            music.source.volume = music.volume;
            music.source.outputAudioMixerGroup = BGMVol;
        }

        Debug.Log(PlayerPrefs.GetFloat("BGM", Slider_BGM.value));
        Debug.Log(PlayerPrefs.GetFloat("SFX", Slider_SFX.value));
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

    public void SetSFXVol(float Volume) {
        SFXVol.audioMixer.SetFloat("SFX", Mathf.Log10(Volume) * 20);
        PlayerPrefs.SetFloat("SFX", Volume);
    }

    public void SetBGMVol(float Volume)
    {
        BGMVol.audioMixer.SetFloat("BGM", Mathf.Log10(Volume) * 20);
        PlayerPrefs.SetFloat("BGM", Volume);
    }

    

    void Start()
    {        
        Slider_BGM.value = PlayerPrefs.GetFloat("BGM", Slider_BGM.value);
        Slider_SFX.value =  PlayerPrefs.GetFloat("SFX", Slider_SFX.value);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
