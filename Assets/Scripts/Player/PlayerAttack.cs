using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerAttack : MonoBehaviour
{
    public VisualEffect lightning;
    public GameObject fireball;
    public float comboTimeWindow;
    public float damageRange;
    public float lightningDamage;
    public float fireballDamage;
    public float fireballDuration;
    //public float comboCooldown = 25f; // future use

    private GameManager manager;
    private Transform playerTransform;
    private Transform bossTransform;
    //private int maxCombo = 3;
    private int comboCounter = 0;
    private bool isRed = true;
    private float heightOffset = 0.5f;
    private float handOffset = 0.5f;

    public InputActionAsset playerControls;
    private InputAction attack;

    void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bossTransform = GameObject.Find("Boss").transform;

        var gameplayActionMap = playerControls.FindActionMap("Gameplay");

        lightning.Stop();

        attack = gameplayActionMap.FindAction("Attack");
        attack.performed += ctx =>
        {
            ComboAttack();
        };
        attack.Enable();

        playerTransform = gameObject.transform;
    }

    void FixedUpdate()
    {
        float newHandOffset = isRed ? -handOffset : handOffset;

        if (lightning.isActiveAndEnabled)
        {
            // set emission position (calculate offset for the right or left hand)
            if (playerTransform.localEulerAngles.y <= 315.0f && playerTransform.localEulerAngles.y >= 225.0f)
            {
                lightning.gameObject.transform.position = transform.forward + new Vector3(playerTransform.position.x,
                                                                                            playerTransform.position.y + heightOffset,
                                                                                            playerTransform.position.z + newHandOffset);
            }
            else if (playerTransform.localEulerAngles.y >= 45.0f && playerTransform.localEulerAngles.y <= 135.0f)
            {
                lightning.gameObject.transform.position = transform.forward + new Vector3(playerTransform.position.x,
                                                                                            playerTransform.position.y + heightOffset,
                                                                                            playerTransform.position.z - newHandOffset);
            }
            else
            {
                lightning.gameObject.transform.position = transform.forward + new Vector3(playerTransform.position.x - newHandOffset,
                                                                                            playerTransform.position.y + heightOffset,
                                                                                            playerTransform.position.z);

            }
        }
    }

    // *add delay interval once animations are in place
    void ComboAttack()
    {
        if (manager.playerDisabled)
        {
            return;
        }

        comboCounter++;

        switch (comboCounter)
        {
            case 1:
                PrepareLightning(isRed = true);
                ShootRedLightning();
                break;
            case 2:
                PrepareLightning(isRed = false);
                ShootBlueLightning();
                break;
            case 3:
                PrepareFireball();
                StartCoroutine(ShootFireball());
                break;
            default:
                break;
        }

        if (comboCounter > 2)
        {
            comboCounter = 0;
        }
    }

    void PrepareFireball()
    {
        fireball.transform.position = transform.forward + new Vector3(playerTransform.position.x,
                                                                      playerTransform.position.y,
                                                                      playerTransform.position.z);

        fireball.transform.rotation = new Quaternion(fireball.transform.rotation.x,
                                                     playerTransform.rotation.y,
                                                     fireball.transform.rotation.z,
                                                     playerTransform.rotation.w);
    }

    IEnumerator ShootFireball()
    {
        fireball.SetActive(true);

        float elapsedTime = 0.0f;
        Vector3 startPosition = fireball.transform.position;
        Vector3 targetPosition = fireball.transform.position + transform.forward * damageRange;

        while (elapsedTime < fireballDuration)
        {
            if ((Mathf.Abs(fireball.transform.position.x - bossTransform.position.x) +
                Mathf.Abs(fireball.transform.position.z - bossTransform.position.z)) < 1.5f)
            {
                break;
            }

            fireball.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / fireballDuration);
            elapsedTime += Time.smoothDeltaTime;

            yield return null;
        }

        if ((Mathf.Abs(fireball.transform.position.x - bossTransform.position.x) +
            Mathf.Abs(fireball.transform.position.z - bossTransform.position.z)) < 1.5f)
        {
            Light light = fireball.GetComponentInChildren<Light>();
            float originalIntensity = light.intensity;

            Vector3 originalScale = fireball.transform.localScale;

            float explosionTime = 0.25f;
            elapsedTime = 0.0f;
            while (elapsedTime < explosionTime)
            {
                fireball.transform.localScale *= 1.10f;
                light.intensity *= 1.10f;

                elapsedTime += Time.fixedDeltaTime;

                yield return null;
            }

            manager.HitBoss(fireballDamage);

            fireball.transform.localScale = originalScale;
            light.intensity = originalIntensity;
        }

        fireball.SetActive(false);

        yield return null;
    }

    void PrepareLightning(bool isRed)
    {
        this.isRed = isRed;
        float newHandOffset = isRed ? -handOffset : handOffset;

        // set emission position (calculate offset for the right or left hand)
        if (playerTransform.localEulerAngles.y <= 315.0f && playerTransform.localEulerAngles.y >= 225.0f)
        {
            lightning.gameObject.transform.position = transform.forward + new Vector3(playerTransform.position.x,
                                                                                        playerTransform.position.y + heightOffset,
                                                                                        playerTransform.position.z + newHandOffset);
        }
        else if (playerTransform.localEulerAngles.y >= 45.0f && playerTransform.localEulerAngles.y <= 135.0f)
        {
            lightning.gameObject.transform.position = transform.forward + new Vector3(playerTransform.position.x,
                                                                                        playerTransform.position.y + heightOffset,
                                                                                        playerTransform.position.z - newHandOffset);
        }
        else
        {
            lightning.gameObject.transform.position = transform.forward + new Vector3(playerTransform.position.x - newHandOffset,
                                                                                        playerTransform.position.y + heightOffset,
                                                                                        playerTransform.position.z);

        }

        // set emission rotation
        lightning.gameObject.transform.rotation = new Quaternion(lightning.transform.rotation.x,
                                                                   playerTransform.rotation.y,
                                                                   lightning.transform.rotation.z,
                                                                   playerTransform.rotation.w);
    }

    void ShootBlueLightning()
    {
        lightning.SetVector4("BoltColor", new Vector4(0.356f, 0.772f, 0.984f, 1));
        lightning.SetVector4("FlashColor", new Vector4(0.713f, 1.545f, 1.968f, 1));

        lightning.Play();

        TryDamage(lightningDamage);
    }

    void ShootRedLightning()
    {
        lightning.SetVector4("BoltColor", new Vector4(0.984f, 0.356f, 0.356f, 1));
        lightning.SetVector4("FlashColor", new Vector4(1.968f, 0.713f, 0.713f, 1));

        lightning.Play();

        TryDamage(lightningDamage);
    }

    void TryDamage(float damage)
    {
        if (Vector3.Distance(bossTransform.position, transform.position) < damageRange)
        {
            manager.HitBoss(damage);
        }
    }
}
