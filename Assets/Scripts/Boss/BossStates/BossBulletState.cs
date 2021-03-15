using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletState : BossBaseState
{

    private float bossAttackTime;
    private float bulletStartDegree;
    private float bulletEndDegree;
    private int bulletDamage;
    private int bulletCount;

    private float timerDuringAttacks = 0.0f;
    public override void EnterState(BossController boss)
    {
        //TODO: New Anim Plays Here

        bossAttackTime = boss.bossAttackTime;
        bulletStartDegree = boss.bulletStartDegree;
        bulletEndDegree = boss.bulletEndDegree;
        bulletDamage = boss.bulletDamage;
        bulletCount = boss.bulletCount;
    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;
        if (timerDuringAttacks > bossAttackTime)
        {
            boss.TransitionToState(boss.IdleState);
        }

        //TODO: Spawn bullets

        throw new System.NotImplementedException();
    }
}
