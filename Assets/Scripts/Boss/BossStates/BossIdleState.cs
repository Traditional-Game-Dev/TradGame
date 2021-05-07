using Assets.Scripts.Boss.BossEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState
{
    private float timerDuringAttacks = 0;
    private GameManager manager;

    public override void EnterState(BossController boss)
    {
        //boss.anim.Play("Idle");
        boss.anim.SetBool("playLaserAnim", false);
        boss.anim.SetBool("playBulletAnim", false);
        boss.anim.SetBool("playPoisonAnim", false);
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;
        if (timerDuringAttacks > boss.bossWaitTime && boss.transitioningLevels == 0)
        {
            float distance = Vector3.Distance(boss.transform.position, boss.player.transform.position);
            //Always Shoot Laser First
            if (boss.prevAttack == BossAttacks.First)
            {
                boss.prevAttack = BossAttacks.Laser;
                boss.TransitionToState(boss.LaserPreState);
            }
            //Really Close to Boss
            else if (distance < 15)
            {
                switch (boss.prevAttack)
                {
                    case BossAttacks.Bullets:
                        if ((Random.value > 0.5f))
                        {
                            boss.prevAttack = BossAttacks.Stomp;
                            boss.TransitionToState(boss.StompPreState);
                        }
                        else
                        {
                            boss.prevAttack = BossAttacks.Poison;
                            boss.poisonType = PoisonType.Defense;
                            boss.TransitionToState(boss.PoisonPreState);
                        }
                        break;

                    case BossAttacks.Poison:
                        boss.prevAttack = BossAttacks.Stomp;
                        boss.TransitionToState(boss.StompPreState);
                        break;

                    case BossAttacks.Laser:
                        if ((Random.value > 0.5f))
                        {
                            boss.prevAttack = BossAttacks.Stomp;
                            boss.TransitionToState(boss.StompPreState);
                        }
                        else
                        {
                            boss.prevAttack = BossAttacks.Poison;
                            boss.poisonType = PoisonType.Defense;
                            boss.TransitionToState(boss.PoisonPreState);
                        }
                        break;
                    
                    case BossAttacks.Stomp:
                        boss.prevAttack = BossAttacks.Poison;
                        boss.poisonType = PoisonType.Defense;
                        boss.TransitionToState(boss.PoisonPreState);
                        break;

                }
            }
            //Sorta Close to boss, shoots laser or poison
            else if (distance < 25)
            {
                switch (boss.prevAttack)
                {
                    case BossAttacks.Bullets:
                        if ((Random.value > 0.5f))
                        {
                            boss.prevAttack = BossAttacks.Laser;
                            boss.TransitionToState(boss.LaserPreState);
                        }
                        else
                        {
                            boss.prevAttack = BossAttacks.Poison;
                            boss.poisonType = PoisonType.Normal;
                            boss.TransitionToState(boss.PoisonPreState);
                        }
                        break;

                    case BossAttacks.Poison:
                        boss.prevAttack = BossAttacks.Laser;
                        boss.TransitionToState(boss.LaserPreState);
                        break;

                    case BossAttacks.Laser:
                        boss.prevAttack = BossAttacks.Poison;
                        boss.poisonType = PoisonType.Normal;
                        boss.TransitionToState(boss.PoisonPreState);
                        break;

                    case BossAttacks.Stomp:
                        boss.prevAttack = BossAttacks.Laser;
                        boss.TransitionToState(boss.LaserPreState);
                        break;
                }
            }
            //Midrange shoots all attacks
            //Bullets aim towards player in cone
            else if (distance < 100 || (manager.currLevel == 2 && distance >= 25))
            {
                switch (boss.prevAttack)
                {
                    case BossAttacks.Bullets:
                        if ((Random.value > 0.5f))
                        {
                            boss.prevAttack = BossAttacks.Laser;
                            boss.TransitionToState(boss.LaserPreState);
                        }
                        else
                        {
                            boss.prevAttack = BossAttacks.Poison;
                            boss.poisonType = PoisonType.Normal;
                            boss.TransitionToState(boss.PoisonPreState);
                        }
                        break;

                    case BossAttacks.Poison:
                        if ((Random.value > 0.5f))
                        {
                            boss.prevAttack = BossAttacks.Laser;
                            boss.TransitionToState(boss.LaserPreState);
                        }
                        else
                        {
                            boss.prevAttack = BossAttacks.Bullets;
                            fireBullets(boss.width, boss);
                            boss.TransitionToState(boss.BulletPreState);

                        }
                        break;

                    case BossAttacks.Laser:
                        if ((Random.value > 0.5f))
                        {
                            boss.prevAttack = BossAttacks.Bullets;
                            fireBullets(boss.width, boss);
                            boss.TransitionToState(boss.BulletPreState);

                        }
                        else
                        {
                            boss.prevAttack = BossAttacks.Poison;
                            boss.poisonType = PoisonType.Normal;
                            boss.TransitionToState(boss.PoisonPreState);
                        }
                        break;

                    case BossAttacks.Stomp:
                        if ((Random.value > 0.66f))
                        {
                            boss.prevAttack = BossAttacks.Laser;
                            boss.TransitionToState(boss.LaserPreState);
                        }
                        else
                        {
                            boss.prevAttack = BossAttacks.Poison;
                            boss.poisonType = PoisonType.Normal;
                            boss.TransitionToState(boss.PoisonPreState);
                        }
                        break;
                }
            }
            //Far Range
            //Fires bullets in 180(ish) towards player or poison
            else if(manager.currLevel == 1)
            {
                Debug.Log("Firing long range");
                switch (boss.prevAttack)
                {
                    case BossAttacks.Laser:
                        if ((Random.value > 0.5f))
                        {
                            boss.prevAttack = BossAttacks.Bullets;
                            fireBullets(165f, boss);
                            boss.TransitionToState(boss.BulletPreState);

                        }
                        else
                        {
                            boss.prevAttack = BossAttacks.Poison;
                            boss.poisonType = PoisonType.Far;
                            boss.TransitionToState(boss.PoisonPreState);
                        }
                        break;

                    case BossAttacks.Poison:
                        boss.prevAttack = BossAttacks.Bullets;
                        fireBullets(165f, boss);
                        boss.TransitionToState(boss.BulletPreState);
                        break;

                    case BossAttacks.Bullets:
                        boss.prevAttack = BossAttacks.Poison;
                        boss.poisonType = PoisonType.Far;
                        boss.TransitionToState(boss.PoisonPreState);
                        break;

                     case BossAttacks.Stomp:
                        if ((Random.value > 0.5f))
                        {
                            boss.prevAttack = BossAttacks.Bullets;
                            fireBullets(165f, boss);
                            boss.TransitionToState(boss.BulletPreState);

                        }
                        else
                        {
                            boss.prevAttack = BossAttacks.Poison;
                            boss.poisonType = PoisonType.Far;
                            boss.TransitionToState(boss.PoisonPreState);
                        }
                        break;
                }
            }
        }
    }

    private void fireBullets(float width, BossController boss)
    {
        if (width > 150)
        {
                boss.bulletStartDegree = 180 - (width / 2);
                boss.bulletEndDegree = 180 + (width / 2);
        }
        else
        {
            Vector2 total = new Vector2(boss.transform.position.x, boss.transform.position.z) - new Vector2(boss.player.transform.position.x, boss.player.transform.position.z);
            float angle = Mathf.Atan2(total.y, total.x) * Mathf.Rad2Deg;
            angle += 90;
            angle -= (angle - 180) * 2;
            boss.bulletStartDegree = angle + (width / 2);
            boss.bulletEndDegree = angle - (width / 2);
        }
    }
}