using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Character
{
    public Vector3 target;
    public float attackRange;
    public float attackRate = 1;
    public ActivationZone activationZone;
    protected bool attacking = false;
    public float attackCooldown = 1f;
    [System.Serializable]
    public class DroppingItems {
        public float chance;
        public Drop item;
    }
    public List<DroppingItems> possibleDrop;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        activationZone.activate.AddListener(Activate);
        activationZone.deactivate.AddListener(Deactivate);
        //StartCoroutine("WanderAround");
    }

    protected bool RotateToTarget() {
        var q = Quaternion.LookRotation(target - new Vector3(transform.position.x, 0, transform.position.z));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 90 * Time.deltaTime);
        return true;
    }

    // Returns true if target in attack range
    protected bool MoveToTarget() {
        Vector3 direction = (target - transform.position).normalized;
        Vector3 rotateDirection = target - new Vector3(transform.position.x, 0, transform.position.z);
        var q = Quaternion.LookRotation(rotateDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360 * Time.deltaTime);
        
        if ((rotateDirection.normalized - transform.forward.normalized).magnitude > 0.1f)
            return false;

        if ((target - transform.position).magnitude > attackRange && !attacking) {
            MoveInDirection(direction);
            return false;
        } else {
            return true;
        }
    }

    protected virtual void Attack() {

    } 

     IEnumerator PersueTheTarget() {
        for (;;) {
            target = activationZone.target.transform.position;
            target = new Vector3(target.x, 0, target.z);
            if (MoveToTarget()) {
                Attack();
                yield return new WaitForSeconds(attackCooldown);
            } else {
            }
            yield return null;
        }
    }

    IEnumerator WanderAround() {
        for(;;) {
            yield return new WaitForSeconds(5);
            Vector3 direction = new Vector3(Random.Range(0, 10f), 0, Random.Range(0, 10)).normalized;
            target = transform.position + direction * 10;
            target = new Vector3(target.x, 0, target.z);
            for (;;) {
                if (MoveToTarget())
                    break;
                yield return null;
            }
        }
    }

    public virtual void Activate() {
        target = activationZone.target.transform.position;
        StopCoroutine("WanderAround");
        StartCoroutine("PersueTheTarget");
    }

    public virtual void Deactivate() {
        //target = new Vector3();
        StartCoroutine("WanderAround");
        StopCoroutine("PersueTheTarget");
    }

    protected virtual void OnDestroy() {
        foreach (var drop in possibleDrop) {
            if (drop.chance < Random.Range(0f, 1f))
                continue;
            
            var item = Instantiate(drop.item, transform.position, transform.rotation);
            var itemRB = item.GetComponent<Rigidbody>();
            if (!itemRB)
                continue;
            
            var direction = Random.rotation.eulerAngles;
            direction.y = Mathf.Clamp(direction.y, 330, 360);
            direction = direction.normalized;

            var force = direction * Random.Range(0.6f, 0.7f);
            itemRB.AddForce(force, ForceMode.Impulse);
        }
    }
}
