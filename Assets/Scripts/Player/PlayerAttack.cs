using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerAttack : MonoBehaviour
{
    [Header("Combo:")]
    public float timeBetweenAttacks;
    public float comboTimeWindow;
    //public float comboCooldown = 25f; // possible future use
    [Header("Lightning:")]
    public VisualEffect lightning;
    public float lightningDamage;
    [Header("Fireball:")]
    public GameObject fireball;
    public VisualEffect fireballImpact;
    public float fireballDamage;
    public float fireballRange;
    public float fireballLifetime;

    private GameManager manager;
    private Transform playerTransform;
    private Transform bossTransform;
    private Light fireballLight;
    private Collider lightningCollider;
    private int comboCounter = 0;
    private bool isRed = true;
    private float heightOffset = 0.5f;
    private float handOffset = 0.5f;
    private bool canAttack = true;

    public InputActionAsset playerControls;
    private InputAction attack;

    void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bossTransform = GameObject.Find("Boss").transform;

        fireballLight = fireball.GetComponentInChildren<Light>();
        lightningCollider = lightning.gameObject.GetComponent<Collider>();

        var gameplayActionMap = playerControls.FindActionMap("Gameplay");

        lightning.Stop();

        attack = gameplayActionMap.FindAction("Attack");

        attack.Enable();

        playerTransform = gameObject.transform;
    }

    void FixedUpdate()
    {
        if (attack.triggered)
        {
            ComboAttack();
        }

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
        if (!canAttack || manager.playerDisabled)
        {
            return;
        }

        comboCounter++;

        switch (comboCounter)
        {
            case 1:
                PrepareLightning(isRed = true);
                ShootLightning(isRed = true);
                break;
            case 2:
                PrepareLightning(isRed = false);
                ShootLightning(isRed = false);
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

        StartCoroutine(RestInterval());
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
        Vector3 targetPosition = fireball.transform.position + transform.forward * fireballRange;

        while (elapsedTime < fireballLifetime)
        {
            if (manager.justHitBoss)
            {
                break;
            }

            fireball.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / fireballLifetime);
            elapsedTime += Time.smoothDeltaTime;

            yield return null;
        }

        if (manager.justHitBoss)
        {
            float originalIntensity = fireballLight.intensity;

            Vector3 originalScale = fireball.transform.localScale;

            fireballImpact.Play();

            float explosionTime = 0.15f;
            elapsedTime = 0.0f;
            while (elapsedTime < explosionTime)
            {
                fireball.transform.localScale *= 1.05f;
                fireballLight.intensity *= 1.10f;

                elapsedTime += Time.fixedDeltaTime;

                yield return null;
            }

            manager.DamageBoss(fireballDamage);

            fireball.transform.localScale = originalScale;
            fireballLight.intensity = originalIntensity;
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

    void ShootLightning(bool isRed)
    {
        lightning.gameObject.SetActive(true);

        if (isRed)
        {
            lightning.SetVector4("BoltColor", new Vector4(0.984f, 0.356f, 0.356f, 1));
            lightning.SetVector4("FlashColor", new Vector4(1.968f, 0.713f, 0.713f, 1));
        }
        else
        {
            lightning.SetVector4("BoltColor", new Vector4(0.356f, 0.772f, 0.984f, 1));
            lightning.SetVector4("FlashColor", new Vector4(0.713f, 1.545f, 1.968f, 1));
        }

        if (lightningCollider.bounds.Contains(new Vector3(bossTransform.position.x, 0.0f, bossTransform.position.z)))
        {
            manager.justHitBoss = true;
            lightning.SetBool("HitBoss", true);
            float dist = Vector3.Distance(new Vector3(bossTransform.position.x, 0.0f, bossTransform.position.z),
                                            new Vector3(playerTransform.position.x, 0.0f, playerTransform.position.z));
            lightning.SetFloat("ImpactOffsetZ", dist / 2); // weird, but works for now
        }

        lightning.Play();

        if (manager.justHitBoss)
        {
            manager.DamageBoss(lightningDamage);
        }
    }

    IEnumerator RestInterval()
    {
        canAttack = false;

        yield return new WaitForSeconds(timeBetweenAttacks);

        lightning.SetBool("HitBoss", false);
        lightning.SetFloat("ImpactOffsetZ", 6.0f);
        lightning.gameObject.SetActive(false);

        canAttack = true;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = new Color(1, 0, 0, 0.5f);
    //    Gizmos.DrawCube(lightning.GetComponent<BoxCollider>().center, lightning.GetComponent<BoxCollider>().size);
    //}
}
