﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwo : MonoBehaviour
{
    public GameObject boss;
    public GameObject manager;
    public GameObject topDownCamera;
    public GameObject bridgeCamera;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            boss.SetActive(true);
            topDownCamera.gameObject.SetActive(true);
            bridgeCamera.gameObject.SetActive(false);
            boss.GetComponent<BossController>().transitioningLevels = 0;
            manager.GetComponent<GameManager>().SwapToPlanning();
            manager.GetComponent<GameManager>().player.GetComponent<PlayerController>().baseSpeed = 12;
            Destroy(gameObject);
        }
    }
}