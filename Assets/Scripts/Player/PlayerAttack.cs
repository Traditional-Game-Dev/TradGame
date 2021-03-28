using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerAttack : MonoBehaviour
{
    public ParticleSystem attackParticles;
    public VisualEffect electricity;

    private float particleStoppingSpeed = 0.1f;
    //private float attackCooldown = 25f; // future use
    private float attackParticlesCooldown = 0.5f;
    private float currentAttackParticlesCooldown = 0f;

    public InputActionAsset playerControls;
    private InputAction attack;

    void Awake()
    {
        var gameplayActionMap = playerControls.FindActionMap("Gameplay");

        attackParticles.Stop();

        attack = gameplayActionMap.FindAction("Attack");
        attack.performed += ctx =>
        {
            //attackParticles.Play();
            //currentAttackParticlesCooldown = 0;
            electricity.Play();
        };
    }

    void FixedUpdate()
    {
        // particles are active
        //if (attackParticles.isEmitting)
        //{
        //    currentAttackParticlesCooldown += particleStoppingSpeed; // this makes sense, trust me

        //    if (currentAttackParticlesCooldown > attackParticlesCooldown)
        //    {
        //        attackParticles.Stop();
        //        currentAttackParticlesCooldown = 0;
        //    }
        //}

        if (electricity.isActiveAndEnabled)
        {
            Transform playerTransform = gameObject.transform;

            electricity.gameObject.transform.position = transform.forward + new Vector3(playerTransform.position.x + 1.5f,
                                                                                        playerTransform.position.y,
                                                                                        playerTransform.position.z + 1.0f);

            //electricity.gameObject.transform.rotation = new Quaternion(playerTransform.transform.rotation.x,
            //                                                           playerTransform.transform.rotation.y,
            //                                                           electricity.transform.rotation.z,
            //                                                           electricity.transform.rotation.);

            electricity.gameObject.transform.rotation = Quaternion.Euler(electricity.transform.rotation.x, 
                                                                         electricity.transform.rotation.y, 
                                                                         playerTransform.transform.rotation.z);
        }
    }
}
