using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.anim.SetBool("Jogging", false);
    }

    public override void Update(PlayerController player)
    {
        if (player.GetDirectionMag() >= 0.1)
        {
            player.TransitionToState(player.MovingState);
        }
    }

}
