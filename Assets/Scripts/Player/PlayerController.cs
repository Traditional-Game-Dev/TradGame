using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Basic Settings:")]
    public InputActionAsset playerControls;
    public Camera cam;
    public CharacterController controller;
    public Animator anim;
    [Header("Movement Settings:")]
    public float baseSpeed;
    [Header("Dash Settings:")]
    public GameObject dashAura;
    public float dashCooldown;
    public float dashMultiplier;
    public int dashCounter;
    public GameObject dashUI;

    private GameManager manager;
    private Transform camTransform;
    private DashSprites dashSprites;
    private List<ParticleSystem> dashParticles;
    private float moveSpeed;
    private Vector3 moveDirection;
    private Vector3 direction;
    private Vector2 dir = new Vector2(0f, 0f);
    private float turnSmoothVelocity;

    private float turnSmoothTime = 0.1f; 
    private const float MAX_DASH_TIME = 0.6f;
    private const int MAX_DASH_COUNTER = 3;
    private float dashStoppingSpeed = 0.1f;
    private float currentDashTime = MAX_DASH_TIME;
    private float currentDashCooldownTime = 0f;

    private InputAction movement;
    private InputAction dash;

    private PlayerBaseState currentState;
    public PlayerBaseState CurrentState { get => currentState; }

    public readonly PlayerIdleState IdleState = new PlayerIdleState();
    public readonly PlayerMovingState MovingState = new PlayerMovingState();

    public void TransitionToState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        camTransform = cam.transform;

        var gameplayActionMap = playerControls.FindActionMap("Gameplay");

        foreach (ParticleSystem ps in dashAura.GetComponentsInChildren<ParticleSystem>())
        {
            ps.Stop();
        }

        movement = gameplayActionMap.FindAction("Movement");
        movement.performed += ctx =>
        {
            if (!manager.playerDisabled)
            {
                dir = ctx.ReadValue<Vector2>();
            }
        };
        movement.canceled += ctx =>
        {
            dir = ctx.ReadValue<Vector2>();
        };
        movement.Enable();

        dash = gameplayActionMap.FindAction("Dash");
        dash.performed += ctx =>
        {
            Vector3 direction = new Vector3(dir.x, 0f, dir.y).normalized;
            if (direction.magnitude >= 0.1f && dashCounter < MAX_DASH_COUNTER)
            {
                currentDashTime = 0;
                currentDashCooldownTime = 0;
                dashCounter += dashCounter <= MAX_DASH_COUNTER ? 1 : 0;
                manager.playerIvin = true;

                foreach (ParticleSystem ps in dashAura.GetComponentsInChildren<ParticleSystem>())
                {
                    ps.Play();
                }

                dashSprites.UpdateDashImage(dashCounter);
            }
        };
        dash.Enable();
    }

    void Start()
    {
        dashSprites = dashUI.GetComponent<DashSprites>();

        TransitionToState(IdleState);
    }

    void Update()
    {
        currentState.Update(this);
    }

    void FixedUpdate()
    {
        direction = new Vector3(dir.x, 0f, dir.y).normalized;

        if (direction.magnitude >= 0.1f && !manager.playerDisabled)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (dashCounter > 0)
            {
                if (dashCounter <= MAX_DASH_COUNTER && currentDashTime < MAX_DASH_TIME)
                {
                    moveSpeed = baseSpeed * dashMultiplier;
                    currentDashTime += dashStoppingSpeed;
                }
                else
                {
                    moveSpeed = baseSpeed;
                    manager.playerIvin = false;

                    foreach (ParticleSystem ps in dashAura.GetComponentsInChildren<ParticleSystem>())
                    {
                        ps.Stop();
                    }
                }

                currentDashCooldownTime += dashStoppingSpeed;
            }
            else
            {
                moveSpeed = baseSpeed;
            }

            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }

        if (dashCounter > 0)
        {
            currentDashCooldownTime += dashStoppingSpeed;
        }
        if (currentDashCooldownTime >= dashCooldown)
        {
            dashSprites.UpdateDashImage(dashCounter);

            dashCounter -= dashCounter > 0 ? 1 : 0;
            currentDashCooldownTime = MAX_DASH_TIME + 1;
        }
    }

    public float GetDirectionMag()
    {
        return direction.magnitude;
    }
}
