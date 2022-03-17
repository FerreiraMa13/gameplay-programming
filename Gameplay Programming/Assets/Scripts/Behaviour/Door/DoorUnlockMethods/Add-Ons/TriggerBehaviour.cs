using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBehaviour : MonoBehaviour
{

    [System.NonSerialized]
    public bool trigger = false;
    public bool independent = false;
    public bool oneTime = false;
    /// <summary>
    /// Makes it so that leaving the trigger will activate it again.
    /// </summary>
    public bool pulseTrigger = false;
    public List<TriggerDoor> triggerDoors;
    /*public TriggerDoor triggerDoor = null;*/
    bool triggered = false;
    private void Awake()
    {
        if (!independent)
        {
            triggerDoors.Clear();
            triggerDoors.Add( GetComponentInParent<TriggerDoor>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !triggered)
        {
            trigger = true;
            if(oneTime)
            {
                triggered = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !triggered)
        {
            if(pulseTrigger)
            {
                trigger = true;
            }
            else
            {
                trigger = false;
            }
        }
    }
}
