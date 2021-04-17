using System;
using UnityEngine;

public class BossStompState : BossBaseState
{
    private float bossAttackTime;

    private float timerDuringAttacks = 0.0f;

    public override void EnterState(BossController boss)
    {
        Debug.Log("STOMP");
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
    }
}
