using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main <c>ObjectPool</c> class
/// </summary>
/// <remarks>
/// Creates large ammounts of duplicate objects on runtime to increase performance in game
/// </remarks>
public class ObjectPool
{
    public List<GameObject> pooledObjects;

    /// <summary>
    /// Create an ObjectPool of type <paramref name="objectToPool"/> and size <paramref name="ammountToPool"/>
    /// </summary>
    public void CreatePool(GameObject objectToPool, int ammountToPool)
    {
        pooledObjects = new List<GameObject>();
        GameObject temp;
        for(int i = 0; i < ammountToPool; i++)
        {
            temp = GameObject.Instantiate(objectToPool) as GameObject;
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    /// <summary>
    /// Completely destroy all pooled objects, removing them from the scene
    /// </summary>
    public void DestroyPool()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            GameObject.Destroy(pooledObjects[i]);
        }
    }

    /// <summary>
    /// Get a reference to an GameObject from the pool, already in scene
    /// </summary>
    /// <remarks>
    /// All objects returned are already instantiated, just disabled
    /// </remarks>
    /// <returns>
    /// GameObject from the pool
    /// </returns>
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Activates all objects in pool
    /// </summary>
    /// <remarks>
    /// With larger pools this can cause a decent performance hit, be careful
    /// </remarks>
    public void ActivateAllPooled()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].SetActive(true);
        }
    }

    /// <summary>
    /// Deactivates all objects in pool
    /// </summary>
    /// <remarks>
    /// Essentially resets the pool to right after CreatePool() was called
    /// </remarks>
    public void DeactivateAllPooled()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].SetActive(false);
        }
    }
}
