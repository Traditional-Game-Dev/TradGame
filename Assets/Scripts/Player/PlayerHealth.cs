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
    public GameManager manager;

    private PlayerController playerController;
    private float MaxDashTime;

    void Start()
    {
        currentHP = totalHp;
        currentLives = totalLives;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        MaxDashTime = playerController.GetMaxDashTime();
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
                manager.ResetTeleporters();
            }
        }

        //TODO: Update HP bar (temp implementation done)
        healthUI.text = $"HP: {currentHP}/{totalHp}";

        //TODO: Update Lives Counter (temp implementation done)
        livesUI.text = $"Lives: {currentLives}/{totalLives}";

    }

    public void damagePlayer(int totalDamage)
    {
        if (playerController.GetCurrentDashTime() >= MaxDashTime && !manager.PlayerInvin())
        {
            currentHP -= totalDamage;
        }
    }
}
