using System;
using System.Collections;
using UnityEngine;

class Projectile
{
    public IEnumerator SimulateProjectile(Transform targetTransform, Vector2 offset, Transform projectileTransform, float firingAngle, float gravity)
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
    }
}
