using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject player;
    public float bossAttackTime;
    private float timer = 0.0f;
    public LineRenderer lineRenderer;
    private bool firingLaser = false;    
    private int i = 0;
    public float radius = 10f;

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
            float angle = i * Mathf.PI*2f / 365;

            lineRenderer.SetPosition(1, new Vector3(Mathf.Cos(angle)*radius, transform.position.y, Mathf.Sin(angle)*radius));
            i++;

            if(i > 365){
                i = 0;
            }
        }
    }
}
