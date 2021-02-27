using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject thirdPersonCamera;
    public GameObject topDownCamera;
    public GameObject gameplayUI;
    public GameObject planningUI;

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
                gameplayUI.SetActive(false);
                planningUI.SetActive(true);
            }
            else
            {
                Debug.Log("going third person");
                topDownCamera.gameObject.SetActive(false);
                thirdPersonCamera.gameObject.SetActive(true);
                gameplayUI.SetActive(true);
                planningUI.SetActive(false);
                var rock = GameObject.Find("Rock1Blueprint");
                if(rock != null)
                {
                    rock.gameObject.SetActive(false);
                }
                var wall = GameObject.Find("WallBlueprint");
                if(wall != null)
                {
                    wall.gameObject.SetActive(false);
                }
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
