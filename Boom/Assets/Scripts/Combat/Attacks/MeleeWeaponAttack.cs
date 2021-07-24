using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponAttack : BaseAttack
{
    public MeleeWeapon weapon;
    public float hitDuration = 0.5f;

    private void Activate() {
        weapon.enabled = true;
    }

    private void Deactivate() {
        weapon.enabled = false;
    }

    protected override IEnumerator AttackProcess()
    {
        yield return base.AttackProcess();
        Activate();
        Invoke("Deactivate", hitDuration);
    }
}
