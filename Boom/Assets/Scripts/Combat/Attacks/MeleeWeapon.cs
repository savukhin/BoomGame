using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float damage = 10;
    
    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Player") {
            collider.GetComponent<FirstPersonController>().TakeDamage(damage);
        }
    }
}
