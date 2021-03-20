using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserPreState : BossBaseState
{
    private GameObject player;
    private ParticleSystem laserWarmUp;
    private float timerDuringAttacks;
    public float timeForWarmup;

    public override void EnterState(BossController boss)
    {
        timerDuringAttacks = 0.0f;
        player = boss.player;
        laserWarmUp = boss.laserWarmUp;
        laserWarmUp.Play();
        timeForWarmup = boss.timeForWarmup;
    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;
        if(timerDuringAttacks >= timeForWarmup){
            laserWarmUp.Stop();
            boss.TransitionToState(boss.LaserAttackState);
        }
    }
}
