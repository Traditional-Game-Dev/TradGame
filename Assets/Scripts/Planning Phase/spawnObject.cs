using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnObject : MonoBehaviour
{
    public int cashMoney;
    public GameObject ruinsWall;
    public int ruinsWallCost;
    public GameObject wall;
    public int wallCost;
    public GameObject rock;
    public int rockCost;
    public GameObject teleporter;
    public int teleporterCost;

    void Awake()
    {
        GameObject.Find("SpawnWall").GetComponentInChildren<Text>().text = $"Create Wall - ${wallCost}";
        GameObject.Find("SpawnRock").GetComponentInChildren<Text>().text = $"Create Rock - ${rockCost}";
        GameObject.Find("SpawnRuins").GetComponentInChildren<Text>().text = $"Create Wide Wall - ${ruinsWallCost}";
        GameObject.Find("SpawnTeleporter").GetComponentInChildren<Text>().text = $"Create Teleporter - ${teleporterCost}";
    }

    public Dictionary<GameObject, int> getCosts()
    {
        Dictionary<GameObject, int> costs = new Dictionary<GameObject, int>();
        costs.Add(wall, wallCost);
        costs.Add(rock, rockCost);
        costs.Add(ruinsWall, ruinsWallCost);
        costs.Add(teleporter, teleporterCost);

        return costs;
    }

    public void createWall()
    {
        rock.SetActive(false);
        ruinsWall.SetActive(false);
        teleporter.SetActive(false);
        wall.SetActive(true);
    }

    public void createRock()
    {
        wall.SetActive(false);
        ruinsWall.SetActive(false);
        teleporter.SetActive(false);
        rock.SetActive(true);
    }

    public void createRuinsWall()
    {
        wall.SetActive(false);
        rock.SetActive(false);
        teleporter.SetActive(false);
        ruinsWall.SetActive(true);
    }

    public void createTeleporter()
    {
        wall.SetActive(false);
        rock.SetActive(false);
        ruinsWall.SetActive(false);
        teleporter.SetActive(true);
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
