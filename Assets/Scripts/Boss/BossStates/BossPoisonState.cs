using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPoisonState : BossBaseState
{
    private GameObject player;
    private GameObject poisonObject;
    private GameObject poisonBomb;

    private float bossAttackTime;
    private float timerDuringAttacks = 0.0f;

    private float emissionDuration;
    private float poisonRadius;
    private int poisonDamage;


    public override void EnterState(BossController boss)
    {
        //TODO: New Anim Plays Here

        poisonObject = boss.poisonObject;
        bossAttackTime = boss.bossAttackTime;
        emissionDuration = boss.emissionDuration;
        poisonRadius = boss.poisonRadius;
        poisonDamage = boss.poisonDamage;

        player = boss.player;

        poisonObject.transform.position = boss.transform.position + new Vector3(0.0f, 5.5f, 0.0f);
        poisonBomb = GameObject.Instantiate(poisonObject);
        poisonBomb.GetComponentInChildren<ParticleSystem>().Stop(); // bad

        PoisonAttack();
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

    private void PoisonAttack()
    {
        poisonBomb.GetComponent<PoisonProjectile>().Begin(player, emissionDuration, poisonRadius, poisonDamage);
    }
}
