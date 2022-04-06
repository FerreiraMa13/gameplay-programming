using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    public enum enemyState
    {
        INVALID = -1,
        IDLE = 0,
        ROAMING = 1,
        PATROLING = 2,
        DISENGAGING = 3,
        CHASING = 4
    }
    public enemyState default_state = enemyState.IDLE;
    public SplineFollower patrol_route;
    [Header("Character Stats")]
    public float character_hp = 10.0f;
    public float damage = 5.0f;
    public float attack_range = 5.0f;
    public float attack_speed = 5.0f;
    public float movement_speed = 5.0f;
    public float reaction_speed = 2.0f;
    public float character_armor = 0.0f;

    [System.NonSerialized] public float distance_from_target;
    [System.NonSerialized] public bool line_of_sight;
    [System.NonSerialized] public bool damage_calc;
    [System.NonSerialized] public enemyState enemy_state;

    private float reaction_timer = 0.0f;
    private float turn_smooth_velocity;
    private float turn_smooth_time = 0.1f;
    private Vector3 target_destination;

    PlayerMovController player_controller;

    private void OnValidate()
    {
        patrol_route = GetComponent<SplineFollower>();
        if (default_state == enemyState.PATROLING && patrol_route != null)
        {
            this.transform.position = patrol_route.spline.GetPoint(0);
        }
    }
    private void Update()
    {
        if (player_controller != null)
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
    private void FixedUpdate()
    {
        enemy_state = Move();
    }
    public void TakeDamage(float damage_taken)
    {
        Debug.Log(damage_taken);
        damage_taken -= character_armor;
        if (damage_taken > 0)
        {
            character_hp -= damage_taken;
        }
        if (character_hp < 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Deathrattle();
        if (character_hp <= 0)
        {
            if (player_controller != null)
            {
                player_controller.RemoveNPC(gameObject.GetComponent<NPC_Controller>());
            }
            Destroy(gameObject);
        }
    }
    public void Deathrattle()
    {
        Debug.Log("Mob Died");
    }
    public bool DamageConditions()
    {
        return player_controller.hit && line_of_sight && (distance_from_target <= player_controller.attack_range + transform.localScale.x / 2 && distance_from_target > 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player_controller = other.gameObject.GetComponent<PlayerMovController>();
            player_controller.AddNPC(gameObject.GetComponent<NPC_Controller>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player_controller = other.gameObject.GetComponent<PlayerMovController>();
            player_controller.RemoveNPC(gameObject.GetComponent<NPC_Controller>());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            /*if(DamageConditions())
            {
                TakeDamage(player_controller.damage);
            }*/
        }
    }
    private enemyState Move()
    {
        float dt = Time.deltaTime;
        switch (enemy_state)
        {
            case (enemyState.IDLE):
                {
                    return Idle(dt);
                }
            case (enemyState.ROAMING):
                {
                    return Roam(dt);
                }
            case (enemyState.PATROLING):
                {
                    return Patrol(dt);
                }
            case (enemyState.DISENGAGING):
                {
                    return Disengage(dt);
                }
            case (enemyState.CHASING):
                {
                    return Chase(dt);
                }
        }
        return enemyState.INVALID;
    }
    private enemyState Idle(float dt)
    {
        if (player_controller != null)
        {
            return enemyState.CHASING;
        }
        return enemyState.IDLE;
    }
    private enemyState Patrol(float dt)
    {
        return enemyState.PATROLING;
    }
    private enemyState Roam(float dt)
    {
        return enemyState.ROAMING;
    }
    private enemyState Disengage(float dt)
    {
        return enemyState.DISENGAGING;
    }
    private enemyState Chase(float dt)
    {
        if (player_controller != null)
        {
            if (reaction_timer <= 0)
            {
                target_destination = player_controller.transform.position;
                reaction_timer = reaction_speed;
            }
            else
            {
                reaction_timer -= dt;
            }

            MoveTowards(target_destination);
            return enemyState.CHASING;
        }
        return default_state;
    }
    public void MoveTowards(Vector3 destination)
    {
        var offset = destination - transform.position;
        Vector3 rotate = RotateCalc(offset, destination.y);
        Vector3 movement = XZMoveCalc(rotate);
        Vector3 next_pos = transform.position + movement;
        transform.position = Vector3.Lerp(transform.position, next_pos, Time.deltaTime);
    }
    private Vector3 RotateCalc(Vector3 direction, float anchor_rotation)
    {

        direction.Normalize();
        float rotateAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + anchor_rotation;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotateAngle, ref turn_smooth_velocity, turn_smooth_time);
        transform.rotation = Quaternion.Euler(0.0f, smoothAngle, 0.0f);

        return new Vector3(0.0f, rotateAngle, 0.0f);
    }
    private Vector3 XZMoveCalc(Vector3 direction)
    {
        Vector3 forward = Quaternion.Euler(direction).normalized * Vector3.forward;
        Vector3 movement = forward * movement_speed;
        return movement;
    }
}
