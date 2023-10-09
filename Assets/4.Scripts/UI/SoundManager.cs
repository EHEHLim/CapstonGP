using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource btnsource;
    public AudioSource musicsource;
    private bool isMuted = false;
    private float originalVolume;

    private void Start()
    {
        originalVolume = musicsource.volume;
    }

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
   public void ToggleMute()
    {
        if (isMuted)
        {
            musicsource.volume = originalVolume;
            isMuted = false;
        }
        else
        {
            musicsource.volume = 0f;
            isMuted = true;
        }
    }

    public void ToggleEffect()
    {
        if (isMuted)
        {
            btnsource.volume = originalVolume;
            isMuted = false;
        }
        else
        {
            btnsource.volume = 0f;
            isMuted = true;
        }
    }

}
