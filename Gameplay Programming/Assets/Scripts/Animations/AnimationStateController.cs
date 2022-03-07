using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    enum attackEvents
    {
        NULL = 0,
        HIT = 1,
        END = 2
    }
    Animator animator;
    PlayerMovController player_mov;

    int movementHash;
    int jumpHash;
    int landHash;
    int attackHash;
    int fallHash;
    float movement;

    private void Start()
    {
        player_mov = GetComponentInParent<PlayerMovController>();
        animator = GetComponent<Animator>();
        movementHash = Animator.StringToHash("movement");
        jumpHash = Animator.StringToHash("jump");
        landHash = Animator.StringToHash("land");
        attackHash = Animator.StringToHash("attack");
        fallHash = Animator.StringToHash("fall");
    }

    public void updateMovement(float new_movement)
    {
        if (new_movement != movement)
        {
            movement = new_movement;
            animator.SetFloat(movementHash, new_movement);
        }
    }
    public void triggerJump()
    {
        animator.SetTrigger(jumpHash);
    }
    public void triggerLand()
    {
        animator.SetTrigger(landHash);
    }
    public void triggerAttack()
    {
        animator.SetTrigger(attackHash);
    }
    public void triggerFall()
    {
        animator.SetTrigger(fallHash);
    }
    public void listenAttack(int status)
    {
        switch (status)
        {
            case (int)attackEvents.HIT:
                player_mov.detectHit();
                break;
            case (int)attackEvents.END:
                player_mov.endAttack();
                break;
            case 0:
                break;
        }
    }
    public void listenLand(int status)
    {
        switch (status)
        {
            case 1:
                player_mov.detectHit();
                break;
            case 2:
                player_mov.endAttack();
                break;
            case 0:
                break;
        }
    }
    public void speedUP(float factor)
    {
        animator.speed = factor;
    }
}
