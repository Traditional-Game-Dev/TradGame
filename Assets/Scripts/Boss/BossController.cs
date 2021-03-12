using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject player;
    public float bossAttackTime;
    public float bossWaitTime;
    public LineRenderer lineRenderer;
    public ParticleSystem laserImpact;
    public Animator anim;
    public float radius;
    public float circleSpeed;
    public float circleDivide;
    public float hitboxOffset;
    public int damageDealt;
    public int headOffset;

    [System.NonSerialized] public bool planningPhase = true;

    private BossBaseState currentState;

    public BossBaseState CurrentState
    {
        get { return currentState; }
    }

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
