using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public ParticleSystem particles;

    private float moveSpeed;
    public float baseSpeed = 6f;

    private Vector3 moveDirection;

    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    private const float MAX_DASH_TIME = 2.5f;
    private float dashStoppingSpeed = 0.1f;
    private float currentDashTime = MAX_DASH_TIME;
    public float dashMultiplier = 12f;

    void Start()
    {
        particles.Stop();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (Input.GetMouseButtonDown(1))
            {
                currentDashTime = 0;

                particles.Play();
            }

            if (currentDashTime < MAX_DASH_TIME)
            {
                moveSpeed = baseSpeed * dashMultiplier;

                currentDashTime += dashStoppingSpeed;
            }
            else
            {
                moveSpeed = baseSpeed;

                particles.Stop();
            }

            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }

    }
}
