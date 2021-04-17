using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRot : MonoBehaviour
{
    public GameObject manager;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Camera.main.GetComponent<CameraController>().BridgeCameraSwap();
            Camera.main.GetComponent<CameraController>().topDownCamera.transform.position = new Vector3(0, 55, 530.3f);
            Camera.main.GetComponent<CameraController>().topDownCamera.transform.rotation = Quaternion.Euler(52.05f, 4.2f, 0);
            manager.GetComponent<GameManager>().spawnLocation = new Vector3(0, 1, 540);
            
        }
    }
}
