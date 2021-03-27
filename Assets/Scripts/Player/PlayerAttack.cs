using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public ParticleSystem attackParticles;

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
            attackParticles.Play();
            currentAttackParticlesCooldown = 0;
        };
    }

    void FixedUpdate()
    {
        // particles are active
        if (attackParticles.isEmitting)
        {
            currentAttackParticlesCooldown += particleStoppingSpeed; // this makes sense, trust me

            if (currentAttackParticlesCooldown > attackParticlesCooldown)
            {
                attackParticles.Stop();
                currentAttackParticlesCooldown = 0;
            }
        }
    }
}
