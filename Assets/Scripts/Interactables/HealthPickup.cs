using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HealthPickup : MonoBehaviour
{
    public float cooldownLength;

    private bool onCooldown = false;
    private Transform heartTransform;
    private Renderer heartRenderer;
    private Light spotLight;

    private void Start()
    {
        heartTransform = this.transform.Find("HealthPickup").transform;
        heartRenderer = heartTransform.gameObject.GetComponent<Renderer>();

        spotLight = transform.Find("Spot Light").GetComponent<Light>();
    }

    private void FixedUpdate()
    {
        heartTransform.Rotate(0.0f, 45.0f * Time.fixedDeltaTime, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !onCooldown)
        {
            PlayerHealth healthScript = other.GetComponent<PlayerHealth>();

            if (healthScript.currentHP < 100)
            {
                int healAmount = healthScript.currentHP > 90 ? 100 - healthScript.currentHP : 10;

                other.GetComponent<PlayerHealth>().healPlayer(healAmount);

                StartCoroutine(Cooldown());
            }
        }
    }

    private IEnumerator Cooldown()
    {
        Vector3 originalScale = heartTransform.localScale;
        heartTransform.localScale *= 0.1f;

        onCooldown = true;
        heartRenderer.enabled = false;
        spotLight.enabled = false;

        yield return new WaitForSeconds(cooldownLength);

        spotLight.enabled = true;
        heartRenderer.enabled = true;
        onCooldown = false;

        while (heartTransform.localScale.magnitude < originalScale.magnitude)
        {
            heartTransform.localScale *= 1.05f;

            yield return null;
        }

        heartTransform.localScale = originalScale;

        yield return null;
    }
}
