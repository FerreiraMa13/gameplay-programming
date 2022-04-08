using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Controller : NPC_Controller
{
    public float jump_cooldown = 20.0f;

    [System.NonSerialized] public bool landed = true;

    private float jump_timer = 0.0f;
    SlimeAnimatorController animator;

    protected override void OwnAwake()
    {
        animator = GetComponentInChildren<SlimeAnimatorController>();
    }
    protected override void OwnUpdate()
    {
        if (patrol_route != null && enemy_state == enemyState.PATROLING)
        {
            patrol_route.active = !landed;
        }

        if(player_controller != null)
        {
            speed_muliplier = 2.0f;
        }
        else
        {
            speed_muliplier = 1.0f;
        }
        
        if (landed)
        {
            if (jump_timer < 0)
            {
                if (!attacking && enemy_state != enemyState.IDLE)
                {
                    animator.triggerJump();
                    landed = false;
                    jump_timer = 0;
                }
            }
            else
            {
                jump_timer -= Time.deltaTime;
            }
        }
    }
    public override void Deathrattle()
    {

    }
    protected override bool ConditionalMove()
    {
        return !landed;
    }
    public void ResetJump()
    {
        landed = true;
        jump_timer = jump_cooldown;
    }
}
