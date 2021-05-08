using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;


public class CameraController : MonoBehaviour
{
    public GameObject thirdPersonCamera;
    public GameObject topDownCamera;
    public GameObject bridgeCamera;
    public GameManager manager;
    public AudioClip tempMusicEnd;
    public AudioMixer mixer;
    private float gameVol;
    private float menuVol;

    void Awake()
    {
        gameObject.GetComponent<AudioSource>().Play();
        var cameraSwap = new InputAction(binding: "<Keyboard>/q");
        cameraSwap.started += cxt =>
        {
            swapMode();
        };
        cameraSwap.Enable();

        mixer.GetFloat("GameBGM", out float gameVol);
        mixer.GetFloat("MenuBGM", out float menuVol);
    }

    public void BridgeCameraSwap()
    {
        thirdPersonCamera.SetActive(false);
        bridgeCamera.SetActive(true);
    }

    public void swapMode()
    {
        if (thirdPersonCamera.activeSelf)
        {
            topDownCamera.gameObject.SetActive(true);
            thirdPersonCamera.gameObject.SetActive(false);
            manager.SwapToPlanning();

            StartCoroutine(FadeMixerGroup.StartFade(mixer, "MenuBGM", 0.75f, 1));
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "GameBGM", 0.25f, 0));
        }
        else
        {
            topDownCamera.gameObject.SetActive(false);
            thirdPersonCamera.gameObject.SetActive(true);
            manager.SwapToGameplay();

            StartCoroutine(FadeMixerGroup.StartFade(mixer, "GameBGM", 0.75f, 1));
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "MenuBGM", 0.25f, 0));
        }
    }
}
