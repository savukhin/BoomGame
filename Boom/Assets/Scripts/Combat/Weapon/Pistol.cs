using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : BaseWeapon
{
    public BaseBullet bulletPrefab;
    
    public override bool Fire(bool burstShooting=false)
    {
        if (base.Fire(burstShooting) == false)
            return false;

        var bullet = Instantiate(bulletPrefab, firePoint.transform.position, GetStrifedRotation(strifeRadius));
        bullet.hitTags.Add("Enemy");
        StartCoroutine(DoRecoil());

        return true;
    }
}
