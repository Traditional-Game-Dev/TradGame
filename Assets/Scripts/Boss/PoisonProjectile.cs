using System.Collections;
using UnityEngine;

public class PoisonProjectile : MonoBehaviour
{
    public Transform projectileTransform;
    private GameObject target;
    private Transform targetTransform;

    private float firingAngle = 45.0f;
    private float gravity = 29.4f;
    private float damageInterval = 0.2f;

    private float emissionDuration;
    private float damageRadius;
    private int damageAmount;

    public void Begin(GameObject target, float emissionDuration, float damageRadius, int damageAmount)
    {
        this.target = target;
        targetTransform = target.transform;

        this.emissionDuration = emissionDuration;
        this.damageRadius = damageRadius;
        this.damageAmount = damageAmount;

        StartCoroutine(SimulateProjectile());
    }

    public IEnumerator SimulateProjectile()
    {
        yield return new WaitForSeconds(1.5f);

        float targetDistance = Vector3.Distance(projectileTransform.position, targetTransform.position);

        // calcualte the velocity needed to throw the object to the target
        float velocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // extract x and y components of velocity
        float vX = Mathf.Sqrt(velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float vY = Mathf.Sqrt(velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        float flightDuration = targetDistance / vX;

        // rotate projectile to face the target
        projectileTransform.rotation = Quaternion.LookRotation(targetTransform.position - projectileTransform.position);

        float elapsedTime = 0;

        // time to fly
        while (elapsedTime < flightDuration)
        {
            projectileTransform.Translate(0, (vY - (gravity * elapsedTime)) * Time.smoothDeltaTime, vX * Time.smoothDeltaTime);
            elapsedTime += Time.smoothDeltaTime;
            yield return null;
        }

        // finished flight, time to play some particles and possibly damage the target (player)
        projectileTransform.gameObject.GetComponentInChildren<ParticleSystem>().Play();

        elapsedTime = 0;
        float currDamageInterval = 0;

        while (elapsedTime < emissionDuration)
        {
            elapsedTime += Time.smoothDeltaTime;

            if ((projectileTransform.position - targetTransform.position).sqrMagnitude < damageRadius)
            {
                if (currDamageInterval <= elapsedTime)
                {
                    target.GetComponent<PlayerHealth>().damagePlayer(damageAmount);

                    currDamageInterval = elapsedTime + damageInterval;
                }
            }

            yield return null;
        }

        // wrap it up
        projectileTransform.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        GameObject.Destroy(projectileTransform.gameObject);
    }

}

