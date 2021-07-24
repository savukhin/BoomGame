using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent Finish;
    public float delay = 0.1f;
    public float duration = 1f;

    public virtual void Attack() {
        StartCoroutine("AttackProcess");
        this.Invoke(() => Finish.Invoke(), duration);
    }

    protected virtual IEnumerator AttackProcess() {
        yield return new WaitForSeconds(delay);
    }
}
