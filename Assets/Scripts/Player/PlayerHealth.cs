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
    public GameObject livesUI;
    [System.NonSerialized] public int currentHP;
    [System.NonSerialized] public int currentLives;
    public GameManager manager;

    void Start()
    {
        currentHP = totalHp;
        currentLives = totalLives;
        livesUI.GetComponent<LivesSprites>().UpdateLivesImage(currentLives + 1);
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
                livesUI.GetComponent<LivesSprites>().UpdateLivesImage(currentLives + 1);
                currentHP = totalHp;
                Camera.main.GetComponent<CameraController>().swapMode();
                manager.ResetTeleporters();
            }
        }

        //TODO: Update HP bar (temp implementation done)
        healthUI.text = $"HP: {currentHP}/{totalHp}";

        //TODO: Update Lives Counter (temp implementation done)
        //livesUI.text = $"Lives: {currentLives}/{totalLives}";
    }

    void OnDisable()
    {
        currentHP = totalHp;
        gameObject.transform.position = new Vector3(0f, 1f, -50f);

    }

    void OnEnable()
    {
        currentHP = totalHp;
    }

    public void damagePlayer(int totalDamage)
    {
        if (!manager.playerIvin)
        {
            currentHP -= totalDamage;
        }
    }

    public void healPlayer(int totalHeal)
    {
        currentHP += totalHeal;
    }
}
