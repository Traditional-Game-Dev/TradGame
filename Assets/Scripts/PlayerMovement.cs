using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public ParticleSystem attackParticles;
    public Camera cam;
    private Transform camTransform;

    public float baseSpeed;
    private float moveSpeed;

    private Vector3 moveDirection;
    private Vector2 dir = new Vector2(0f, 0f);

    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;

    private const float MAX_DASH_TIME = 1.5f;
    private const int MAX_DASH_COUNTER = 3;
    private float dashStoppingSpeed = 0.1f;
    private float currentDashTime = MAX_DASH_TIME;
    private float currentMaxCooldownTime = 0;
    private float currentStepCooldownTime = 0;
    public float maxDashCooldown;
    public float stepDashCooldown;
    public float dashMultiplier;
    public int dashCounter;

    //private float attackCooldown = 25f; // for future use
    private float attackParticlesCooldown = 0.5f;
    private float currentAttackParticlesCooldown = 0f;

    public InputActionAsset playerControls;
    private InputAction movement;
    private InputAction dash;
    private InputAction attack;


    //private PostProcessingBehavior postProcessingBehavior; ///

    void Awake()
    {
        camTransform = cam.transform;

        var gameplayActionMap = playerControls.FindActionMap("Gameplay");

        movement = gameplayActionMap.FindAction("Movement");
        movement.performed += ctx =>
        {
            dir = ctx.ReadValue<Vector2>();
        };
        movement.canceled += ctx =>
        {
            dir = ctx.ReadValue<Vector2>();
        };
        movement.Enable();

        dash = gameplayActionMap.FindAction("Dash");
        dash.performed += ctx =>
        {
            currentDashTime = 0;

            currentStepCooldownTime = 0;

            dashCounter += dashCounter <= MAX_DASH_COUNTER ? 1 : 0;
        };
        dash.Enable();

        attackParticles.Stop();
        attack = gameplayActionMap.FindAction("Attack");
        attack.performed += ctx =>
        {
            attackParticles.Play();
            currentAttackParticlesCooldown = 0;
        };

        //postProcessingBehavior = camera.GetComponent<PostProcessingBehavior>(); ///
    }

    void FixedUpdate()
    {
        // particles are active
        if (attackParticles.isEmitting)
        {
            currentAttackParticlesCooldown += dashStoppingSpeed; // this makes sense, trust me

            if (currentAttackParticlesCooldown > attackParticlesCooldown)
            {
                attackParticles.Stop();
                currentAttackParticlesCooldown = 0;
            }
        }
    }

    void Update()
    {
        Vector3 direction = new Vector3(dir.x, 0f, dir.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (dashCounter > MAX_DASH_COUNTER) // exceeded max dash count
            {
                if (currentMaxCooldownTime < maxDashCooldown) 
                {
                    currentMaxCooldownTime += dashStoppingSpeed;
                }
                else   
                {
                    currentMaxCooldownTime = 0;
                    currentStepCooldownTime = 0;
                    dashCounter = 0;
                    currentDashTime = MAX_DASH_TIME;
                }

                //postProcessingBehavior.useBlur = false; ///

                moveSpeed = baseSpeed;
            }
            else if (currentDashTime < MAX_DASH_TIME) // can, and is, dashing
            {
                moveSpeed = baseSpeed * dashMultiplier;

                currentDashTime += dashStoppingSpeed;

                //postProcessingBehavior.useBlur = true; ///
            }
            else // not dashing
            {
                moveSpeed = baseSpeed;

                //postProcessingBehavior.useBlur = false; ///
            }

            // keep track of constant dash cooldown
            if (dashCounter > 0)
            {
                currentStepCooldownTime += dashStoppingSpeed;
            }
            if (currentStepCooldownTime >= stepDashCooldown)
            {
                dashCounter -= dashCounter > 0 ? 1 : 0;
                currentStepCooldownTime = 0;
            }

            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
