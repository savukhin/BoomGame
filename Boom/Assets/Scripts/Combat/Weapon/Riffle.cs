using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riffle : BaseWeapon
{
    public BaseBullet bulletPrefab;
    private float startRecoil;

    protected override void Start()
    {
        base.Start();
        startRecoil = recoilImpulse;
    }

    public override bool Fire(bool burstShooting=false)
    {
        if (base.Fire(burstShooting) == false)
            return false;
        
        var bullet = Instantiate(bulletPrefab, firePoint.transform.position, GetStrifedRotation(strifeRadius + Mathf.Min(burstCounter, 10)));
        bullet.hitTags.Add("Enemy");
        StartCoroutine(DoRecoil(Mathf.Min(burstCounter / 5, 2)));

        return true;
    }
}
