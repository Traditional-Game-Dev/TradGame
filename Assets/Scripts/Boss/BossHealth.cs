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
    private GameManager gameManager;
    private GameObject boss;
    public BossController bossControl;
    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;
    public GameObject wall;
    public LineRenderer lineRenderer;
    public GameObject laserLight;
    public ParticleSystem laserImpact;
    public ParticleSystem laserWarmUp;
    public GameObject cutscene;
    public AudioClip deathRattle;
    public GameObject deathScene;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        boss = GameObject.Find("Boss");

        bossMeshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Awake()
    {

    }

    public void ReceiveDamage(float damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        StartCoroutine(BossDamageColor());
        if (currentHealth <= 33 && gameManager.currLevel == 1){
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            cutscene.SetActive(true);
            gameManager.currLevel = 2;
            lineRenderer.enabled = false;
            laserImpact.Stop();
            laserWarmUp.Stop();
            laserLight.SetActive(false); 
            boss.GetComponent<BossController>().anim.speed = 1;
            boss.GetComponent<BossController>().transitioningLevels = 1;
            boss.GetComponent<BossController>().TransitionToState(boss.GetComponent<BossController>().IdleState);
        }
        else if (currentHealth <= 0 && gameManager.currLevel == 2)
        {
            lineRenderer.enabled = false;
            laserImpact.Stop();
            laserWarmUp.Stop();
            laserLight.SetActive(false); 
            boss.GetComponent<BossController>().anim.speed = 1;
            boss.GetComponent<BossController>().transitioningLevels = 1;
            boss.GetComponent<BossController>().TransitionToState(boss.GetComponent<BossController>().IdleState);
            bossControl.anim.SetTrigger("Die");
            boss.GetComponent<AudioSource>().clip = deathRattle;
            boss.GetComponent<AudioSource>().Play();
            deathScene.SetActive(true);

        }
    }

    public void moveBoss(){
        boss.transform.position = new Vector3(27, boss.transform.position.y, 858);
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

    public void endGame()
    {
        SceneManager.LoadScene("Win Screen");
    }
}
