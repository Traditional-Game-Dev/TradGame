using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnObject : MonoBehaviour
{
    public int cashMoney;
    public GameObject wall;
    public GameObject rock;

    public void createWall()
    {
        var rock = GameObject.Find("Rock1Blueprint(Clone)");
        Debug.Log(rock);
        if(rock != null)
        {
            Destroy(rock);
        }

        if(cashMoney > 0 && GameObject.Find("WallBlueprint(Clone)") == null)
        {
            cashMoney--;
            Instantiate(wall);
        }
    }

    public void createRock()
    {
        var wall = GameObject.Find("WallBlueprint(Clone)");
        if(wall != null)
        {
            Destroy(wall);
        }

        if(cashMoney > 0 && GameObject.Find("Rock1Blueprint(Clone)") == null)
        {
            cashMoney--;
            Instantiate(rock);
        }
    }

    void Update()
    {
        foreach(Transform child in transform)
        {
            if(child.name == "MoneyCount")
            {
                child.gameObject.GetComponent<Text>().text = cashMoney.ToString();
            }
        }
    }
}
