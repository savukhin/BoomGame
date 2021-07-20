using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : BaseWeapon
{
    public BaseBullet bulletPrefab;
    
    public override void Fire()
    {
        base.Fire();
        var bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        bullet.hitTags.Add("Enemy");
    }
}
