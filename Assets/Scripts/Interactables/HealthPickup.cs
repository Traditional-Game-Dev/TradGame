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
        if (other.gameObject.tag.Equals("Player") && !onCooldown)
        {
            PlayerHealth healthScript = other.GetComponent<PlayerHealth>();

            if (healthScript.currentHP < 100)
            {
                int healAmount = healthScript.currentHP > 90 ? healthScript.currentHP - 100 : -10;

                other.GetComponent<PlayerHealth>().damagePlayer(healAmount);

                StartCoroutine(Cooldown());
            }
        }
    }

    private IEnumerator Cooldown()
    {
        onCooldown = true;
        heartRenderer.enabled = false;
        spotLight.enabled = false;

        yield return new WaitForSeconds(cooldownLength);

        spotLight.enabled = true;
        heartRenderer.enabled = true;
        onCooldown = false;

        yield return null;
    }
}
