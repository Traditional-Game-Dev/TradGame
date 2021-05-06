using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletHellPreAttack : BossBaseState
{
    private GameObject player;
    private float timerDuringAttacks;
    public float timeForLoading;


    public override void EnterState(BossController boss)
    {
        timerDuringAttacks = 0.0f;
        player = boss.player;
        timeForLoading = boss.timeForLoad;
        boss.anim.SetBool("playBulletAnim", true);
        boss.anim.speed = 2f;
        boss.GetComponent<AudioSource>().clip = boss.bulletWindupNoise;
        timeForLoading = boss.bulletWindupNoise.length - 0.75f;
        boss.GetComponent<AudioSource>().Play();
    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;
        if(timerDuringAttacks >= timeForLoading){
            boss.TransitionToState(boss.BulletState);
        }
    }
}
