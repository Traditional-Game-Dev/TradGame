using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnObject : MonoBehaviour
{
    public int cashMoney;
    public GameObject wall;
    public int wallCost;
    public GameObject rock;
    public int rockCost;

    void Awake()
    {
        GameObject.Find("SpawnWall").GetComponentInChildren<Text>().text = $"Create Wall - ${wallCost}";
        GameObject.Find("SpawnRock").GetComponentInChildren<Text>().text = $"Create Rock - ${rockCost}";
    }

    public Dictionary<GameObject, int> getCosts()
    {
        Dictionary<GameObject, int> costs = new Dictionary<GameObject, int>();
        costs.Add(wall, wallCost);
        costs.Add(rock, rockCost);

        return costs;
    }

    public void createWall()
    {
        rock.SetActive(false);
        wall.SetActive(true);
    }

    public void createRock()
    {
        wall.SetActive(false);
        rock.SetActive(true);
    }

    void Update()
    {
        GameObject.Find("MoneyCount").GetComponent<Text>().text = cashMoney.ToString();

        if(cashMoney <= 0)
        {
            Camera.main.GetComponent<CameraController>().swapMode();
            cashMoney++;
        }
    }
}
