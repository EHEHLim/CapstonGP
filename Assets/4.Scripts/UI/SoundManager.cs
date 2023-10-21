using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource btnsource;
    public AudioSource musicsource;
    private bool isMuted = false;
    private bool isMutedEffect = false;
    private float originalVolume;
    private float originalEffect;

    private void Awake()
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
            originalVolume = musicsource.volume;
            musicsource.volume = 0f;
            isMuted = true;
        }
    }

    public void ToggleEffect()
    {
        if (isMutedEffect)
        {
            btnsource.volume = originalEffect;
            isMutedEffect = false;
        }
        else
        {
            originalEffect = btnsource.volume;
            btnsource.volume = 0f;
            isMutedEffect = true;
        }
    }

}
