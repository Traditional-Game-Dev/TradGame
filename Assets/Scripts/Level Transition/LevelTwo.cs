using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwo : MonoBehaviour
{
    public GameObject boss;
    public GameObject manager;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            boss.SetActive(true);
            manager.GetComponent<GameManager>().SwapToPlanning();
        }
    }
}