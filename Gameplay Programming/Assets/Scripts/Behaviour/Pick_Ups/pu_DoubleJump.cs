using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pu_DoubleJump : pu_Spawnable
{
    private void Awake()
    {
        powerUpEffect = PowerUpEffects.DOUBLE_JUMP;
    }
}
