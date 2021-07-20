using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public VFX fireVFX;
    public GameObject firePoint;

    public virtual void Fire() {
        Instantiate(fireVFX, firePoint.transform.position, firePoint.transform.rotation);
    }
}
