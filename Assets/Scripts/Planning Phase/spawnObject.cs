using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnObject : MonoBehaviour
{
    public int planningMana;
    [System.NonSerialized] public int mana;
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
        GameObject.Find("SpawnWall").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Wall\n{wallCost}";
        GameObject.Find("SpawnRock").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Rock\n{rockCost}";
        GameObject.Find("SpawnRuins").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Wide Wall\n{ruinsWallCost}";
        GameObject.Find("SpawnTeleporter").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Teleporter\n{teleporterCost}";
        GameObject.Find("ManaBarSlider").GetComponent<Slider>().maxValue = planningMana;

        mana = planningMana;
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
        GameObject.Find("MoneyCount").GetComponent<Text>().text = $"{mana} / {planningMana}";
        GameObject.Find("ManaBarSlider").GetComponent<Slider>().value = mana;


        if(mana <= 0)
        {
            Camera.main.GetComponent<CameraController>().swapMode();
            mana++;
        }
    }
}
