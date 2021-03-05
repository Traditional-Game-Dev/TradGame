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
    public int headOffset;

    private int hitCounter = 0;
    private int i = 0;
    private bool firingLaser = false;
    private float timerBetweenAttacks = 0.0f;
    private float timerDuringAttacks = 0.0f;
    private float invinTimer = 0.0f;
    private bool playerInvin = false;
    private float playerInvinTime = 1.0f;
    [System.NonSerialized] public bool planningPhase = true;

    void start()
    {
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!planningPhase)
        {
            if (firingLaser)
            {
                timerDuringAttacks += Time.deltaTime;
                if (timerDuringAttacks > bossAttackTime)
                {
                    firingLaser = false;
                    lineRenderer.enabled = false;
                    timerDuringAttacks = timerDuringAttacks - bossAttackTime;
                }
            }
            else
            {
                timerBetweenAttacks += Time.deltaTime;
                if (timerBetweenAttacks > bossWaitTime)
                {
                    firingLaser = true;
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, new Vector3(transform.position.x - headOffset, 10, transform.position.z));

                    timerBetweenAttacks = timerBetweenAttacks - bossWaitTime;
                }
            }

            if (firingLaser)
            {
                float dist = Vector3.Distance(player.transform.position, transform.position);
                for (int j = 0; j < circleSpeed; j++)
                {
                    radius = Vector3.Distance(player.transform.position, transform.position) + 5;
                    float angle = i * Mathf.PI * 2f / circleDivide;
                    Vector3 lineEndPosition = new Vector3(Mathf.Cos(angle) * radius + transform.position.x, 0, Mathf.Sin(angle) * radius + transform.position.z);

                    lineRenderer.SetPosition(1, lineEndPosition);
                    i++;

                    if (i > circleDivide)
                    {
                        i = 0;
                    }

                    RaycastHit hitNotPlayer;
                    if (Physics.Linecast(new Vector3(transform.position.x - headOffset, 10, transform.position.z), lineEndPosition, out hitNotPlayer))
                    {
                        lineRenderer.SetPosition(1, hitNotPlayer.point);
                    }


                    RaycastHit hit;
                    if (Physics.Linecast(new Vector3(transform.position.x - headOffset, 10, transform.position.z), lineEndPosition, out hit))
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
}
