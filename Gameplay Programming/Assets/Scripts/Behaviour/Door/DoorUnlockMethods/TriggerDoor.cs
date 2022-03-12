using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : DoorOpeningMethod
{
    public bool independent = false;
    public TriggerBehaviour trigger= null;

    private void Awake()
    {
        method = DoorUnlockingMethod.TRIGGER;
        if (!independent)
        {
            trigger = GetComponentInChildren<TriggerBehaviour>();
        }
    }

    public override bool UnlockRequirements()
    {
        bool hasTriggered = trigger.trigger;
        if(hasTriggered && !(animator.status == DoorAnimator.DoorStatus.OPENING || animator.status == DoorAnimator.DoorStatus.CLOSING))
        {
            trigger.trigger = false;
        }
        return hasTriggered;
    }
}
