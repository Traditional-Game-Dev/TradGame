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
        rock.SetActive(false);

        if(cashMoney > 0)
        {
            cashMoney--;
            wall.SetActive(true);
        }
    }

    public void createRock()
    {
        wall.SetActive(false);

        if(cashMoney > 0)
        {
            cashMoney--;
            rock.SetActive(true);
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
