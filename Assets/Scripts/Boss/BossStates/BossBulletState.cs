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
        if (timerDuringAttacks < 3f)
        {
            boss.anim.speed = 2f;
        }
        else
        {
            boss.anim.speed = 0f;
        }
        if (timerDuringAttacks > bossAttackTime)
        {
            timerDuringAttacks -= bossAttackTime;
            boss.anim.speed = 1f;
            boss.TransitionToState(boss.IdleState);
        }


        for (int i = 0; i < bulletCount / ((1.0f / Time.smoothDeltaTime) * bossAttackTime); i++)
        {
            spawnBullet(boss);
        }
    }

    private void spawnBullet(BossController boss)
    {
        GameObject bullet = bulletPool.GetPooledObject();
        if (bullet != null)
        {
            bullet.GetComponent<BulletScript>().damageDealt = bulletDamage;
            bullet.GetComponent<BulletScript>().movementSpeed = bulletSpeed;
            bullet.transform.position = new Vector3(boss.transform.position.x + 1.0f, 9.75f, boss.transform.position.z - 1.0f);
            bullet.transform.position = new Vector3(-1.73f, 7.23f, 54.24f);
            bullet.GetComponent<BulletScript>().startPos = bullet.transform.position;
            bullet.transform.rotation = Quaternion.Euler(0.0f, Random.Range(bulletStartDegree, bulletEndDegree), 0.0f);
            bullet.SetActive(true);
        }
    }
}
