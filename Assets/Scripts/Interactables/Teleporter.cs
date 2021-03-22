using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Teleporter : MonoBehaviour
{
    private ParticleSystem particles;
    private Transform otherTeleporterTransform;
    private float baseEmissionRate;
    private float baseEmissionSpeed;

    public GameManager manager;

    void Start()
    {
        particles = this.GetComponentInChildren<ParticleSystem>();
        baseEmissionRate = particles.emission.rateOverTime.constantMax;
        baseEmissionSpeed = particles.main.simulationSpeed;

        otherTeleporterTransform = GameObject.FindGameObjectsWithTag("Teleporter").Where(x => x != this.gameObject).First().transform;
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && (manager.GetTeleportStatus() == false))
        {
            StartCoroutine(TryTeleport(other.gameObject));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            StartCoroutine(EndTeleport(other.gameObject));
        }
    }

    IEnumerator TryTeleport(GameObject player)
    {
        Vector3 startPlayerPosition = player.transform.position;

        var emission = particles.emission;
        float currEmissionRate = emission.rateOverTime.constantMax;
        var main = particles.main;
        float currSpeed = main.simulationSpeed;

        // wait for some time..
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.25f);

            currEmissionRate += 25.0f;
            currSpeed += 0.2f;

            emission.rateOverTime = currEmissionRate;
            main.simulationSpeed = currSpeed;
        }

        // .. and then check if player position is still similar
        if (player.activeSelf && (startPlayerPosition.magnitude - player.transform.position.magnitude) < 1.0f)
        {
            manager.SetTeleportStatus(true);

            // teleport
            player.transform.position = new Vector3(otherTeleporterTransform.position.x, 
                                                    player.transform.position.y, 
                                                    otherTeleporterTransform.position.z);
        }

        yield return null;
    }

    IEnumerator EndTeleport(GameObject player)
    {
        var emission = particles.emission;
        float currEmissionRate = emission.rateOverTime.constantMax;
        var main = particles.main;
        float currSpeed = main.simulationSpeed;

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.25f);

            // only reduce emission if we're above the base state
            currEmissionRate -= currEmissionRate > baseEmissionRate ? 25.0f : 0.0f;
            currSpeed -= currSpeed > baseEmissionSpeed ? 0.2f : 0.0f;

            emission.rateOverTime = currEmissionRate;
            main.simulationSpeed = currSpeed;
        }
    }

    public void ResetState()
    {
        StopAllCoroutines();

        var emission = particles.emission;
        emission.rateOverTime = baseEmissionRate;
        var main = particles.main;
        main.simulationSpeed = baseEmissionSpeed;
    }
}
