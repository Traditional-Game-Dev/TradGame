using System;
using UnityEngine;

public class BossStompState : BossBaseState
{
    private float bossAttackTime;

    private float timerDuringAttacks = 0.0f;

    public override void EnterState(BossController boss)
    {
        Debug.Log("STOMP");

        bossAttackTime = boss.bossAttackTime;

    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;

        if (timerDuringAttacks > bossAttackTime)
        {
            timerDuringAttacks -= bossAttackTime;
            boss.TransitionToState(boss.IdleState);
        }
    }
}
