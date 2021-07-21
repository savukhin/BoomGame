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

        var bullet = Instantiate(bulletPrefab, realFirePoint.transform.position, GetStrifedRotation(strifeRadius));
        bullet.hitTags.Add("Enemy");
        bullet.ingoreTags.Add("Player");
        StartCoroutine(DoRecoil());

        return true;
    }
}
