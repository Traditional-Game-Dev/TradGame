﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject thirdPersonCamera;
    public GameObject topDownCamera;

    void Awake()
    {
        var cameraSwap = new InputAction(binding: "<Keyboard>/q");
        cameraSwap.started += cxt =>
        {
            if (thirdPersonCamera.activeSelf)
            {
                Debug.Log("going top down");
                topDownCamera.gameObject.SetActive(true);
                thirdPersonCamera.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("going third person");
                topDownCamera.gameObject.SetActive(false);
                thirdPersonCamera.gameObject.SetActive(true);
            }
        };
        cameraSwap.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
