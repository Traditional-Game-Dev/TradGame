using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerAttack : MonoBehaviour
{
    public ParticleSystem attackParticles;
    public VisualEffect electricity;

    //private float particleStoppingSpeed = 0.1f;
    //private float attackCooldown = 25f; // future use
    //private float attackParticlesCooldown = 0.5f;
    //private float currentAttackParticlesCooldown = 0f;

    public InputActionAsset playerControls;
    private InputAction attack;

    void Awake()
    {
        var gameplayActionMap = playerControls.FindActionMap("Gameplay");

        attackParticles.Stop();

        electricity.gameObject.SetActive(true);

        attack = gameplayActionMap.FindAction("Attack");
        attack.performed += ctx =>
        {
            //currentAttackParticlesCooldown = 0;
            electricity.Play();
        };
    }

    void FixedUpdate()
    {
        if (electricity.isActiveAndEnabled)
        {
            Transform playerTransform = gameObject.transform;

            electricity.gameObject.transform.position = transform.forward + new Vector3(playerTransform.position.x,
                                                                                        playerTransform.position.y,
                                                                                        playerTransform.position.z);

            electricity.gameObject.transform.rotation = new Quaternion(electricity.transform.rotation.x,
                                                                       playerTransform.rotation.y,
                                                                       electricity.transform.rotation.z,
                                                                       playerTransform.rotation.w);

            //electricity.gameObject.transform.rotation = Quaternion.Euler(electricity.transform.rotation.x,
            //                                                             playerTransform.rotation.y - 180,
            //                                                             electricity.transform.rotation.z);
        }
    }
}
