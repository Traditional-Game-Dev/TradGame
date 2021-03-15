using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [System.NonSerialized] public int damageDealt = 0;
    [System.NonSerialized] public float movementSpeed = 50;

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
        //gameObject.GetComponentInChildren<ParticleSystem>().gameObject.transform.Rotate(0, 0, 0);
    }

    void OnTriggerEnter(Collider other)    
    {
        if(other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().damagePlayer(damageDealt);
        }
        if(other.gameObject.name != "Boss" && !other.gameObject.name.Contains("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
