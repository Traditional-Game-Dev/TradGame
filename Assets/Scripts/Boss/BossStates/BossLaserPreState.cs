using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserPreState : BossBaseState
{
    private GameObject player;
    private ParticleSystem laserWarmUp;
    private float timerDuringAttacks;
    private float timeForWarmup;
    public GameManager manager;

    public override void EnterState(BossController boss)
    {
        timerDuringAttacks = 0.0f;
        player = boss.player;
        laserWarmUp = boss.laserWarmUp;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(manager.currLevel == 2){
            laserWarmUp.transform.position = new Vector3(27, boss.transform.position.y + 5, 858);
        }
        laserWarmUp.Play();
        laserWarmUp.GetComponentInParent<AudioSource>().Play();
        timeForWarmup = boss.timeForWarmup;
        timeForWarmup = laserWarmUp.GetComponentInParent<AudioSource>().clip.length - 0.5f;
        //boss.anim.Play("LaserAnim");
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
