﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPoisonPreState : BossBaseState
{
    private GameObject player;
    private float timerDuringAttacks;
    public float timeForAim;

    public override void EnterState(BossController boss)
    {
        timerDuringAttacks = 0.0f;
        player = boss.player;
        timeForAim = boss.timeForAim;
        //TODO: Transition Animation to pre poison ball animation here
    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;
        if(timerDuringAttacks >= timeForAim){
            //TODO: Transition Animation back to Idle here
            boss.TransitionToState(boss.PoisonState);
        }
    }
}