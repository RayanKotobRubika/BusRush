using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] MusicSounds, SFXSounds;
    public AudioSource MusicSource, SFXSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("ActivateMusic") == 0 && MusicSource.isPlaying) MusicSource.Stop(); 
        if (PlayerPrefs.GetInt("ActivateSfx") == 0 && SFXSource.isPlaying) SFXSource.Stop(); 
    }

    public void PlayMusic(string name)
    {
        if (PlayerPrefs.GetInt("ActivateMusic") == 0) return; 
        
        Sound s = Array.Find(MusicSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            MusicSource.clip = s.Clip;
            MusicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        if (PlayerPrefs.GetInt("ActivateSfx") == 0) return; 
        
        Sound s = Array.Find(SFXSounds, x => x.Name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            SFXSource.PlayOneShot(s.Clip);
            SFXSource.Play();
        }
    }
}