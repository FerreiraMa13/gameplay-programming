using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineFollower : MonoBehaviour
{
    public enum SplineFollowerMode
    { 
        NONE = 0,
        AUTO = 1,
        INDEPENDENT = 2
    }
    public BezierSpline spline;
    public SplineFollowerMode movementMode = SplineFollowerMode.NONE;
    bool valid = false;
    /*[System.NonSerialized]*/
    public bool active = true;
    [System.NonSerialized]
    public Vector3 movementVector = Vector3.zero;
    public float timeTaken = 10.0f;
    float timeStep;
    float progress = 0.0f;
    float moveDirection = 1.0f;


    private void OnValidate()
    {
        timeStep = 1 / timeTaken;
        valid = (spline != null);
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
        progress += movementVector.magnitude;
        if(progress > 1)
        {
            progress = 1;
            active = false;
        }
        if(progress < 0)
        {
            progress = 0;
            active = false;
        }
        transform.rotation = Quaternion.Euler(spline.GetDirection(progress));
    }
}
