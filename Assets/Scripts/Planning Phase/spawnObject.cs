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
    public GameObject healthPickup;
    public int healthPickupCost;
    public GameObject reflector;
    public int reflectorCost;


    void Awake()
    {
        GameObject.Find("SpawnWall").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Wall\n{wallCost}";
        GameObject.Find("SpawnRock").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Rock\n{rockCost}";
        GameObject.Find("SpawnRuins").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Wide Wall\n{ruinsWallCost}";
        GameObject.Find("SpawnTeleporter").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Teleporter\n{teleporterCost}";
        GameObject.Find("SpawnReflector").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Reflector\n{reflectorCost}";
        GameObject.Find("SpawnHealthPad").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"Health Pad\n{healthPickupCost}";
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
        costs.Add(reflector, reflectorCost);
        costs.Add(healthPickup, healthPickupCost);

        return costs;
    }

    public void createWall()
    {
        deactivateAll();
        wall.SetActive(true);
    }

    public void createRock()
    {
        deactivateAll();
        rock.SetActive(true);
    }

    public void createRuinsWall()
    {
        deactivateAll();
        ruinsWall.SetActive(true);
    }
    public void createTeleporter()
    {
        deactivateAll();
        teleporter.SetActive(true);
    }

    public void createHealth()
    {
        deactivateAll();
        healthPickup.SetActive(true);
    }

    public void createReflector()
    {
        deactivateAll();
        reflector.SetActive(true);
    }

    private void deactivateAll()
    {
        wall.SetActive(false);
        rock.SetActive(false);
        ruinsWall.SetActive(false);
        teleporter.SetActive(false);
        healthPickup.SetActive(false);
        reflector.SetActive(false);
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
