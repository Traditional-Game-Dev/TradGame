using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TimeManager : MonoBehaviour
{
    public float slowMo = 0.05f;
    
    private float defaultFixedDelta;
    private float slowdownLength = 1.0f;

    public AudioMixer mixer;

    void Start()
    {
        defaultFixedDelta = Time.fixedDeltaTime;
    }

    void Update()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

        Time.fixedDeltaTime += (defaultFixedDelta / slowdownLength) * Time.unscaledDeltaTime;
        Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0f, defaultFixedDelta);
    }

    public void DoSlowMotion(float length)
    {
        slowdownLength = length;

        Time.timeScale = slowMo;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        // --- wip
        //AudioMixerGroup pitchBlendGroup = (AudioMixerGroup)Resources.Load("PrimaryMixer");
        //mixer.outputAudioMixerGroup = pitchBlendGroup;

        //mixer.pitch = 0.5f;
        //pitchBlendGroup.audioMixer.SetFloat("PitchBlend", 1.0f / 0.5f);
    }
}
