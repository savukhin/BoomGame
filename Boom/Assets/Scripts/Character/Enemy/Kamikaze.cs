using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

public class Kamikaze : BaseEnemy
{
    public VFX boomVFX;
    public float boomRange = 3f;
    public float boomDamage = 50f;

    protected override void Attack()
    {
        base.Attack();
        if (boomVFX)
            Instantiate(boomVFX, transform.position, transform.rotation);
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, boomRange);
        foreach (var collider in hitColliders) {
            var player = collider.GetComponent<FirstPersonController>();
            if (player)
                player.TakeDamage(boomDamage);
        }
        Destroy(gameObject);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, boomRange);
    }
}
