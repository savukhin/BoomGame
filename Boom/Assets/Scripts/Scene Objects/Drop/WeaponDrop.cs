using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : Drop
{
    public BaseWeapon weapon;

    public override void Activate(FirstPersonController player)
    {
        //throw new System.NotImplementedException();
        player.AddWeapon(weapon);
    }
}
