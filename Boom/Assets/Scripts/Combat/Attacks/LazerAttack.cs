using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAttack : BaseAttack
{
    public GameObject lazerPoint;
    public LazerRay rayPrefab;
    public float lazerDuration = 0.5f;
    private LazerRay lazerInstance;

    private void Activate() {
        lazerInstance = Instantiate(rayPrefab, lazerPoint.transform);
        lazerInstance.hitTags.Add("Player");
        lazerInstance.ignoreTags.Add("Player");
    }

    private void Deactivate() {
        Destroy(lazerInstance.gameObject);
    }

    protected override IEnumerator AttackProcess()
    {
        yield return base.AttackProcess();
        Activate();
        Invoke("Deactivate", lazerDuration);
    }
}
