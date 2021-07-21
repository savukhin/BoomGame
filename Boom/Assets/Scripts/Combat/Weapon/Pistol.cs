using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : BaseWeapon
{
    public BaseBullet bulletPrefab;
    
    public override bool Fire()
    {
        
        if (base.Fire() == false)
            return false;

        var bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        bullet.hitTags.Add("Enemy");
        StartCoroutine(DoRecoil());

        return true;
    }
}
