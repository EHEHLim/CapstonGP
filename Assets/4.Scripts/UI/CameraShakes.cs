using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakes : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeIntensity = 5f;
    private float shakeTime = 0.2f;

    private bool attacked = false;
    private bool hitMonster = false;

    private float timer;
    private CinemachineBasicMultiChannelPerlin _cvmcp;

    public void Attacked()
    {
        attacked = true;
    }
  
    private void Start()
    {
        stopShake();
    }
    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin _cvmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cvmcp.m_AmplitudeGain = shakeIntensity;

        timer = shakeTime;
    }

    void stopShake()
    {
        CinemachineBasicMultiChannelPerlin _cvmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cvmcp.m_AmplitudeGain = 0f;

        timer = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (attacked)
        {
            ShakeCamera();
            attacked = false;
        }
       
        if(timer > 0)
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                stopShake();
            }
        }
    }
}
