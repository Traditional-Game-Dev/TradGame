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
    private float random;

    private readonly float windUpTime = 1.0f;


    public override void EnterState(BossController boss)
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

        GameObject projectile = GameObject.Instantiate(boss.genericProjectile);
        projectileScript = projectile.GetComponent<Projectile>();

        stompRadius = boss.stompRadius;
        shockwave = boss.shockwave;

        timerDuringAttacks = 0.0f;
        triedStomp = false;

        boss.player.GetComponent<PlayerController>().anim.SetTrigger("Launch");

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

            if(manager.currLevel == 2){
                random = UnityEngine.Random.value;
                if(boss.currPosition == 1){
                    if(random < 0.5){
                        boss.currPosition = 2;
                        boss.transform.position = new Vector3(27, boss.transform.position.y, 858);
                    }
                    else if(random >= 0.51){
                        boss.currPosition = 3;
                        boss.transform.position = new Vector3(122, boss.transform.position.y, 806);
                    }
                }
                else if(boss.currPosition == 2){
                    if(random < 0.5){
                        boss.currPosition = 1;
                        boss.transform.position = new Vector3(-65, boss.transform.position.y, 867);
                    }
                    else if(random >= 0.51){
                        boss.currPosition = 3;
                        boss.transform.position = new Vector3(122, boss.transform.position.y, 806);
                    }
                }
                else{
                    if(random < 0.5){
                        boss.currPosition = 1;
                        boss.transform.position = new Vector3(-65, boss.transform.position.y, 867);
                    }
                    else if(random >= 0.51){
                        boss.currPosition = 2;
                        boss.transform.position = new Vector3(27, boss.transform.position.y, 858);
                    }
                }
            }

            triedStomp = true;
        }
    }
}
