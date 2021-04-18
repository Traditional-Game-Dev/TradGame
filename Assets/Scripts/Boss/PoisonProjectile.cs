using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class PoisonProjectile : MonoBehaviour
{
    public Transform projectileTransform;
    private GameObject target;
    private Transform targetTransform;
    private Vector2 offset;

    private float firingAngle = 45.0f;
    private float gravity = 29.4f;
    private float damageInterval = 0.2f;

    private float emissionDuration;
    private float stoppingPoint;
    private float damageRadius;
    private int damageAmount;

    public void Begin(GameObject target, float emissionDuration, float damageRadius, int damageAmount)
    {
        this.target = target;
        targetTransform = target.transform;
        offset = new Vector2(0, 0);


        this.emissionDuration = emissionDuration;
        stoppingPoint = emissionDuration * 0.5f;
        this.damageRadius = damageRadius;
        this.damageAmount = damageAmount;

        StartCoroutine(ThrowBomb());
    }

    public void Begin(GameObject target, Vector2 offset, float emissionDuration, float damageRadius, int damageAmount)
    {
        this.target = target;
        targetTransform = target.transform;
        this.offset = offset;

        this.emissionDuration = emissionDuration;
        stoppingPoint = emissionDuration * 0.5f;
        this.damageRadius = damageRadius;
        this.damageAmount = damageAmount;

        StartCoroutine(ThrowBomb());
    }

    public IEnumerator ThrowBomb()
    {
        yield return new WaitForSeconds(1.5f);

        Vector3 targetPosition = new Vector3(targetTransform.position.x + offset.x, targetTransform.position.y, targetTransform.position.z + offset.y);

        float targetDistance = Vector3.Distance(projectileTransform.position, targetPosition);

        // calculate the velocity needed to throw the object to the target
        float velocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // extract x and y components of velocity
        float vX = Mathf.Sqrt(velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float vY = Mathf.Sqrt(velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        float flightDuration = targetDistance / vX + 0.025f;

        // rotate projectile to face the target
        projectileTransform.rotation = Quaternion.LookRotation(targetPosition - projectileTransform.position);

        float elapsedTime = 0;

        // time to fly
        while (elapsedTime < flightDuration)
        {
            projectileTransform.Translate(0, (vY - (gravity * elapsedTime)) * Time.smoothDeltaTime, vX * Time.smoothDeltaTime);
            elapsedTime += Time.smoothDeltaTime;
            yield return null;
        }

        // finished flight, time to play some particles and possibly damage the target (player)
        projectileTransform.gameObject.GetComponentInChildren<VisualEffect>().Play();

        elapsedTime = 0;
        float currDamageInterval = 0;
        bool hasStopped = false;

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

            if (elapsedTime > stoppingPoint)
            {
                projectileTransform.gameObject.transform.localScale *= 0.97f;
                damageRadius *= 0.985f; // look into
                if (!hasStopped)
                {
                    hasStopped = true;
                    projectileTransform.gameObject.GetComponentInChildren<VisualEffect>().playRate = 2.0f;
                    projectileTransform.gameObject.GetComponentInChildren<VisualEffect>().Stop();
                }
            }

            yield return null;
        }

        GameObject.Destroy(projectileTransform.gameObject);
    }

}

