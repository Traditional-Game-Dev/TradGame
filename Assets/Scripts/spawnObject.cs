using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObject : MonoBehaviour
{
    public GameObject wall;
    public GameObject rock;

    public void createWall()
    {
        Instantiate(wall);
    }

    public void createRock()
    {
        Instantiate(rock);
    }
}
