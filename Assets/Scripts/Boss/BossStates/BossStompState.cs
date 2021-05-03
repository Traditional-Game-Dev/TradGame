using System;
using UnityEngine;
using UnityEngine.VFX;

public class BossStompState : BossBaseState
{
    private GameManager manager;
    private VisualEffect shockwave;
    private Projectile projectileScript;
    private float bossAttackTime;
    private float stompRadius;

    
    private float timerDuringAttacks;
    private bool triedStomp;

    private readonly float windUpTime = 1.0f;


    public override void EnterState(BossController boss)
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        GameObject projectile = GameObject.Instantiate(boss.genericProjectile);
        projectileScript = projectile.GetComponent<Projectile>();

        timerDuringAttacks = 0.0f;
        triedStomp = false;

        stompRadius = boss.stompRadius;
        shockwave = boss.shockwave;

        boss.anim.SetTrigger("playStompAnim");
        bossAttackTime = boss.bossAttackTime;
        boss.anim.speed = 3;
    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;

        if (timerDuringAttacks > bossAttackTime)
        {
            boss.anim.speed = 1;
            boss.TransitionToState(boss.IdleState);
        }

        if (!triedStomp && timerDuringAttacks > windUpTime)
        {
            if (!shockwave.gameObject.activeSelf)
            {
                shockwave.gameObject.SetActive(true);
            }
            shockwave.gameObject.transform.position = boss.transform.position - new Vector3(0.0f, 4.1f, 0.0f);
            shockwave.Play();

            if (Vector3.Distance(boss.transform.position, boss.player.transform.position) < stompRadius)
            {
                projectileScript.Begin(boss.player.transform, manager.playerSpawnLocation, new Vector2(), 45.0f, 29.4f);
            }

            triedStomp = true;
        }
    }
}
