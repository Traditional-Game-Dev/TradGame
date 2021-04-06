using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private GameManager manager;

    void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            manager.justHitBoss = true;
        }
    }
}
