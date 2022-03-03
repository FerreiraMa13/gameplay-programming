using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Up : MonoBehaviour
{
    public enum PowerUpType
    {
        NULL = 0,
        ONCE = 1,
        SPAWN = 2,
        ETERNAL = 3
    }

    public enum PowerUpEffects
    {
        NULL = 0,
        DOUBLE_JUMP = 1,
        SPEED_BOOST = 2
    }

    public bool active = true;
    [System.NonSerialized]
    public PowerUpType type = PowerUpType.ONCE;
    public GameObject particles;
    [System.NonSerialized]
    public GameObject player;
    public float duration = 5.0f;
    [System.NonSerialized]
    public PowerUpEffects powerUpEffect = PowerUpEffects.NULL;


    public void Update()
    {
        if(!active)
        {
            if(type != PowerUpType.SPAWN)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other .CompareTag("Player"))
        {
            player = other.gameObject;
            PickUp();
        }
    }
    public void PickUp()
    {
        if (active)
        {
            if (particles != null)
            {
                Instantiate(particles, transform.position, transform.rotation);
            }

            if (type != PowerUpType.ETERNAL)
            {
                active = false;
            }
            ActivateEffect();

        }
    }
    public void ActivateEffect()
    {
        player.GetComponent<PlayerMovController>().affectPlayer(powerUpEffect, duration);
        Debug.Log("Effect Activated");
    }

}
