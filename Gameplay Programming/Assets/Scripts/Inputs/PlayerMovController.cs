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
    public float deadzone = 0.1F;
    public float turn_smooth_time = 0.1f;
    public Transform cam_transform;
    private float turn_smooth_velocity;

    bool jumping = false;
    public float init_jump_velocity;
    public float max_jumping_height;
    public float max_jump_time;
    public float gravity = -9.8f;


    private void Awake()
    {
        controls = new GameplayPlayerController();
        controller = GetComponent<CharacterController>();
        animation_controller = GetComponentInChildren<AnimationStateController>();
        controls.Player.Move.performed += ctx => SendMessage(ctx.ReadValue<Vector2>());
        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;

        controls.Player.Jump.started += ctx => SendMessage();
        controls.Player.Jump.started += ctx => jumping = true;
        controls.Player.Jump.canceled += ctx => jumping = false;
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
        HandleMovement();
    }

    private void Update()
    {
        HandleAnimations();
    }

    private void HandleMovement()
    {

        Vector3 input_direction = new Vector3(move.x, 0.0f, move.y).normalized;

        float rotateAngle = Mathf.Atan2(input_direction.x, input_direction.z) * Mathf.Rad2Deg + cam_transform.eulerAngles.y;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotateAngle, ref turn_smooth_velocity, turn_smooth_time);
        transform.rotation = Quaternion.Euler(0.0f, smoothAngle, 0.0f);
        Vector3 camForward = Quaternion.Euler(0.0f, rotateAngle, 0.0f).normalized * Vector3.forward;
        Vector3 movement = speed * camForward;
        //transform.Translate(camForward, Space.World);
        if (!compare_to_deadzone(move.x) && !compare_to_deadzone(move.y))
        {
            movement = new Vector3(0, 0, 0);
        }
        controller.SimpleMove(movement);

    }

    private void HandleAnimations()
    {
        Vector3 input_direction = new(move.x, 0.0f, move.y);
        animation_controller.updateMovement(input_direction.magnitude);
    }

    private bool compare_to_deadzone( float value)
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
}
