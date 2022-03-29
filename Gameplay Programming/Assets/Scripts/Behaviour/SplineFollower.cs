using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineFollower : MonoBehaviour
{
    public enum SplineFollowerMode
    { 
        NONE = 0,
        AUTO = 1,
        INDEPENDENT = 2,
        SYNCHRONISED = 3
    }
    public BezierSpline spline;
    public SplineFollowerMode movementMode = SplineFollowerMode.NONE;
    bool valid = false;
    /*[System.NonSerialized]*/
    public bool active = true;
    [System.NonSerialized]
    PlayerMovController player;

    [Header("Auto Movement")]
    public float timeTaken = 10.0f;
    float timeStep;
    float progress = 0.0f;

    [System.NonSerialized]
    public Vector3 movementVector = Vector3.zero;
    float moveDirection = 1.0f;

    private void OnValidate()
    {
        timeStep = 1 / timeTaken;
        valid = (spline != null);
        if(player != null)
        {
            player.restricted = active;
        }
    }
    private void Awake()
    {
        player = gameObject.GetComponent<PlayerMovController>();
    }
    private void Update()
    {
        if(valid && active)
        {
            Move();
        }
    }
    private void Move()
    {
        if (valid)
        {
            switch(movementMode)
            {
                case (SplineFollowerMode.AUTO):
                {
                        AutoMove();
                        break;
                }
                case (SplineFollowerMode.INDEPENDENT):
                {
                        if(active)
                        {
                            IndependentMove();
                        }
                        break;
                }
            }
            
        }
    }
    private void AutoMove()
    {
        progress += moveDirection * timeStep * Time.deltaTime;
        if (progress > 1)
        {
            progress = 1;
            moveDirection = moveDirection * -1.0f;
        }
        else if (progress < 0)
        {
            progress = 0;
            moveDirection = moveDirection * -1.0f;
        }
        transform.position = spline.GetPoint(progress);
    }
    private void IndependentMove()
    {
        if(player != null)
        {
            movementVector = new Vector3(player.move.x, 0.0f, player.move.y);
        }
        if(movementVector.x > 0)
        {
            moveDirection = 1.0f;
        }
        else if(movementVector.x < 0)
        {
            moveDirection = -1.0f;
        }

        /*Vector3 new_pos = new Vector3(spline.GetPoint(progress).x, transform.position.y, spline.GetPoint(progress).z);
        transform.position = new_pos;*/

        /*Vector3 spline_direction = spline.GetDirection(progress);
        float rotateAngle = Mathf.Atan2(spline_direction.x, spline_direction.z) * Mathf.Rad2Deg + transform.rotation.y;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotateAngle, ref turn_smooth_velocity, turn_smooth_time);
        if(moveDirection < 0 && smoothAngle > 0)
        {
            smoothAngle = smoothAngle * -1.0f;
        }
        transform.rotation = Quaternion.Euler(0.0f, smoothAngle, 0.0f) ;*/

        if (player.ApproachPoint(spline.GetPoint(progress)))
        {
            progress += movementVector.magnitude  * moveDirection * player.speed * timeStep * Time.deltaTime;
            if (progress > 1)
            {
                progress = 1;
                /*active = false;
                player.restricted = false;*/
            }
            if (progress < 0)
            {
                progress = 0;
                /*active = false;
                player.restricted = false;*/
            }
        }
    }
}
