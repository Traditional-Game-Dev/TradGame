using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject player;
    public float bossAttackTime;
    public LineRenderer lineRenderer;
    public float radius;
    public float attackSpeed;
    public float hitboxOffset;
    public int damageDealt;

    private int hitCounter = 0;
    private int i = 0;
    private bool firingLaser = false;
    private float timer = 0.0f;

    void start(){
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Check if we have passed 5 seconds
        if(timer > bossAttackTime)
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

            timer = timer - bossAttackTime;
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
            if(Physics.Linecast(transform.position, new Vector3(Mathf.Cos(angle)*(radius + hitboxOffset) + transform.position.x, 0, Mathf.Sin(angle)*(radius + hitboxOffset) + transform.position.z), out hit)){
                if(hit.collider.name == "Player"){
                    hitCounter++;
                    Debug.Log("Player Detected" + hitCounter);
                    player.GetComponent<PlayerHealth>().damagePlayer(damageDealt);
                }
            }
        }
    }
}
