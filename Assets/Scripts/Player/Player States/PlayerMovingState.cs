using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovingState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
    }

    public override void Update(PlayerController player)
    {
        player.anim.Play("Jog");

        if (player.GetDirectionMag() < 0.1)
        {
            player.TransitionToState(player.IdleState);
        }
    }

}
