using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorCode : MonoBehaviour
{
    
    Quaternion reflectorAngle;
    Vector3 reflectorForwardVector;

    void Start(){
        reflectorAngle = transform.rotation;
        reflectorForwardVector = reflectorAngle * Vector3.forward;
    }

    void OnTriggerEnter(Collider other)    
    {
        if(other.gameObject.name.Contains("Bullet"))
        {
            Vector3 newDirection = Vector3.Reflect(other.transform.forward, reflectorForwardVector);
            other.gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
        
        }
    }
}