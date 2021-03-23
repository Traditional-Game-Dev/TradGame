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

    public Material teleportingPlayerMat;
    public GameManager manager;
    public int waitIntervals;

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

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag.Equals("Player"))
    //    {
    //        StartCoroutine(EndTeleport(other.gameObject));
    //    }
    //}

    IEnumerator TryTeleport(GameObject player)
    {
        Material originalPlayerMat = player.GetComponentInChildren<SkinnedMeshRenderer>().material;
        Vector3 startPlayerPosition = player.transform.position;

        var emission = particles.emission;
        float currEmissionRate = emission.rateOverTime.constantMax;
        var main = particles.main;
        float currSpeed = main.simulationSpeed;

        int halftime = waitIntervals / 2;
        // wait for some time..
        for (int i = 0; i < waitIntervals; i++)
        {
            yield return new WaitForSeconds(0.25f);

            currEmissionRate += 25.0f;
            currSpeed += 0.2f;

            emission.rateOverTime = currEmissionRate;
            main.simulationSpeed = currSpeed;

            if (i == halftime)
            {
                manager.SetPlayerInvin(true);

                manager.EnableSlowMotion(5.0f); // remove constant

                player.GetComponentInChildren<SkinnedMeshRenderer>().material = teleportingPlayerMat;
            }
        }

        // .. and then check if player position is still similar
        if (player.activeInHierarchy && (startPlayerPosition.magnitude - player.transform.position.magnitude) < 2.0f)
        {
            manager.SetTeleportStatus(true);

            // teleport
            player.transform.position = new Vector3(otherTeleporterTransform.position.x, 
                                                    player.transform.position.y, 
                                                    otherTeleporterTransform.position.z);
        }

        // time to clean up
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(EndTeleport());

        // don't try to reassign material if player gameobject is not active
        while (!player.activeInHierarchy)
        {
            yield return new WaitForSeconds(0.1f);
        }

        player.GetComponentInChildren<SkinnedMeshRenderer>().material = originalPlayerMat;
        manager.SetPlayerInvin(false);


        yield return null;
    }

    IEnumerator EndTeleport()
    {
        var emission = particles.emission;
        float currEmissionRate = emission.rateOverTime.constantMax;
        var main = particles.main;
        float currSpeed = main.simulationSpeed;

        for (int i = 0; i < waitIntervals; i++)
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
        //StopAllCoroutines();

        var emission = particles.emission;
        emission.rateOverTime = baseEmissionRate;
        var main = particles.main;
        main.simulationSpeed = baseEmissionSpeed;
    }
}
