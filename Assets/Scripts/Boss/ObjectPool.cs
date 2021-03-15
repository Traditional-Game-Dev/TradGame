using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public List<GameObject> pooledObjects;

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

    public void DestroyPool()
    {
        for(int i = 0; i < pooledObjects.Count-1; i++)
        {
            GameObject.Destroy(pooledObjects[i]);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count-1; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void ActivateAllPooled()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].SetActive(true);
        }
    }

    public void DeactivateAllPooled()
    {
        for(int i = 0; i < pooledObjects.Count-1; i++)
        {
            pooledObjects[i].SetActive(false);
        }
    }
}
