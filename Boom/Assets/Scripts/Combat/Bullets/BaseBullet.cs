using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseBullet : MonoBehaviour
{
    public float damage = 10;
    public float startSpeed = 20;
    public List<string> hitTags = new List<string>();
    public List<string> ingoreTags = new List<string>() {"Bullet", "Volume"};
    private Rigidbody rb;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * startSpeed, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider) {
        if (hitTags.Contains(collider.tag))
            collider.GetComponent<Character>().TakeDamage(damage);
        if (!ingoreTags.Contains(collider.tag))
            Destroy(gameObject);
    }
}
