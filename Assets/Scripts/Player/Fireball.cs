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

    //int frameCount = 0;
    //int maxCount = 15;

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Boss")
    //    {
    //        frameCount++;
    //        if (frameCount > maxCount)
    //        {
    //            manager.justHitBoss = true;
    //            frameCount = 0;
    //        }
    //    }
    //}
}
