using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBoss : BaseEnemy
{
    [System.Serializable]
    public class AttackDescription {
        public string triggerName;
        public float attackRange;
        public BaseAttack attack;
    }

    public AttackDescription[] attacks;
    private AttackDescription nextAttack;
    private float timeBetweenAttacks = 2f;

    protected override void Start()
    {
        base.Start();
        timeBetweenAttacks = attackCooldown;
        SetNextAttack();
    }

    private void SetNextAttack() {
        if (nextAttack != null)
            attackCooldown = nextAttack.attack.duration + timeBetweenAttacks;
        //nextAttack = attacks[Random.Range(0, attacks.Length)];
        nextAttack = attacks[0];
        attackRange = nextAttack.attackRange;
    }

    protected override void Attack() {
        if (animator)
            animator.SetTrigger(nextAttack.triggerName);
        nextAttack.attack.Attack();
        SetNextAttack();
    }


    protected sealed override IEnumerator PersueTheTarget() {
        for (;;) {
            animator.SetBool("Move", true);
            target = activationZone.target.transform.position;
            target = new Vector3(target.x, 0, target.z);
            if (MoveToTarget()) {
                animator.SetBool("Move", false);
                Attack();
                yield return new WaitForSeconds(attackCooldown);
            } else {
            }
            yield return null;
        }
    }
}
