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
    public GameObject player;
    public GameObject timeManager;
    public AudioClip tempMusicEnd;

    void Awake()
    {
        var cameraSwap = new InputAction(binding: "<Keyboard>/q");
        cameraSwap.started += cxt =>
        {
            swapMode();
        };
        cameraSwap.Enable();
        gameObject.GetComponent<AudioSource>().Pause();

    }

    public void swapMode()
    {
        if (thirdPersonCamera.activeSelf)
        {
            Debug.Log("going top down");
            topDownCamera.gameObject.SetActive(true);
            thirdPersonCamera.gameObject.SetActive(false);
            gameplayUI.SetActive(false);
            planningUI.SetActive(true);
            timeManager.SetActive(false);
            player.SetActive(false);
            planningUI.GetComponent<spawnObject>().cashMoney = 10;
            GameObject.Find("Boss").GetComponent<BossController>().planningPhase = true;
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().PlayOneShot(tempMusicEnd);
        }
        else
        {
            Debug.Log("going third person");
            player.SetActive(true);
            timeManager.SetActive(true);
            player.transform.position = new Vector3(0f, 1f, -50f);
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
            var ruinsWall = GameObject.Find("RuinsWallBlueprint");
            if(ruinsWall != null)
            {
                ruinsWall.gameObject.SetActive(false);
            }
            var teleporter = GameObject.Find("TeleporterBlueprint");
            if (teleporter != null)
            {
                teleporter.gameObject.SetActive(false);
            }
            GameObject.Find("Boss").GetComponent<BossController>().planningPhase = false;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
