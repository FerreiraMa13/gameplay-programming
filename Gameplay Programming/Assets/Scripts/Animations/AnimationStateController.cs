using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    int movementHash;
    float movement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        movementHash = Animator.StringToHash("movement");
    }

    public void updateMovement( float new_movement)
    {
        if(new_movement != movement)
        {
            movement = new_movement;
            animator.SetFloat(movementHash, new_movement);
        }
    }
}
