using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBoxAttack : BaseAttack
{
    public Bounds attackBounds;
    public float damage = 10;
    public float knockBack = 4;

    protected override IEnumerator AttackProcess()
    {
        yield return base.AttackProcess();
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward * Vector3.Distance(attackBounds.center, Vector3.zero), attackBounds.size, transform.rotation);
        foreach (var collider in colliders) {
            if (collider.tag == "Player")
                collider.GetComponent<FirstPersonController>().TakeDamage(damage, knockBack);
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position + attackBounds.center, attackBounds.extents);
    }
}
