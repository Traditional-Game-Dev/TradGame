using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject player;
    public float bossAttackTime;
    private float timer = 0.0f;
    public GameObject laser;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Check if we have passed 5 seconds
        if(timer > bossAttackTime)
        {
            bossFire();
            timer = timer - bossAttackTime;
        }
    }

    void bossFire()
    {
        return;
    }
}
