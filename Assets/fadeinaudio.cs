using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class fadeinaudio : MonoBehaviour
{
    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeMixerGroup.StartFade(mixer, "MenuBGM", 2, 1));
    }
}
