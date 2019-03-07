using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ControlCameraShake : MonoBehaviour {

    public float shakeDuration;
    public float shakeAmplitude;
    public float shakeFrequency;

    public float shakeElapsedTime;

    public bool spikeTrigger;
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
	// Use this for initialization
	void Start () {
        spikeTrigger = false;
        if (virtualCamera != null)
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
	}
	
	// Update is called once per frame
	void Update () {
        if (spikeTrigger)
            ShakeCamera();
       
    }

    public void ShakeCamera()
    {

        if(shakeElapsedTime > 0)
        {
          
            virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
            virtualCameraNoise.m_FrequencyGain = shakeFrequency;
            shakeElapsedTime -= Time.deltaTime;
        }
        else
        {
            virtualCameraNoise.m_AmplitudeGain = 0f;
            shakeElapsedTime = 0f;
            spikeTrigger = false;
            shakeElapsedTime = shakeDuration;
        }
    }
}
