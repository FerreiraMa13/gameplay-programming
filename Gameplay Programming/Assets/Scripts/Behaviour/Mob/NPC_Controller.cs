using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    [Header("Character Stats")]
    public float character_hp = 10.0f;
    public float damage = 5.0f;
    public float attack_range = 5.0f;
    public float attack_speed = 5.0f;
    public float movement_speed = 5.0f;
    public float character_armor = 0.0f;

    [System.NonSerialized] public float distance_from_target;
    [System.NonSerialized] public bool line_of_sight;

    PlayerMovController player_controller;

    private void Update()
    {
        if(player_controller != null)
        {
            distance_from_target = Vector3.Distance(transform.position, player_controller.transform.position);
            line_of_sight = !(Physics.Linecast(transform.position, player_controller.transform.position));
        }
        else
        {
            distance_from_target = -1;
            line_of_sight = false;
        }
    }
    public void TakeDamage(float damage_taken)
    {
        damage_taken -= character_armor;
        if(damage_taken > 0)
        {
            character_hp -= damage_taken;
        }
        if(character_hp < 0)
        {
            
        }
    }
    public void Die()
    {
        Deathrattle();
        if(character_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Deathrattle()
    {
        Debug.Log("Mob Died");
    }
    public bool DamageConditions()
    {
        return player_controller.hit && line_of_sight && (distance_from_target <= player_controller.attack_range + transform.localScale.x/2 && distance_from_target > 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player_controller = other.gameObject.GetComponent<PlayerMovController>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player_controller = other.gameObject.GetComponent<PlayerMovController>();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(DamageConditions())
            {
                TakeDamage(player_controller.damage);
            }
        }
    }
}
