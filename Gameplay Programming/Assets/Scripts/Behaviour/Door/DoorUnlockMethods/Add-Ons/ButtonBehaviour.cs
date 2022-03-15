using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : HitObject
{
    [System.NonSerialized]
    public bool pressed = false;
    public bool independent = false;
    public ButtonDoor buttonDoor = null;
    public Cinematic_Behaviour cinematic;

    private void Awake()
    {
        if(!independent)
        {
            buttonDoor = GetComponentInParent<ButtonDoor>();
        }
    }

    public void gotPressed()
    {
        pressed = !pressed;
        player.interact = false;
        if(cinematic != null)
        {
            cinematic.SignalSTARTING();
        }
    }

    public override void OnHitBehaviour()
    {
        gotPressed();
    }
}
