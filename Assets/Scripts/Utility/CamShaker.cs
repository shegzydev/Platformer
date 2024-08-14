using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShaker : MonoBehaviour
{
    float Amplitude;
    static CamShaker instance;
    public CinemachineVirtualCamera VirtualCamera;

    float maxShakeTime;
    float shakeTime;
    float shakeStrength;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if (maxShakeTime > 0.0001f)
        {
            Amplitude = Mathf.Sin(Mathf.Deg2Rad * (shakeTime / maxShakeTime) * 180) * shakeStrength;
            shakeTime -= Time.deltaTime;
            shakeTime = Mathf.Clamp(shakeTime, 0, maxShakeTime);
        }

        VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Amplitude;
    }

    public static void Shake(float strength, float time = 0.15f)
    {
        instance.maxShakeTime = time;
        instance.shakeTime = time;
        instance.shakeStrength = strength;
    }
}
