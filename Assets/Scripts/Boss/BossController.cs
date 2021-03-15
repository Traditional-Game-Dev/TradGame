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

    [Header("Laser Attack:")]
    public LineRenderer lineRenderer;
    public ParticleSystem laserImpact;
    [Range(10, 50)] public float circleSpeed;
    [Range(1, 100)] public int laserDamage;
    [Tooltip("Player center aim adjustment")]
    public int headOffset;
    [Tooltip("Adjusts laser collision, do not change")]
    public float hitboxOffset;
    [Tooltip("Number of points laser hits on the circle, do not change")]
    public float circleDivide;

    [Header("Bullet Hell Attack:")]
    [Range(1, 100)] public int bulletDamage;
    public int bulletCount;
    public int bulletRadius;

    [Header("Poison Cloud Attack:")]
    [Range(1, 100)] public int poisonDamage;
    public int poisonRadius;

    [System.NonSerialized] public bool planningPhase = true;

    private BossBaseState currentState;
    public BossBaseState CurrentState { get => currentState; }

    public readonly BossIdleState IdleState = new BossIdleState();
    public readonly BossLaserAttackState LaserAttackState = new BossLaserAttackState();

    public void TransitionToState(BossBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void Start()
    {
        laserImpact.Stop();
        lineRenderer.enabled = false;

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
