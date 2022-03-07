using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningMethod : MonoBehaviour
{
    public enum DoorUnlockingMethod
    {
        NONE = 0,
        BUTTON = 1,
        KEY = 2,
        TRIGGER = 3
    }

    [System.NonSerialized]
    public DoorUnlockingMethod method = DoorUnlockingMethod.NONE;
    DoorAnimator animator;
    bool locked = true;

    public void Start()
    {
        if (method != DoorUnlockingMethod.NONE)
        {
            animator = GetComponent<DoorAnimator>();
        }
    }
    virtual public bool UnlockRequirements()
    {
        return false;
    }

    public bool Unlock()
    {
        if(locked)
        {
            locked = false;
            animator.Open();
            return true;
        }
        return false;
        //Animator stuff here
        
    }

    void Update()
    {
        if(UnlockRequirements())
        {
            Unlock();
        }
    }
}
