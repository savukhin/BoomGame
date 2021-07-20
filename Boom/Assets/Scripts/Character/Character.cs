using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float maxHealthPoints = 100;
    public float healthPoints = 100;
    public float jumpForce = 5f;
    public float moveSpeed;
    protected Rigidbody rb;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool IsGrounded(float extraDimension = 0.1f) {
        //return Physics.BoxCast(model.GetComponent<Collider>().bounds.center, model.GetComponent<Collider>().bounds.size / 2 - new Vector3(extraDimension, extraDimension, extraDimension), new Vector3(1f, -1f, 1f), model.transform.rotation, 2 * extraDimension, platformLayerMask.value);
        return Physics.BoxCast(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size / 2 - new Vector3(extraDimension, extraDimension, extraDimension), new Vector3(1f, -1f, 1f), transform.rotation, 2 * extraDimension);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Jump() {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }


    public virtual void TakeDamage(float damage) {
        healthPoints -= damage;
        if (healthPoints <= 0)
            Destroy(gameObject);
    }
}
