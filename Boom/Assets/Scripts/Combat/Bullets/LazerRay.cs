using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerRay : BaseBullet
{
    public float maxDistance = 100;

    private void Resize(float size) {
        transform.position = transform.parent.position + transform.forward.normalized * size;
        transform.localScale = new Vector3(1, 1, size);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        RaycastHit hit;
        Physics.Raycast(transform.parent.position, transform.forward, out hit, maxDistance);
        if (hit.collider)
            Resize(Vector3.Distance(hit.point, transform.position));
        else
            Resize(maxDistance);
    }

    void OnTriggerEnter(Collider collider) {
        if (Hit(collider)) {
            if (hitVFX)
                Instantiate(hitVFX, transform.position, transform.rotation);
        }
    }
}
