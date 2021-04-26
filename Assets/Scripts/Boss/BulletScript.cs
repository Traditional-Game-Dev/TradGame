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
    private bool hitReflect;

    Quaternion reflectorAngle;
    Vector3 reflectorForwardVector;

    public Color reflectColor;

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
        hitReflect = false;
    }

    void OnDisable()
    {
        //Reset Color (In Case of Reflect)
        objectRenderer.material.SetColor("_Color", Color.red);
        objectRenderer.material.SetColor("_EmissiveColor", Color.red);
        gameObject.GetComponentInChildren<Light>().color = Color.red;
        gameObject.GetComponentInChildren<ParticleSystemRenderer>().material.SetColor("_Color", Color.red);
        gameObject.GetComponentInChildren<ParticleSystemRenderer>().material.SetColor("_EmissiveColor", Color.red);
        hitReflect = false;
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
        if(!other.isTrigger && !other.CompareTag("Boss") && !other.CompareTag("Bullet") && !other.CompareTag("Reflector"))
        {
            movementSpeed = 0;
            gameObject.GetComponentInChildren<Light>().enabled = false;
            if(other.CompareTag("Player") && !manager.playerIvin)
            {
                other.gameObject.GetComponent<PlayerHealth>().damagePlayer(damageDealt);
            }
            gameObject.SetActive(false);
        }
        else if(other.CompareTag("Reflector") && hitReflect == false){
            reflectorAngle = other.transform.rotation;
            reflectorForwardVector = reflectorAngle * Vector3.forward;

            Vector3 newDirection = Vector3.Reflect(transform.forward, reflectorForwardVector);
            transform.rotation = Quaternion.LookRotation(newDirection);
            Debug.Log(transform.rotation);

            //Change Color
            Renderer tempRender = gameObject.GetComponent<MeshRenderer>();
            tempRender.material.SetColor("_Color", reflectColor);
            tempRender.material.SetColor("_EmissiveColor", reflectColor);
            gameObject.GetComponentInChildren<Light>().color = reflectColor;
            tempRender = gameObject.GetComponentInChildren<ParticleSystemRenderer>();
            tempRender.material.SetColor("_Color", reflectColor);
            tempRender.material.SetColor("_EmissiveColor", reflectColor);
            hitReflect = true;
        }
    }
}
