using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CameraRot : MonoBehaviour
{
    public AudioMixer mixer;
    public GameObject manager;
    public GameObject wallHitbox;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Camera.main.GetComponent<CameraController>().BridgeCameraSwap();
            Camera.main.GetComponent<CameraController>().topDownCamera.transform.position = new Vector3(0, 55, 530.3f);
            Camera.main.GetComponent<CameraController>().topDownCamera.transform.rotation = Quaternion.Euler(52.05f, 4.2f, 0);
            manager.GetComponent<GameManager>().playerSpawnLocation = new Vector3(0, 1, 540);
            var backdrop = GameObject.Find("brokenbackdrop");
            if(backdrop != null)
            {
                Destroy(backdrop);
            }
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "MenuBGM", 10, 1));
            StartCoroutine(FadeMixerGroup.StartFade(mixer, "GameBGM", 1, 0));
            manager.GetComponent<GameManager>().player.GetComponent<PlayerController>().baseSpeed = 20;
            foreach(GameObject tele in GameObject.FindGameObjectsWithTag("Teleporter"))
            {
                Destroy(tele);
            }
            foreach(GameObject health in GameObject.FindGameObjectsWithTag("HealthPack"))
            {
                Destroy(health);
            }
            foreach(GameObject reflect in GameObject.FindGameObjectsWithTag("Reflector"))
            {
                Destroy(reflect);
            }
            wallHitbox.SetActive(false);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            wallHitbox.SetActive(true);
        }
    }
}
