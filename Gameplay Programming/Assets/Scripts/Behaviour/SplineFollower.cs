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
    [System.NonSerialized]
    public float progress = 0.0f;

    [Header("Auto Movement")]
    public float timeTaken = 10.0f;
    float timeStep;

    [System.NonSerialized]
    public Vector3 movementVector = Vector3.zero;
    float moveDirection = 1.0f;

    [Header("Synchonised Movement")]
    public SplineFollower coordenator;
    [System.NonSerialized]
    public float synched_progress = 0.0f;

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
            StandardMove();
        }
    }
    private void FixedUpdate()
    {
        if (valid && active)
        {
            FixedMove();
        }
    }
    private void StandardMove()
    {
        if (valid)
        {
            if(active)
            {
                switch (movementMode)
                {

                    case (SplineFollowerMode.INDEPENDENT):
                        {
                            if (active)
                            {
                                IndependentMove();
                            }
                            break;
                        }
                    case (SplineFollowerMode.AUTO):
                        {
                            AutoMove();
                            break;
                        }
                    case (SplineFollowerMode.SYNCHRONISED):
                        {
                            SynchMove();
                            break;
                        }
                }
            }
        }
    }
    private void FixedMove()
    {
        if (valid)
        {
            if (active)
            {
                switch (movementMode)
                {
                    /*case (SplineFollowerMode.AUTO):
                        {
                            AutoMove();
                            break;
                        }
                    case (SplineFollowerMode.SYNCHRONISED):
                        {
                            SynchMove();
                            break;
                        }*/
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
    private void SynchMove()
    {
        synched_progress = coordenator.progress;
        progress = synched_progress;

        if (progress > 1) { progress = 1; }else if(progress < 0) { progress = 0; }

        Vector3 lerp_pos = Vector3.Slerp(transform.position, spline.GetPoint(progress), 2f);
        transform.position = lerp_pos;
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

        if (player.ApproachPoint(spline.GetPoint(progress)))
        {
            progress += movementVector.magnitude  * moveDirection * player.speed * timeStep * Time.deltaTime;
            if (progress > 1)
            {
                progress = 1;
            }
            if (progress < 0)
            {
                progress = 0;
            }
        }
    }
}
