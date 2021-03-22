using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Teleporter : MonoBehaviour
{
    private ParticleSystem particles;


    void Start()
    {
        particles = this.GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
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
        Vector3 currPlayerPosition = player.transform.position;

        var emission = particles.emission;
        float currEmissionRate = emission.rateOverTime.constantMax;
        var main = particles.main;
        float currSpeed = main.simulationSpeed;

        // wait and then check if position is still similar
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.5f);

            currEmissionRate += 25.0f;
            currSpeed += 0.2f;

            emission.rateOverTime = currEmissionRate;
            main.simulationSpeed = currSpeed;
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
            yield return new WaitForSeconds(0.5f);

            currEmissionRate -= 25.0f;
            currSpeed -= 0.2f;

            emission.rateOverTime = currEmissionRate;
            main.simulationSpeed = currSpeed;
        }
    }
}
