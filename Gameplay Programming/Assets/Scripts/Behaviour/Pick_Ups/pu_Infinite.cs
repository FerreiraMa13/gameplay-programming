using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pu_Infinite : Pick_Up
{
    private void Awake()
    {
        type = PowerUpType.ETERNAL;
    }
}
