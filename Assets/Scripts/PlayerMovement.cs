using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    //public ParticleSystem particles;

    private float moveSpeed;
    public float baseSpeed = 6f;
    private Vector3 moveDirection;

    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    private const float MAX_DASH_TIME = 2.5f;
    private const float MAX_DASH_COOLDOWN = 25f;
    private const int MAX_DASH_COUNTER = 3;
    private float dashStoppingSpeed = 0.1f;
    private float currentDashTime = MAX_DASH_TIME;
    private float currentCooldownTime = 0;
    public float dashMultiplier = 12f;
    public int dashCounter = 0;

    private Vector2 dir = new Vector2(0f, 0f);
    public InputActionAsset playerControls;
    private InputAction movement;
    private InputAction dash;


    void Awake()
    {
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

            dashCounter += dashCounter <= MAX_DASH_COUNTER ? 1 : 0;
        };
        dash.Enable();
    }

    void Update()
    {
        Vector3 direction = new Vector3(dir.x, 0f, dir.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


            if (dashCounter > MAX_DASH_COUNTER) 
            {
                if (currentCooldownTime < MAX_DASH_COOLDOWN) 
                {
                    currentCooldownTime += dashStoppingSpeed;
                }
                else   
                {
                    currentCooldownTime = 0;

                    dashCounter = 0;

                    currentDashTime = MAX_DASH_TIME;
                }

                moveSpeed = baseSpeed;
            }
            else if (currentDashTime < MAX_DASH_TIME)
            {
                moveSpeed = baseSpeed * dashMultiplier;

                currentDashTime += dashStoppingSpeed;
            }
            else
            {
                moveSpeed = baseSpeed;
            }

            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
