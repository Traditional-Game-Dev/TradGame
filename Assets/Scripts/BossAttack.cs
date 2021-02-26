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
    public float attackSpeed;
    public float hitboxOffset;
    public int damageDealt;

    private int hitCounter = 0;
    private int i = 0;
    private bool firingLaser = false;
    private float timerBetweenAttacks = 0.0f;
    private float timerDuringAttacks = 0.0f;

    void start(){
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*timerBetweenAttacks += Time.deltaTime;

        //Check if we have passed 5 seconds
        if(timerBetweenAttacks > bossAttackTime)
        {
            if(firingLaser){
                firingLaser = false;
                lineRenderer.enabled = false;
            }
            else{
                firingLaser = true;
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position);
            }

            timerBetweenAttacks = timerBetweenAttacks - bossAttackTime;
        }*/

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
            
            float angle = i * Mathf.PI*2f / attackSpeed;
            Vector3 lineEndPosition = new Vector3(Mathf.Cos(angle)*radius + transform.position.x, 0, Mathf.Sin(angle)*radius + transform.position.z);

            lineRenderer.SetPosition(1, lineEndPosition);
            i++;

            if(i > attackSpeed){
                i = 0;
            }

            RaycastHit hit;
            if(Physics.Linecast(new Vector3(transform.position.x, 1, transform.position.z), new Vector3(Mathf.Cos(angle)*(radius + hitboxOffset) + transform.position.x, 0, Mathf.Sin(angle)*(radius + hitboxOffset) + transform.position.z), out hit)){
                if(hit.collider.name == "Player"){
                    hitCounter++;
                    Debug.Log("Player Detected" + hitCounter);
                    player.GetComponent<PlayerHealth>().damagePlayer(damageDealt);
                }
            }
        }
    }
}
