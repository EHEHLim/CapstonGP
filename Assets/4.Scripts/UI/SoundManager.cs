using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource btnsource;
    public AudioSource musicsource;

    public void SetMusicVolume(float volume)
    {
        musicsource.volume = volume;
    }

    public void OnSfx()
    {
        btnsource.Play();
    }
    public void SetButtonVolume(float volume)
    {
        btnsource.volume = volume;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            btnsource.Play();
        }
    }
    
    public void ToggleAudio()
    {
        if (musicsource.isPlaying)
        {
            musicsource.Stop();
        }
        else
        {
            musicsource.Play();
        }
    }

}
