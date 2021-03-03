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

    private PlayerMovement playerMoveScript;
    private float MaxDashTime;

    void Start()
    {
        currentHP = totalHp;
        currentLives = totalLives;

        playerMoveScript = GameObject.Find("Player").GetComponent<PlayerMovement>();
        MaxDashTime = playerMoveScript.GetMaxDashTime();
    }

    void Update()
    {
        //If the player dies
        if(currentHP <= 0)
        {
            //If the player runs out of lives
            if(currentLives <= 1)
            {
                SceneManager.LoadScene("Game Over");
            }
            else
            {
                currentLives--;
                currentHP = totalHp;
                Camera.main.GetComponent<CameraController>().swapMode();
            }
        }

        //TODO: Update HP bar (temp implementation done)
        healthUI.text = $"HP: {currentHP}/{totalHp}";

        //TODO: Update Lives Counter (temp implementation done)
        livesUI.text = $"Lives: {currentLives}/{totalLives}";

    }

    public void damagePlayer(int totalDamage)
    {
        if (playerMoveScript.GetCurrentDashTime() >= MaxDashTime)
        {
            currentHP -= totalDamage;
        }
    }
}
