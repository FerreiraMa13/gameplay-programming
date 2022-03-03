using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pu_Spawnable : Pick_Up
{
    new PowerUpType type = PowerUpType.SPAWN;
    public float respawn_timer_duration = 5.0f;
    public float respawn_timer = 0.0f;

    new void Update()
    {
        if(!active)
        {
            if(respawn_timer > 0.0f)
            {
                respawn_timer -= Time.deltaTime;
                if(gameObject.GetComponent<MeshRenderer>().enabled)
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
            }
            else
            {
                respawn_timer = respawn_timer_duration;
                active = true;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
