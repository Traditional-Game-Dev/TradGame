using Assets.Scripts.Boss.BossEnums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("General Settings:")]
    public GameObject player;
    public Animator anim;
    public float bossAttackTime;
    public float bossWaitTime;
    [System.NonSerialized] public BossAttacks prevAttack = BossAttacks.First;

    [Header("Laser Attack:")]
    public LineRenderer lineRenderer;
    public ParticleSystem laserImpact;
    public GameObject laserLight;
    public ParticleSystem laserWarmUp;
    [Range(10, 50)] public float circleSpeed;
    [Range(1, 100)] public int laserDamage;
    [Tooltip("Player center aim adjustment")]
    public int headOffset;
    [Tooltip("Adjusts laser collision, do not change")]
    public float hitboxOffset;
    [Tooltip("Number of points laser hits on the circle, do not change")]
    public float circleDivide;
    [Tooltip("The length of time the warmup animation will play for before firing laser")]
    public float timeForWarmup;
    [Tooltip("Use this to adjust how much the speed of the animation is reduced")]
    public float slowDown;

    [Header("Bullet Hell Attack:")]
    public GameObject bulletObject;
    [Range(1, 100)] public int bulletDamage;
    [Range(1, 500)] public int bulletCount;
    [System.NonSerialized] public float bulletStartDegree;
    [System.NonSerialized] public float bulletEndDegree;
    [Range(0, 360)]public float width;
    [Range(50.0f, 75.0f)] public float bulletSpeed;
    [System.NonSerialized] public ObjectPool bulletPool = new ObjectPool();
    public float timeForLoad;

    [Header("Poison Cloud Attack:")]
    public GameObject poisonObject;
    [Range(1, 100)] public int poisonDamage;
    [Range(50, 500)] public int poisonRadius;
    [Range(1, 10)] public float emissionDuration;
    public float timeForAim;

    [Header("Stomp Attack:")]
    [Range(1, 100)] public float stompRadius;
    public GameObject genericProjectile;

    [System.NonSerialized] public PoisonType poisonType = PoisonType.Normal;

    [System.NonSerialized] public bool planningPhase = true;

    private BossBaseState currentState;
    public BossBaseState CurrentState { get => currentState; }

    public readonly BossIdleState IdleState = new BossIdleState();
    public readonly BossLaserAttackState LaserAttackState = new BossLaserAttackState();
    public readonly BossBulletState BulletState = new BossBulletState();
    public readonly BossPoisonState PoisonState = new BossPoisonState();
    public readonly BossLaserPreState LaserPreState = new BossLaserPreState();
    public readonly BossPoisonPreState PoisonPreState = new BossPoisonPreState();
    public readonly BossBulletHellPreAttack BulletPreState = new BossBulletHellPreAttack();
    public readonly BossPoisonDefenseState PoisonDefenseState = new BossPoisonDefenseState();
    public readonly BossPoisonFarState PoisonFarState = new BossPoisonFarState();
    public readonly BossStompPreState StompPreState = new BossStompPreState();
    public readonly BossStompState StompState = new BossStompState();

    public void TransitionToState(BossBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void Start()
    {
        laserImpact.Stop();
        laserWarmUp.Stop();
        lineRenderer.enabled = false;
        //TODO: Find Ratio of BulletCount to PoolSize (Optimization Task)
        bulletPool.CreatePool(bulletObject, bulletCount/2);

        TransitionToState(IdleState);
    }

    void Update()
    {
        //currentState.Update(this);
    }

    void FixedUpdate()
    {
        if (!planningPhase)
        {
            currentState.Update(this);
        }
    }
}
