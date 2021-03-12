using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState
{
    private float attackRate = 0.01f;

    public override void EnterState(BossController boss)
    {
        boss.anim.Play("Idle");
    }

    public override void Update(BossController boss)
    {
        if (attackRate > Random.Range(0.0f, 1.0f))
        {
            boss.TransitionToState(boss.LaserAttackState);
        }
    }
}
