using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBullet : BaseBullet
{
    public float maxDistance = 100f;
    protected override void Start()
    {
        base.Start();
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, maxDistance);
        foreach (var hit in hits) { 
            if (!Hit(hit.collider))
                continue;
            
            if (hitVFX)
                Instantiate(hitVFX, hit.point, transform.rotation);
        }
        Destroy(gameObject);
    }
}
