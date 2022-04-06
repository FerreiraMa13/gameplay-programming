using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimatorController : MonoBehaviour
{
    Animator animator;
    NPC_Controller controller;

    int jumpHash;

    private void Start()
    {
        controller = GetComponentInParent<NPC_Controller>();
        animator = GetComponent<Animator>();
        jumpHash = Animator.StringToHash("jump");
    }
    public void triggerJump()
    {
        animator.SetTrigger(jumpHash);
    }
    public void listenLand()
    {
        controller.landed = true;
    }
}
