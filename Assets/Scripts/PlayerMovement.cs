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
    private float dashStoppingSpeed = 0.1f;
    private float currentDashTime = MAX_DASH_TIME;
    private Vector2 dir = new Vector2(0f, 0f);
    public float dashMultiplier = 12f;
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

            if (currentDashTime < MAX_DASH_TIME)
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
