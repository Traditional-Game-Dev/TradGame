using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public HealthBar healthBar;
    public GameObject boss;
    public Material damagedBossMat;
    public int range;
    public int damageInflicted;
    public InputActionAsset playerControls;

    private SkinnedMeshRenderer bossMeshRenderer;

    private InputAction attack;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        bossMeshRenderer = boss.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Awake()
    {
        var gameplayActionMap = playerControls.FindActionMap("Gameplay");
        attack = gameplayActionMap.FindAction("Attack");
        attack.performed += ctx =>
        {
            if (Vector3.Distance(boss.transform.position, transform.position) < range)
            {
                dealDamage(damageInflicted);
            }
        };
        attack.Enable();
    }

    void dealDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        StartCoroutine(BossDamageColor());

        if (currentHealth <= 0)
        {
            SceneManager.LoadScene("winScreen");
        }
    }

    IEnumerator BossDamageColor()
    {
        Material originalBossMat = bossMeshRenderer.material;
        bossMeshRenderer.material = damagedBossMat;

        yield return new WaitForSeconds(0.25f);

        bossMeshRenderer.material = originalBossMat;
    }
}
