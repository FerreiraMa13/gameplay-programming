using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    [System.NonSerialized]
    public bool pressed = false;
    public bool independent = false;
    public ButtonDoor buttonDoor = null;


    PlayerMovController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovController>();

        if(!independent)
        {
            GetComponentInParent<ButtonDoor>();
        }
    }

    public void gotPressed()
    {
        if(!pressed)
        {
            pressed = true;
            Debug.Log("Pressed");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!pressed && player.attacking)
            {
                gotPressed();
            }
        }
    }
}
