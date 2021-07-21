using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseWeapon
{
    public BaseBullet bulletPrefab;
    public int bulletCount = 5;
    public float strifeRadius = 10f;
    
    public override bool Fire()
    {
        if (base.Fire() == false)
            return false;

        for (int i = 0; i < bulletCount; i++) {
            var rotation = firePoint.transform.rotation.eulerAngles;
            rotation += new Vector3(Random.Range(-strifeRadius, strifeRadius),
                                    Random.Range(-strifeRadius, strifeRadius),
                                    Random.Range(-strifeRadius, strifeRadius));
            var bullet = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.Euler(rotation));
            bullet.hitTags.Add("Enemy");
        }
        StartCoroutine(DoRecoil());
        
        return true;
    }
}
