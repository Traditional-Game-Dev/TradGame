using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Teleporter : MonoBehaviour
{
    public Material teleportingPlayerMat;
    public Material dissolveMat;

    private GameManager manager;
    private ParticleSystem particles;
    private Transform otherTeleporterTransform;
    private float baseEmissionRate;
    private float baseEmissionSpeed;

    private readonly int numIntervals = 100; // was 10
    private readonly float intervalLength = 0.025f; // was 0.25
    private readonly float transferDuration = 4.0f;

    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        particles = this.GetComponentInChildren<ParticleSystem>();
        baseEmissionRate = particles.emission.rateOverTime.constantMax;
        baseEmissionSpeed = particles.main.simulationSpeed;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (!manager.JustTeleported()))
        {
            StartCoroutine(TryTeleport(other.gameObject));
        }
    }

    private IEnumerator TryTeleport(GameObject player)
    {
        SkinnedMeshRenderer playerMesh = player.GetComponentInChildren<SkinnedMeshRenderer>();
        Material originalPlayerMat = playerMesh.material;

        Vector3 startPlayerPosition = player.transform.position;

        var emission = particles.emission;
        float currEmissionRate = emission.rateOverTime.constantMax;
        var main = particles.main;
        float currSpeed = main.simulationSpeed;

        int halftime = numIntervals / 2;
        // wait for some time..
        for (int i = 0; i < numIntervals; i++)
        {
            yield return new WaitForSeconds(intervalLength);

            currEmissionRate += 2.5f; // was 25
            currSpeed += 0.025f; // was 0.2

            emission.rateOverTime = currEmissionRate;
            main.simulationSpeed = currSpeed;

            if (i == halftime && player.activeInHierarchy && (startPlayerPosition - player.transform.position).magnitude < 3.0f)
            {
                manager.playerIvin = true;
                manager.playerDisabled = true;

                //playerMesh.material = teleportingPlayerMat;
                playerMesh.material = dissolveMat;
                playerMesh.material.SetFloat("_Amount", -1.0f);
            }
            else if (i == halftime && (!player.activeInHierarchy || (startPlayerPosition - player.transform.position).magnitude >= 3.0f))
            {
                // reset teleporter
                StartCoroutine(EndTeleport());

                // stop the current coroutine
                yield break;
            }

            if (i > halftime)
            {
                float nextAmount = playerMesh.material.GetFloat("_Amount") + 0.05f;
                playerMesh.material.SetFloat("_Amount", nextAmount);
            }
        }

        // .. and then do a sanity check (the player should be frozen and invincible at this point)
        if (player.activeInHierarchy)
        {
            manager.Teleported();

            playerMesh.enabled = false;

            Vector3 startPosition = player.transform.position;

            Vector3 endPosition = new Vector3(otherTeleporterTransform.position.x, 
                                              player.transform.position.y, 
                                              otherTeleporterTransform.position.z);

            manager.EnableSlowMotion(transferDuration);

            // linear interp the line between the player and other teleporter,
            // uses 'ease in/out lerp' formula for smooth camera transition
            float elapsedTime = 0.0f;
            while (elapsedTime < transferDuration)
            {
                float t = elapsedTime / transferDuration;
                t = t * t * (3.0f - 2.0f * t);

                player.transform.position = Vector3.Lerp(startPosition, endPosition, t);

                elapsedTime += Time.fixedDeltaTime;

                yield return null;
            }

            player.transform.position = endPosition;

            playerMesh.enabled = true;
        }

        // time to clean up
        yield return new WaitForSeconds(intervalLength);
        StartCoroutine(EndTeleport());

        if (player.activeInHierarchy)
        {
            // un-dissolve
            for (int i = 0; i < halftime; i++) 
            {
                yield return new WaitForSeconds(intervalLength);

                float nextAmount = playerMesh.material.GetFloat("_Amount") - 0.05f;
                playerMesh.material.SetFloat("_Amount", nextAmount);
            }
        }
        else
        {
            // hot swap to regular material
            // don't try to reassign material if player gameobject is not active
            while (!player.activeInHierarchy)
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        playerMesh.material = originalPlayerMat;
        manager.playerDisabled = false;
        manager.playerIvin = false;

        yield return null;
    }

    private IEnumerator EndTeleport()
    {
        var emission = particles.emission;
        float currEmissionRate = emission.rateOverTime.constantMax;
        var main = particles.main;
        float currSpeed = main.simulationSpeed;

        // only reduce emission if we're above the base state
        while (currEmissionRate > baseEmissionRate || currSpeed > baseEmissionSpeed)
        {
            yield return new WaitForSeconds(intervalLength);

            currEmissionRate -= currEmissionRate > baseEmissionRate ? 25.0f : 0.0f;
            currSpeed -= currSpeed > baseEmissionSpeed ? 0.2f : 0.0f;

            emission.rateOverTime = currEmissionRate;
            main.simulationSpeed = currSpeed;
        }

        yield return null;
    }

    public void ResetState()
    {
        //StopAllCoroutines();

        var emission = particles.emission;
        emission.rateOverTime = baseEmissionRate;
        var main = particles.main;
        main.simulationSpeed = baseEmissionSpeed;
    }

    public void UpdateParticleColor(Color color)
    {
        var main = particles.main;
        main.startColor = new MinMaxGradient(color);
    }

    public void LinkToOtherTeleporter(Transform otherTransform)
    {
        otherTeleporterTransform = otherTransform;
    }
}
