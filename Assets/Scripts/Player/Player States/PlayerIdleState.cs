using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    private float timeToWatch;
    private float timer;

    public override void EnterState(PlayerController player)
    {
        player.anim.SetBool("Jogging", false);
        timeToWatch = 5f;
        timer = 0f;
        player.anim.speed = 1;
    }

    public override void Update(PlayerController player)
    {
        if (player.GetDirectionMag() >= 0.1 && player.dashing != true)
        {
            player.TransitionToState(player.MovingState);
            timer = 0f;        
        }
        else{
            player.anim.SetBool("Jogging", false);
            timer += Time.deltaTime;
            if (timer > timeToWatch)
            {
                player.anim.speed = 1;
                player.anim.SetTrigger("LookAtWatch");
                timer -= timeToWatch;
                timeToWatch = 10f;
            }
        }
    }

}
