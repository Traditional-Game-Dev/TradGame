using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerAttack : MonoBehaviour
{
    public VisualEffect electricity;
    public float comboTimeWindow = 1.0f;
    public int maxCombo = 3;
    public int comboCounter = 0;
    //public float comboCooldown = 25f; // future use

    private float heightOffset = 0.5f;
    private float handOffset = 0.5f;

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
            ComboAttack();
        };
    }

    void FixedUpdate()
    {

    }

    // *add delay interval once animations are in place
    void ComboAttack()
    {
        comboCounter++;

        switch (comboCounter)
        {
            case 1:
                PrepareLightning(true);
                ShootRedLightning();
                break;
            case 2:
                PrepareLightning(false);
                ShootBlueLightning();
                break;
            //case 3:
            //    ShootFireBall();
            //    break;
            //default:
            //    break;
        }

        if (comboCounter > 1)
        {
            comboCounter = 0;
        }
    }

    void PrepareLightning(bool isRed)
    {
        Transform playerTransform = gameObject.transform;

        float newHandOffset = isRed ? -handOffset : handOffset;

        // set emission position (calculate offset for the right or left hand)
        if (playerTransform.localEulerAngles.y <= 315.0f && playerTransform.localEulerAngles.y >= 225.0f)
        {
            electricity.gameObject.transform.position = transform.forward + new Vector3(playerTransform.position.x,
                                                                                       playerTransform.position.y + heightOffset,
                                                                                       playerTransform.position.z + newHandOffset);
        }
        else if (playerTransform.localEulerAngles.y >= 45.0f && playerTransform.localEulerAngles.y <= 135.0f)
        {
            electricity.gameObject.transform.position = transform.forward + new Vector3(playerTransform.position.x,
                                                                                       playerTransform.position.y + heightOffset,
                                                                                       playerTransform.position.z - newHandOffset);
        }
        else
        {
            electricity.gameObject.transform.position = transform.forward + new Vector3(playerTransform.position.x - newHandOffset,
                                                                                       playerTransform.position.y + heightOffset,
                                                                                       playerTransform.position.z);

        }

        // set emission rotation
        electricity.gameObject.transform.rotation = new Quaternion(electricity.transform.rotation.x,
                                                              playerTransform.rotation.y,
                                                              electricity.transform.rotation.z,
                                                              playerTransform.rotation.w);
    }

    void ShootBlueLightning()
    {
        electricity.SetVector4("BoltColor", new Vector4(0.356f, 0.772f, 0.984f, 1));
        electricity.SetVector4("FlashColor", new Vector4(0.713f, 1.545f, 1.968f, 1));

        electricity.Play();
    }

    void ShootRedLightning()
    {
        electricity.SetVector4("BoltColor", new Vector4(0.984f, 0.356f, 0.356f, 1));
        electricity.SetVector4("FlashColor", new Vector4(1.968f, 0.713f, 0.713f, 1));

        electricity.Play();
    }

    void ShootFireBall()
    {

    }
}
