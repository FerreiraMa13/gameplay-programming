using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimatorController : MonoBehaviour
{
    Animator animator;
    Slime_Controller controller;

    int jumpHash;

    private void Start()
    {
        controller = GetComponentInParent<Slime_Controller>();
        animator = GetComponent<Animator>();
        jumpHash = Animator.StringToHash("jump");
    }
    public void triggerJump()
    {
        animator.SetTrigger(jumpHash);
    }
    public void listenLand()
    {
        controller.ResetJump();
    }
    
}
