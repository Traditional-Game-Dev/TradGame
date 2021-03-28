using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerAttack : MonoBehaviour
{
    public VisualEffect electricity;

    //private float attackCooldown = 25f; // future use

    public InputActionAsset playerControls;
    private InputAction attack;

    void Awake()
    {
        var gameplayActionMap = playerControls.FindActionMap("Gameplay");

        electricity.gameObject.SetActive(true);
        electricity.Stop();

        attack = gameplayActionMap.FindAction("Attack");
        attack.performed += ctx =>
        {
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
        }
    }
}
