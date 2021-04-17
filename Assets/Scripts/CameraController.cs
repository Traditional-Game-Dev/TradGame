using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject thirdPersonCamera;
    public GameObject topDownCamera;
    public GameObject bridgeCamera;
    public GameManager manager;
    public AudioClip tempMusicEnd;

    private AudioSource bgMusic;

    void Awake()
    {
        var cameraSwap = new InputAction(binding: "<Keyboard>/q");
        cameraSwap.started += cxt =>
        {
            swapMode();
        };
        cameraSwap.Enable();

        bgMusic = gameObject.GetComponent<AudioSource>();
        bgMusic.Pause();
    }

    void Update()
    {

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
            //Debug.Log("going top down");
            topDownCamera.gameObject.SetActive(true);
            thirdPersonCamera.gameObject.SetActive(false);
            manager.SwapToPlanning();
            
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().PlayOneShot(tempMusicEnd);
        }
        else
        {
            //Debug.Log("going third person");
            topDownCamera.gameObject.SetActive(false);
            thirdPersonCamera.gameObject.SetActive(true);
            manager.SwapToGameplay();

            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
