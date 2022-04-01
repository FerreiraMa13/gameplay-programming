using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterForce : MonoBehaviour
{
    PlayerMovController controller;
    public float water_speed_multiplier = 0.8f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            controller = other.gameObject.GetComponent<PlayerMovController>();
            controller.speed_boost = water_speed_multiplier;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            controller.speed_boost = 1.0f;
            controller = null;
        }
    }
}
