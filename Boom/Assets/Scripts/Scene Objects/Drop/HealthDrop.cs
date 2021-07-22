using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : Drop
{
    public float healthPoints = 10;

    public override void Activate(FirstPersonController player)
    {
        //throw new System.NotImplementedException();
        player.Heal(healthPoints);
    }
}
