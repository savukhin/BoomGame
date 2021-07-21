using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseBullet : MonoBehaviour
{
    public float damage = 10;
    public VFX hitVFX;
    public List<string> hitTags = new List<string>();
    public List<string> ingoreTags = new List<string>() {"Bullet", "Volume"};

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual bool Hit(Collider collider) {
        if (hitTags.Contains(collider.tag)) {
            collider.GetComponent<Character>().TakeDamage(damage);
        }
        return !ingoreTags.Contains(collider.tag);
    }
}
