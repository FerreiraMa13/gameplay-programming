using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : HitObject
{
    [System.NonSerialized]
    public bool pressed = false;
    public bool independent = false;
    public ButtonDoor buttonDoor = null;

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
    }

    public override void OnHitBehaviour()
    {
        gotPressed();
    }
}
