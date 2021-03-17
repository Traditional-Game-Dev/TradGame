using System.Collections;
using UnityEngine;

public class PoisonProjectile : MonoBehaviour
{
    private float firingAngle = 45.0f;
    private float gravity = 29.4f;

    private float emissionDuration = 5.0f;
    private float damageInterval = 0.2f;
    private float damageRange = 100.0f;
    private int damageAmount = 1;

    public Transform projectileTransform;
    private GameObject target;
    private Transform targetTransform;

    public void Begin(GameObject target)
    {
        this.target = target;
        this.targetTransform = target.transform;

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

        while (elapsedTime < flightDuration)
        {
            projectileTransform.Translate(0, (vY - (gravity * elapsedTime)) * Time.smoothDeltaTime, vX * Time.smoothDeltaTime);
            elapsedTime += Time.smoothDeltaTime;
            yield return null;
        }

        projectileTransform.gameObject.GetComponentInChildren<ParticleSystem>().Play();

        elapsedTime = 0;
        float currDamageInterval = 0;

        while (elapsedTime < emissionDuration)
        {
            elapsedTime += Time.smoothDeltaTime;

            if ((projectileTransform.position - targetTransform.position).sqrMagnitude < damageRange)
            {
                if (currDamageInterval <= elapsedTime)
                {
                    target.GetComponent<PlayerHealth>().damagePlayer(damageAmount);

                    currDamageInterval = elapsedTime + damageInterval;
                }
            }

            yield return null;
        }

        projectileTransform.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        GameObject.Destroy(projectileTransform.gameObject);
    }

}

