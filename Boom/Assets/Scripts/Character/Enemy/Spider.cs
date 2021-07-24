using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : BaseBoss
{
    private AttackDescription chewedAttack;
    private AttackDescription legAttack;
    private AttackDescription lazerAttack;
    private List<AttackDescription> unorderedAttacks;

    protected override void Start()
    {
        foreach (var attack in attacks) {
            switch (attack.triggerName) {
                case "Chewed Attack":
                    chewedAttack = attack;
                    break;
                case "Leg Attack":
                    legAttack = attack;
                    break;
                case "Lazer Attack":
                    lazerAttack = attack;
                    break;
                default:
                    unorderedAttacks.Add(attack);
                    break;
            }
        }
        base.Start();
    }

    protected override void SetNextAttack()
    {
        if (Vector3.Distance(transform.position, target) <= chewedAttack.attackRange / 2)
            base.SetNextAttack(chewedAttack);
        else
            base.SetNextAttack();

        if (nextAttack == chewedAttack) {
            moveSpeed = defaultMoveSpeed * 3f;
        } else {
            moveSpeed = defaultMoveSpeed;
        }
    }
}
