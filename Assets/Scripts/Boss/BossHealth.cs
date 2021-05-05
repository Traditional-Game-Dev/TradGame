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
    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;
    public GameObject wall;
    public GameObject wallHitbox;
    public LineRenderer lineRenderer;
    public GameObject laserLight;
    public ParticleSystem laserImpact;
    public ParticleSystem laserWarmUp;
    public GameObject cutscene;

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
        //TODO: CHANGE ME BACK TO 33
        if (currentHealth <= 99 && gameManager.currLevel == 1){
            cutscene.SetActive(true);
            gameManager.currLevel = 2;
            //rock1.SetActive(false);
            //rock2.SetActive(false);
            //rock3.SetActive(false);
            wall.SetActive(false);
            wallHitbox.SetActive(false);
            lineRenderer.enabled = false;
            laserImpact.Stop();
            laserWarmUp.Stop();
            laserLight.SetActive(false); 
            boss.GetComponent<BossController>().anim.speed = 1;
            boss.GetComponent<BossController>().transitioningLevels = 1;
            boss.GetComponent<BossController>().TransitionToState(boss.GetComponent<BossController>().IdleState);
            boss.transform.position = new Vector3(27, boss.transform.position.y, 858);
        }
        else if (currentHealth <= 0 && gameManager.currLevel == 2)
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
