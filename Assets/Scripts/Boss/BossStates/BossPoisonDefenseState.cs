﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BossPoisonDefenseState : BossBaseState
{
    private GameObject player;
    private GameObject poisonObject;
    private GameObject poisonBomb;
    private GameObject poisonBomb1;
    private GameObject poisonBomb2;
    private GameObject poisonBomb3;


    private float bossAttackTime;
    private float emissionDuration;
    private float poisonRadius;
    private int poisonDamage;

    private float timerDuringAttacks = 0.0f;


    public override void EnterState(BossController boss)
    {
        //TODO: New Anim Plays Here
        poisonObject = boss.poisonObject;
        bossAttackTime = boss.bossAttackTime;
        emissionDuration = boss.emissionDuration;
        poisonRadius = boss.poisonRadius;
        poisonDamage = boss.poisonDamage;

        player = boss.player;

        poisonObject.transform.position = boss.transform.position + new Vector3(1.9f, 5.8f, 0.0f);
        poisonBomb = GameObject.Instantiate(poisonObject);
        poisonBomb1 = GameObject.Instantiate(poisonObject);
        poisonBomb2 = GameObject.Instantiate(poisonObject);
        poisonBomb3 = GameObject.Instantiate(poisonObject);

        poisonBomb.GetComponentInChildren<VisualEffect>().Stop(); // bad
        poisonBomb1.GetComponentInChildren<VisualEffect>().Stop(); // bad
        poisonBomb2.GetComponentInChildren<VisualEffect>().Stop(); // bad
        poisonBomb3.GetComponentInChildren<VisualEffect>().Stop(); // bad


        PoisonAttack();
    }

    public override void Update(BossController boss)
    {
        //boss.anim.speed = 0f;
        timerDuringAttacks += Time.deltaTime;
        if (timerDuringAttacks > 0.75f && timerDuringAttacks < 1.5f){
            boss.anim.speed = 0f;
        }
        else{
            boss.anim.speed = 1f;
        }
        if (timerDuringAttacks > bossAttackTime)
        {
            timerDuringAttacks -= bossAttackTime;
            boss.TransitionToState(boss.IdleState);
        }
    }

    private void PoisonAttack()
    {
        poisonBomb.GetComponent<PoisonProjectile>().Begin(player, new Vector2(5, 5), emissionDuration, poisonRadius, poisonDamage);
        poisonBomb1.GetComponent<PoisonProjectile>().Begin(player, new Vector2(-5, 5), emissionDuration, poisonRadius, poisonDamage);
        poisonBomb2.GetComponent<PoisonProjectile>().Begin(player, new Vector2(5, -5), emissionDuration, poisonRadius, poisonDamage);
        poisonBomb3.GetComponent<PoisonProjectile>().Begin(player, new Vector2(-5, -5), emissionDuration, poisonRadius, poisonDamage);
    }
}
