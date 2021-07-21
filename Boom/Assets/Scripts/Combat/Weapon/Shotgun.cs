using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseWeapon
{
    public BaseBullet bulletPrefab;
    public int bulletCount = 5;
    
    public override bool Fire(bool burstShooting=false)
    {
        if (base.Fire(burstShooting) == false)
            return false;

        for (int i = 0; i < bulletCount; i++) {
            var bullet = Instantiate(bulletPrefab, firePoint.transform.position, GetStrifedRotation(strifeRadius));
            bullet.hitTags.Add("Enemy");
        }
        StartCoroutine(DoRecoil());
        
        return true;
    }
}
