using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int totalHp;
    public int totalLives;
    public Text healthUI;
    public Text livesUI;
    [System.NonSerialized] public int currentHP;
    [System.NonSerialized] public int currentLives;

    void Start()
    {
        currentHP = totalHp;
        currentLives = totalLives;
    }

    void Update()
    {
        //If the player dies
        if(currentHP <= 0)
        {
            //If the player runs out of lives
            if(currentLives <= 1)
            {
                //TODO: End game or restart or whatever
                SceneManager.LoadScene("Game Over");
            }
            else
            {
                currentLives--;
                currentHP = totalHp;
            }
        }

        //TODO: Update HP bar (temp implementation done)
        healthUI.text = $"HP: {currentHP}/{totalHp}";

        //TODO: Update Lives Counter (temp implementation done)
        livesUI.text = $"Lives: {currentLives}/{totalLives}";

    }

    public void damagePlayer(int totalDamage)
    {
        currentHP -= totalDamage;
    }
}
