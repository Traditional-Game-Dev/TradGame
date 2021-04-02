using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState
{
    private float timerDuringAttacks = 0;

    public override void EnterState(BossController boss)
    {
        //boss.anim.Play("Idle");
        boss.anim.SetBool("playLaserAnim", false);
        boss.anim.SetBool("playBulletAnim", false);
        boss.anim.SetBool("playPoisonAnim", false);
    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;
        if(timerDuringAttacks > boss.bossWaitTime)
        {
            float distance = Vector3.Distance(boss.transform.position, boss.player.transform.position);
            //Always Shoot Laser First
            if(boss.prevAttack == "first")
            {
                boss.prevAttack = "laser";
                boss.TransitionToState(boss.LaserPreState);
            }
            //Really Close to Boss
            //Always shoots poison, only attack that can get this close
            else if(distance < 15)
            {
                boss.prevAttack = "poison";
                boss.poisonAttackType = "defense";
                boss.TransitionToState(boss.PoisonPreState);
            }
            //Sorta Close to boss, shoots laser or poison
            else if(distance < 25)
            {
                switch(boss.prevAttack)
                {
                    case "bullets":
                        if((Random.value > 0.5f))
                        {
                            boss.prevAttack = "laser";
                            boss.TransitionToState(boss.LaserPreState);
                        }
                        else
                        {
                            boss.prevAttack = "poison";
                            boss.poisonAttackType = "normal";
                            boss.TransitionToState(boss.PoisonPreState);
                        }
                    break;

                    case "poison":
                        boss.prevAttack = "laser";
                        boss.TransitionToState(boss.LaserPreState);
                    break;

                    case "laser":
                        boss.prevAttack = "poison";
                        boss.poisonAttackType = "normal";
                        boss.TransitionToState(boss.PoisonPreState);
                    break;
                }
            }
            //Midrange shoots all attacks
            //Bullets aim towards player in cone
            else if(distance < 100)
            {
                switch(boss.prevAttack)
                {
                    case "bullets":
                        if((Random.value > 0.5f))
                        {
                            boss.prevAttack = "laser";
                            fireBullets(boss.width, boss);
                            boss.TransitionToState(boss.BulletPreState);

                        }
                        else
                        {
                            boss.prevAttack = "poison";
                            boss.poisonAttackType = "normal";
                            boss.TransitionToState(boss.PoisonPreState);
                        }
                    break;

                    case "poison":
                        if((Random.value > 0.5f))
                        {
                            boss.prevAttack = "laser";
                            boss.TransitionToState(boss.LaserPreState);
                        }
                        else
                        {
                            boss.prevAttack = "bullets";
                            fireBullets(boss.width, boss);
                            boss.TransitionToState(boss.BulletPreState);

                        }
                    break;

                    case "laser":
                        if((Random.value > 0.5f))
                        {
                            boss.prevAttack = "bullets";
                            fireBullets(boss.width, boss);
                            boss.TransitionToState(boss.BulletPreState);

                        }
                        else
                        {
                            boss.prevAttack = "poison";
                            boss.poisonAttackType = "normal";
                            boss.TransitionToState(boss.PoisonPreState);
                        }
                    break;
                }
            }
            //Far Range
            //Fires bullets in 180(ish) towards player or poison
            //TODO: POISON TRI SHOT
            else
            {
                switch(boss.prevAttack)
                {
                    case "laser":
                        if((Random.value > 0.5f))
                        {
                            boss.prevAttack = "bullets";
                            fireBullets(165f, boss);
                            boss.TransitionToState(boss.BulletPreState);

                        }
                        else
                        {
                            boss.prevAttack = "poison";
                            boss.poisonAttackType = "far";
                            boss.TransitionToState(boss.PoisonPreState);
                        }
                    break;

                    case "poison":
                        boss.prevAttack = "bullets";
                        fireBullets(165f, boss);
                        boss.TransitionToState(boss.BulletPreState);
                    break;

                    case "bullets":
                        boss.prevAttack = "poison";
                        boss.poisonAttackType = "far";
                        boss.TransitionToState(boss.PoisonPreState);
                    break;
                }
            }
        }   
    }

    private void fireBullets(float width, BossController boss)
    {
        if(width > 150)
        {
            if(boss.player.transform.position.x < boss.transform.position.x)
            {
                boss.bulletStartDegree = 180 - (width/2);
                boss.bulletEndDegree = 180 + (width/2);
            }
            else
            {
                boss.bulletStartDegree = 0 - (width/2);
                boss.bulletEndDegree = 0 + (width/2);
            }
        }
        else
        {
            Vector2 total = new Vector2(boss.transform.position.x, boss.transform.position.z) - new Vector2(boss.player.transform.position.x, boss.player.transform.position.z);
            float angle = Mathf.Atan2(total.y, total.x) * Mathf.Rad2Deg;
            angle += 90;
            angle -= (angle - 180) * 2;
            boss.bulletStartDegree = angle + (width/2);
            boss.bulletEndDegree = angle - (width/2);
        }
    }
}
