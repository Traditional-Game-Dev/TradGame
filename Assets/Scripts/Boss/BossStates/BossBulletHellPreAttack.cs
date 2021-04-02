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
        //TODO: Transition Animation to pre poison ball animation here
    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;
        if(timerDuringAttacks >= timeForLoading){
            //TODO: Transition Animation back to Idle here
            boss.anim.SetBool("playBulletAnim", false);
            boss.TransitionToState(boss.BulletState);
        }
    }
}
