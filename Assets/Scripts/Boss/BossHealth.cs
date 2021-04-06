using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    public Material damagedBossMat;
    public HealthBar healthBar;
    public float maxHealth;

    private SkinnedMeshRenderer bossMeshRenderer;
    private float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        bossMeshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Awake()
    {

    }

    public void ReceiveDamage(float damage){
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
        Material originalBossMat = bossMeshRenderer.sharedMaterial;

        if (originalBossMat == damagedBossMat)
        {
            yield break;
        }

        bossMeshRenderer.material = damagedBossMat;

        yield return new WaitForSeconds(0.25f);

        bossMeshRenderer.material = originalBossMat;
    }
}
