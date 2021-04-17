using System;
using UnityEngine;

public class BossStompPreState : BossBaseState
{
    private float timeForLoading;

    private float timerDuringAttacks;

    public override void EnterState(BossController boss)
    {
        timerDuringAttacks = 0.0f;
        timeForLoading = boss.timeForLoad;

    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;
        if (timerDuringAttacks > timeForLoading)
        {
            boss.TransitionToState(boss.StompState);
        }
    }
}

