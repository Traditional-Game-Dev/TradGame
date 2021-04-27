using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour {
    
   public AudioMixer audioMixer;

   public void setVolumeMusic (float vol)
    {
        audioMixer.SetFloat("MusicVolParam", Mathf.Log10(vol) * 20);
    }

    public void setVolumeEffects(float vol)
    {
        audioMixer.SetFloat("SFXVolParam", Mathf.Log10(vol) * 20);
    }

    public void setVolumeAmbient(float vol)
    {
        audioMixer.SetFloat("AmbientVolParam", Mathf.Log10(vol) * 20);
    }

    public void setVolumeMaster(float vol)
    {
        audioMixer.SetFloat("MasterVolParam", Mathf.Log10(vol) * 20);
    }
}
   
