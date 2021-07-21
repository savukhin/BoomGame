using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalBullet : BaseBullet
{
    public float startSpeed = 20;
    private Rigidbody rb;

    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * startSpeed, ForceMode.VelocityChange);
    }



    void OnTriggerEnter(Collider collider) {
        if (Hit(collider)) {
            if (hitVFX)
                Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void OnDestroy() {
        
    }
}
