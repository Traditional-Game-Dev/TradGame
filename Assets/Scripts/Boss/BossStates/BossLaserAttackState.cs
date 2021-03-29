using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserAttackState : BossBaseState
{
    private GameObject player;
    private LineRenderer lineRenderer;
    private ParticleSystem laserImpact;
    private GameObject laserLight;

    private float radius;
    private float circleSpeed;
    private float circleDivide;
    private int headOffset;
    private int damageDealt;
    private float bossAttackTime;

    private int i = 0;
    private int hitCounter = 0;
    private float invinTimer = 0.0f;
    private bool playerInvin = false;
    private float playerInvinTime = 1.0f;
    private float timerDuringAttacks = 0.0f;

    public override void EnterState(BossController boss)
    {
        // new animation played here
        boss.anim.Play("LaserAnim");

        player = boss.player;
        lineRenderer = boss.lineRenderer;
        laserImpact = boss.laserImpact;
        radius = 100;
        circleSpeed = boss.circleSpeed;
        circleDivide = boss.circleDivide;
        headOffset = boss.headOffset;
        damageDealt = boss.laserDamage;
        bossAttackTime = boss.bossAttackTime;
        laserLight = boss.laserLight;

        lineRenderer.enabled = true;
        laserImpact.Play();
        laserLight.SetActive(true);
        lineRenderer.SetPosition(0, new Vector3(boss.transform.position.x - headOffset, 10, boss.transform.position.z));
    }

    public override void Update(BossController boss)
    {
        timerDuringAttacks += Time.deltaTime;
        if (timerDuringAttacks > bossAttackTime)
        {
            //firingLaser = false;
            lineRenderer.enabled = false;
            laserImpact.Stop();
            laserLight.SetActive(false);
            timerDuringAttacks -= bossAttackTime;

            boss.TransitionToState(boss.IdleState);
        }

        float dist = Vector3.Distance(player.transform.position, boss.transform.position);
        int updatedSpeed = (int)(circleSpeed * ((100 - radius) / 50));
        if (updatedSpeed < circleSpeed)
        {
            updatedSpeed = (int)circleSpeed;
        }
        for (int j = 0; j < updatedSpeed / (0.02f / Time.fixedDeltaTime); j++)
        {
            radius = Vector3.Distance(player.transform.position, boss.transform.position) + 5;
            float angle = i * Mathf.PI * 2f / circleDivide;
            Vector3 lineEndPosition = new Vector3(Mathf.Cos(angle) * radius + boss.transform.position.x, 0, Mathf.Sin(angle) * radius + boss.transform.position.z);

            lineRenderer.SetPosition(1, lineEndPosition);
            laserImpact.transform.position = lineEndPosition;
            laserLight.transform.position = Vector3.MoveTowards(lineEndPosition, boss.transform.position, Vector3.Distance(lineEndPosition, boss.transform.position)/4);

            Vector3 relativePos = lineEndPosition - boss.transform.position;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            laserImpact.transform.rotation = rotation;
            i++;

            if (i > circleDivide)
            {
                i = 0;
            }

            RaycastHit hitNotPlayer;
            if (Physics.Linecast(new Vector3(boss.transform.position.x - headOffset, 10, boss.transform.position.z), lineEndPosition, out hitNotPlayer))
            {
                lineRenderer.SetPosition(1, hitNotPlayer.point);
                laserImpact.transform.position = hitNotPlayer.point;
            }


            RaycastHit hit;
            if (Physics.Linecast(new Vector3(boss.transform.position.x - headOffset, 10, boss.transform.position.z), lineEndPosition, out hit))
            {
                if (hit.collider.name == "Player")
                {
                    hitCounter++;
                    if (!playerInvin)
                    {
                        player.GetComponent<PlayerHealth>().damagePlayer(damageDealt);
                    }
                    playerInvin = true;
                }
            }

            if (playerInvin)
            {
                invinTimer += Time.deltaTime;
                if (invinTimer > playerInvinTime)
                {
                    invinTimer = 0f;
                    playerInvin = false;
                }
            }
        }
    }
}
