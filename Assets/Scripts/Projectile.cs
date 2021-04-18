using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void Begin(Transform projectileTransform, Vector3 targetPosition, Vector2 offset, float firingAngle, float gravity)
    {
        StartCoroutine(SimulateProjectile(projectileTransform, targetPosition, offset, firingAngle, gravity));
    }

    public IEnumerator SimulateProjectile(Transform projectileTransform, Vector3 targetPosition, Vector2 offset, float firingAngle, float gravity)
    {
        yield return new WaitForSeconds(1.5f);

        Vector3 newTargetPosition = new Vector3(targetPosition.x + offset.x, targetPosition.y, targetPosition.z + offset.y);

        float targetDistance = Vector3.Distance(projectileTransform.position, targetPosition);

        // calculate the velocity needed to throw the object to the target
        float velocity = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // extract x and y components of velocity
        float vX = Mathf.Sqrt(velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float vY = Mathf.Sqrt(velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        //float flightDuration = targetDistance / vX + 0.025f;
        float flightDuration = targetDistance / vX;

        // rotate projectile to face the target
        projectileTransform.rotation = Quaternion.LookRotation(newTargetPosition - projectileTransform.position);

        float elapsedTime = 0;

        // time to fly
        while (elapsedTime < flightDuration)
        {
            projectileTransform.Translate(0, (vY - (gravity * elapsedTime)) * Time.smoothDeltaTime, vX * Time.smoothDeltaTime);
            elapsedTime += Time.smoothDeltaTime;
            yield return null;
        }

        GameObject.Destroy(this);
    }
}
