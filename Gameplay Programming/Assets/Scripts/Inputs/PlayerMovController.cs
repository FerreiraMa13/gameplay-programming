using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovController : MonoBehaviour
{
    GameplayPlayerController controls;
    Vector2 move;
    public float speed = 10.0f;
    public float deadzone = 0.1F;
    public float turnSmoothTime = 0.1f;
    public Transform camTransform;
    private float turnSmoothVelocity;

    private void Awake()
    {
        controls = new GameplayPlayerController();
        controls.Player.Jump.performed += ctx => SendMessage();
        controls.Player.Move.performed += ctx => SendMessage();
        controls.Player.Move.performed += ctx =>
                                      SendMessage(ctx.ReadValue<Vector2>());
        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;
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
        /*Vector3 movement = new Vector3(move.x, 0.0f, move.y) * speed * Time.deltaTime;
        if(movement.magnitude >= deadzone)
        {
            float rotateAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotateAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, smoothAngle, 0.0f);
            Vector3 camForward = Quaternion.Euler(0.0f, rotateAngle, 0.0f).normalized * Vector3.forward;

            transform.Translate(camForward, Space.World);
        }*/

        Vector3 input_direction = new Vector3(move.x, 0.0f, move.y);
        if (input_direction.magnitude >= deadzone)
        {
            
            float rotateAngle = Mathf.Atan2(input_direction.x, input_direction.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotateAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, smoothAngle, 0.0f);
            Vector3 camForward = Quaternion.Euler(0.0f, rotateAngle, 0.0f).normalized * Vector3.forward;

            camForward = camForward * speed * Time.deltaTime;

            transform.Translate(camForward, Space.World);
        }

        /*Vector3 movement = new Vector3(move.x, 0.0f, move.y) * speed * Time.deltaTime;
        if (movement.magnitude >= deadzone)
        {
            float rotateAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotateAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, smoothAngle, 0.0f);

            Vector3 camForward = Quaternion.Euler(0.0f, rotateAngle, 0.0f) * Vector3.forward;
            transform.Translate(camForward, Space.World);
        }*/
    }
}
