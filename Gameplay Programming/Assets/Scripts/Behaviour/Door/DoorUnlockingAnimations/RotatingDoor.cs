using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDoor : DoorAnimator
{

    public Vector3 pivotPoint = Vector3.zero;
    public Enums.Axis rotationAxis = Enums.Axis.NONE;
    public float rotationSpeed = 1.0f;
    private void Awake()
    {
        type = DoorOpeningAnimation.ROTATING;
    }
}
