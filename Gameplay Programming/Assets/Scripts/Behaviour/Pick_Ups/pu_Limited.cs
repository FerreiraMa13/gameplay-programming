using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pu_Limited : Pick_Up
{
    [System.NonSerialized]
    new public PowerUpType type = PowerUpType.LIMITED;
    public float respawn_timer_duration = 3.0f;
    public float respawn_timer = 0.0f;
    public int lives = 3;

    new void Update()
    {
        if (!active)
        {
            if (respawn_timer > 0.0f)
            {
                respawn_timer -= Time.deltaTime;
                if (gameObject.GetComponent<MeshRenderer>().enabled)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
            }
            else
            {
                if(lives > 0)
                {
                    respawn_timer = respawn_timer_duration;
                    active = true;
                    lives -= 1;
                    gameObject.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }
}
