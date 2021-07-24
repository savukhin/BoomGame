using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBoxAttack : BaseAttack
{
    public Bounds attackBounds;
    public float damage = 10;

    protected override IEnumerator AttackProcess()
    {
        yield return base.AttackProcess();
        print(attackBounds.center);
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.forward * Vector3.Distance(attackBounds.center, Vector3.zero), attackBounds.size, transform.rotation);
        foreach (var collider in colliders) {
            if (collider.tag == "Player")
                collider.GetComponent<FirstPersonController>().TakeDamage(10);
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position + attackBounds.center, attackBounds.extents);
    }
}
