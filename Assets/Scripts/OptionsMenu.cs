using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour {
    
   public AudioMixer audioMixer;

   public void setVolumeMusic (float vol)
    {

        audioMixer.SetFloat("MusicVolParam", vol);

    }

    public void setVolumeEffects(float vol)
    {

        audioMixer.SetFloat("SFXVolParam", vol);

    }


}
   
