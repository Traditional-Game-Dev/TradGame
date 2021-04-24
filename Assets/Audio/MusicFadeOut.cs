using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicFadeOut : MonoBehaviour
{
    public AudioMixer mixer;
    void OnEnable()
    {
        StartCoroutine(FadeMixerGroup.StartFade(mixer, "MusicVolParam", 1, 0));
    }
}
