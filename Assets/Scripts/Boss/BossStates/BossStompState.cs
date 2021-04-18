using System;
using UnityEngine;

public class BossStompState : BossBaseState
{
    private float bossAttackTime;
    private float stompRadius;

    private float timerDuringAttacks = 0.0f;
    private float windUpTime = 1.0f;
    private bool triedStomp = false;

    public override void EnterState(BossController boss)
    {
        stompRadius = boss.stompRadius;

        boss.anim.SetTrigger("playStompAnim");
        bossAttackTime = boss.bossAttackTime;
        boss.anim.speed = 3;
    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;

        if (timerDuringAttacks > bossAttackTime)
        {
            timerDuringAttacks -= bossAttackTime;
            boss.anim.speed = 1;
            boss.TransitionToState(boss.IdleState);
        }

        if (!triedStomp && timerDuringAttacks > windUpTime)
        {
            if (Vector3.Distance(boss.transform.position, boss.player.transform.position) < stompRadius)
            {

            }

            triedStomp = true;
        }
    }
}
