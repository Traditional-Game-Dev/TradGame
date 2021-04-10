using Assets.Scripts.Boss.BossEnums;
using System.Collections;
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
        //boss.anim.Play("PoisonAnim");
        //TODO: Transition Animation to pre poison ball animation here
    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;
        if(timerDuringAttacks >= timeForAim){
            //TODO: Transition Animation back to Idle here
            boss.anim.SetBool("playPoisonAnim", true);
            switch(boss.poisonType)
            {
                case PoisonType.Normal:
                    boss.TransitionToState(boss.PoisonState);
                break;

                case PoisonType.Defense:
                    boss.TransitionToState(boss.PoisonDefenseState);
                break;

                case PoisonType.Far:
                    boss.TransitionToState(boss.PoisonFarState);
                break;
            }
        }
    }
}
