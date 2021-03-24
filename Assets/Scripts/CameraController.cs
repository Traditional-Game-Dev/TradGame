using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject thirdPersonCamera;
    public GameObject topDownCamera;
    public GameManager manager;
    public AudioClip tempMusicEnd;

    // List<AudioSource> made bgMusic unhappy, but should still figure out a better solution
    private AudioSource bgMusic;
    private AudioSource laserWarmup;

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
        laserWarmup = GameObject.Find("Laser Warm Up").GetComponent<AudioSource>();
    }

    void Update()
    {
        bgMusic.pitch = Time.timeScale;
        laserWarmup.pitch = Time.timeScale;
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
