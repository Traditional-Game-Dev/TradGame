using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorCode : MonoBehaviour
{
    
    Quaternion reflectorAngle;
    Vector3 reflectorForwardVector;

    public Color reflectColor;

    void Start(){
        reflectorAngle = transform.rotation;
        reflectorForwardVector = reflectorAngle * Vector3.forward;
    }

    void OnTriggerEnter(Collider other)    
    {
        if(other.CompareTag("Bullet"))
        {
            Vector3 newDirection = Vector3.Reflect(other.transform.forward, reflectorForwardVector);
            other.gameObject.transform.rotation = Quaternion.LookRotation(newDirection);

            //Change Color
            Renderer tempRender = other.gameObject.GetComponent<MeshRenderer>();
            tempRender.material.SetColor("_Color", reflectColor);
            tempRender.material.SetColor("_EmissiveColor", reflectColor);
            other.gameObject.GetComponentInChildren<Light>().color = reflectColor;
            tempRender = other.gameObject.GetComponentInChildren<ParticleSystemRenderer>();
            tempRender.material.SetColor("_Color", reflectColor);
            tempRender.material.SetColor("_EmissiveColor", reflectColor);

        }
    }
}