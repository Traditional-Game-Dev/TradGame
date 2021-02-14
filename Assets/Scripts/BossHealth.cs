using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public GameObject player;
    public int range;
    public int damageInflicted;
    public InputActionAsset playerControls;
    private InputAction attack;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Awake()
    {
        var gameplayActionMap = playerControls.FindActionMap("Gameplay");
        attack = gameplayActionMap.FindAction("Attack");
        attack.performed += ctx =>
        {
            if (Vector3.Distance(transform.position, player.transform.position) < range)
            {
                TakeDamage(damageInflicted);
            }
        };
        attack.Enable();
    }

    void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
