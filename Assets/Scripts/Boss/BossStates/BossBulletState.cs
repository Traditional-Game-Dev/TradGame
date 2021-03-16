using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletState : BossBaseState
{
    private float bossAttackTime;
    private float bulletStartDegree;
    private float bulletEndDegree;
    private int bulletDamage;
    private ObjectPool bulletPool;
    private int bulletCount;
    private float bulletSpeed;
    private float timerDuringAttacks = 0.0f;

    public override void EnterState(BossController boss)
    {
        //TODO: New Anim Plays Here

        bossAttackTime = boss.bossAttackTime;
        bulletStartDegree = boss.bulletStartDegree;
        bulletEndDegree = boss.bulletEndDegree;
        bulletDamage = boss.bulletDamage;
        bulletPool = boss.bulletPool;
        bulletCount = boss.bulletCount;
        bulletSpeed = boss.bulletSpeed;
    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;
        if (timerDuringAttacks > bossAttackTime)
        {
            timerDuringAttacks -= bossAttackTime;
            boss.TransitionToState(boss.IdleState);
        }


        for(int i = 0; i < bulletCount/((1.0f/Time.smoothDeltaTime)*bossAttackTime); i++)
        {
            spawnBullet(boss);
        }
    }

    private void spawnBullet(BossController boss)
    {
        GameObject bullet = bulletPool.GetPooledObject();
        if(bullet != null)
        {
            bullet.GetComponent<BulletScript>().damageDealt = bulletDamage;
            bullet.GetComponent<BulletScript>().movementSpeed = bulletSpeed;
            Vector3 pos = boss.transform.position;
            pos.y = 1;
            bullet.transform.position = pos;
            bullet.transform.rotation = Quaternion.Euler(0.0f, Random.Range(bulletStartDegree, bulletEndDegree), 0.0f);
            bullet.SetActive(true);
        }
    }
}
