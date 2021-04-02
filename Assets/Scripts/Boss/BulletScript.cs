using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [System.NonSerialized] public int damageDealt = 0;
    [System.NonSerialized] public float movementSpeed = 50;
    [System.NonSerialized] public Vector3 startPos;
    MeshRenderer objectRenderer;
    private GameManager manager;

    void Awake()
    {
        //Init All Colors to Red
        objectRenderer = gameObject.GetComponent<MeshRenderer>();
        objectRenderer.material.SetColor("_Color", Color.red);
        objectRenderer.material.SetColor("_EmissiveColor", Color.red);
        objectRenderer.material.SetFloat("_EmissionIntensity", 100);
        gameObject.GetComponentInChildren<ParticleSystemRenderer>().material.SetColor("_Color", Color.red);
        gameObject.GetComponentInChildren<ParticleSystemRenderer>().material.SetColor("_EmissiveColor", Color.red);
        gameObject.GetComponentInChildren<ParticleSystemRenderer>().material.SetFloat("_EmissionIntensity", 25);
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    void OnDisable()
    {
        //Reset Color (In Case of Reflect)
        objectRenderer.material.SetColor("_Color", Color.red);
        objectRenderer.material.SetColor("_EmissiveColor", Color.red);
        gameObject.GetComponentInChildren<Light>().color = Color.red;
        gameObject.GetComponentInChildren<ParticleSystemRenderer>().material.SetColor("_Color", Color.red);
        gameObject.GetComponentInChildren<ParticleSystemRenderer>().material.SetColor("_EmissiveColor", Color.red);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
        if(transform.position.y > 1)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y-0.25f, transform.position.z);
        }

        if(!gameObject.GetComponentInChildren<Light>().enabled && Vector3.Distance(transform.position, startPos) > 30)
        {
            gameObject.GetComponentInChildren<Light>().enabled = true;
        }
    }

    void OnTriggerEnter(Collider other)    
    {
        if(other.gameObject.name != "Boss" && !other.gameObject.name.Contains("Bullet") && !other.gameObject.name.Contains("reflector"))
        {
            movementSpeed = 0;
            gameObject.GetComponentInChildren<Light>().enabled = false;
            if(other.gameObject.name == "Player" && !manager.playerIvin)
            {
                other.gameObject.GetComponent<PlayerHealth>().damagePlayer(damageDealt);
            }
            gameObject.SetActive(false);
        }
    }
}
