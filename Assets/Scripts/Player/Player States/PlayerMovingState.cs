using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovingState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.anim.SetBool("Jogging", true);
        player.anim.speed = 7;
    }

    public override void Update(PlayerController player)
    {
        if (player.GetDirectionMag() < 0.1)
        {
            player.TransitionToState(player.IdleState);
        }
    }

}
