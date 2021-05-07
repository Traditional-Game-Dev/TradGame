using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneScript : MonoBehaviour
{
    private GameManager manager;

    private void OnEnable()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        manager.playerDisabled = true;
        manager.playerIvin = true;

        foreach (GameObject poisonBomb in GameObject.FindGameObjectsWithTag("Bomb"))
        {
            Destroy(poisonBomb);
        }
    }

    private void OnDisable()
    {
        manager.playerDisabled = false;
        manager.playerIvin = false;
    }
}
