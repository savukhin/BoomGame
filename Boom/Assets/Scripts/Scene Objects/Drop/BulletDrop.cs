using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDrop : Drop
{
    //public WeaponTypesEnum weaponType = WeaponTypesEnum.Pistol;
    public string weaponName = "Marakov's Pistol";
    public int bulletCount = 10;

    public override void Activate(FirstPersonController player)
    {
        //throw new System.NotImplementedException();
        player.AddBullets(weaponName, bulletCount);
    }
}
