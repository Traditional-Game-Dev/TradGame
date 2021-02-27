using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject player;
    public float bossAttackTime;
    public float bossWaitTime;
    public LineRenderer lineRenderer;
    public float radius;
    public float circleSpeed;
    public float circleDivide;
    public float hitboxOffset;
    public int damageDealt;

    private int hitCounter = 0;
    private int i = 0;
    private bool firingLaser = false;
    private float timerBetweenAttacks = 0.0f;
    private float timerDuringAttacks = 0.0f;
    private float invinTimer = 0.0f;
    private bool playerInvin = false;
    private float playerInvinTime = 1.0f;

    void start(){
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(firingLaser)
        {
            timerDuringAttacks += Time.deltaTime;
            if(timerDuringAttacks > bossAttackTime)
            {
                firingLaser = false;
                lineRenderer.enabled = false;
                timerDuringAttacks = timerDuringAttacks - bossAttackTime;
            }
        }
        else
        {
            timerBetweenAttacks += Time.deltaTime;
            if(timerBetweenAttacks > bossWaitTime)
            {
                firingLaser = true;
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position);

                timerBetweenAttacks = timerBetweenAttacks - bossWaitTime;
            }
        }

        if(firingLaser){
            for (int j = 0; j < circleSpeed; j++)
            {
                float angle = i * Mathf.PI * 2f / circleDivide;
                Vector3 lineEndPosition = new Vector3(Mathf.Cos(angle) * radius + transform.position.x, 0, Mathf.Sin(angle) * radius + transform.position.z);

                lineRenderer.SetPosition(1, lineEndPosition);
                i++;

                if (i > circleDivide)
                {
                    i = 0;
                }

                RaycastHit hitNotPlayer;
                if(Physics.Linecast(transform.position, lineEndPosition, out hitNotPlayer))
                {
                    lineRenderer.SetPosition(1, hitNotPlayer.point);
                }


                RaycastHit hit;
                if (Physics.Linecast(new Vector3(transform.position.x, 1, transform.position.z), new Vector3(Mathf.Cos(angle) * (radius + hitboxOffset) + transform.position.x, 0, Mathf.Sin(angle) * (radius + hitboxOffset) + transform.position.z), out hit))
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
}
