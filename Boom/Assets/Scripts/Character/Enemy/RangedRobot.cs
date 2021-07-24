using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedRobot : BaseEnemy
{
    public BaseBullet bulletPrefab;
    public GameObject strikePoint;

    protected override void Attack()
    {
        base.Attack();
        //Vector3 direction = (target - transform.position).normalized;
        var bullet = Instantiate(bulletPrefab, strikePoint.transform.position, strikePoint.transform.rotation);
        bullet.hitTags.Add("Player");
        bullet.ignoreTags.Add("Enemy");
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
