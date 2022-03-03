using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovController : MonoBehaviour
{
    AnimationStateController animation_controller;
    CharacterController controller;
    GameplayPlayerController controls;
    Vector2 move;
    public float speed = 10.0f;
    float speed_multiplier = 1.0f;
    public float deadzone = 0.1F;
    public float turn_smooth_time = 0.1f;
    public Transform cam_transform;
    private float turn_smooth_velocity;

    bool jumping = false;
    public float jump_force = 10.0f;
    private float jump_velocity = 0.0f;
    public float gravity = 9.8f;
    private float additional_decay = 0.0f;

    bool attacking = false;
    bool attacked = false;

    bool falling = false;

    private void Awake()
    {
        controls = new GameplayPlayerController();
        controller = GetComponent<CharacterController>();
        animation_controller = GetComponentInChildren<AnimationStateController>();
        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;

        controls.Player.Jump.started += ctx => Jump();
        controls.Player.Attack.started += ctx => Attack();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    void SendMessage(Vector2 coordinates)
    {
        Debug.Log("Thumb-stick coordinates = " + coordinates);
    }
    void SendMessage()
    {
        Debug.Log("Input Detected");
    }

    void FixedUpdate()
    {
        HandleAttack();
        if(!attacking)
        {
            HandleMovement();
            HandleJump();
        }
    }

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleMovement()
    {
        Vector3 input_direction = new Vector3(move.x, 0.0f, move.y);

        speed_multiplier = 0.5f + 2 * input_direction.magnitude;
        input_direction.Normalize();
        float rotateAngle = Mathf.Atan2(input_direction.x, input_direction.z) * Mathf.Rad2Deg + cam_transform.eulerAngles.y;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotateAngle, ref turn_smooth_velocity, turn_smooth_time);
        transform.rotation = Quaternion.Euler(0.0f, smoothAngle, 0.0f);
        Vector3 camForward = Quaternion.Euler(0.0f, rotateAngle, 0.0f).normalized * Vector3.forward;
        Vector3 movement = Vector3.zero;
        movement = camForward * speed * speed_multiplier;


        if (!Compare2Deadzone(move.x) && !Compare2Deadzone(move.y))
        {
            movement = Vector3.zero;
        }
        movement.y = jump_velocity;

        controller.Move(movement * Time.deltaTime);
    }

    private void HandleAnimations()
    {
        if(!jumping)
        {
            Vector3 input_direction = new(move.x, 0.0f, move.y);
            animation_controller.updateMovement(input_direction.magnitude);
        }
    }

    private void HandleJump()
    {
        if(controller.isGrounded)
        {
            if (jumping && jump_velocity < 0.0f)
            {
                jumping = false;
                animation_controller.triggerLand();
                additional_decay = 0.0f;
            }
            else if(falling && jump_velocity < 0.0f)
            {
                falling = false;
                animation_controller.triggerLand();
                additional_decay = 0.0f;
            }
            jump_velocity = -gravity * Time.deltaTime;
        }
        else
        {
            if(!falling && !jumping)
            {
                falling = true;
                animation_controller.triggerFall();
            }
            jump_velocity -= (gravity * Time.deltaTime) + additional_decay ;
            additional_decay += 0.2f * speed * Time.deltaTime;
        }
     
    }

    private void HandleAttack()
    {
        if(!jumping && !falling)
        {
            if (attacking && !attacked)
            {
                animation_controller.triggerAttack();
                attacked = true;
            }
        }
    }

    private void Jump()
    {
        if(!jumping)
        {
            animation_controller.triggerJump();
            jumping = true;
            jump_velocity = jump_force;
            additional_decay = 0.0f;
        }
    }
    
    private void Attack()
    {
        if(!jumping && !attacked && !falling)
        {
            attacking = true;
        }
    }

    private bool Compare2Deadzone( float value)
    {
        if (value < deadzone)
        {
            if(value > - deadzone)
            {
                return false;
            }
        }
        return true;
    }

    public void endAttack()
    {
        attacking = false;
        attacked = false;
    }

    public void detectHit()
    {
        Debug.Log("Hit");
    }
}
