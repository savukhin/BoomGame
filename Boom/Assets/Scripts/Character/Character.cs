using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float maxHealthPoints = 100;
    public float healthPoints = 100;
    public float jumpForce = 5f;
    public float maxSpeed = 6f;
    public float moveSpeed;
    protected Rigidbody rb;
    private Vector3 additionalVelocity = Vector3.zero; 

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        //MoveInDirection(transform.forward);
    }

    public bool IsGrounded(float extraDimension = 0.7f) {
        return Physics.BoxCast(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size / 2 - new Vector3(extraDimension, extraDimension, extraDimension), Vector3.down, transform.rotation, 2 * extraDimension);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate() {
        // if (rb.velocity.magnitude > maxSpeed)
        //     rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    private bool isMovingForward = false;

    protected void MoveInDirection(Vector3 direction) {
        isMovingForward = (direction == transform.forward);
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    protected void Jump() {
        var direction = Vector3.up;
        if (isMovingForward)
            direction += transform.forward;
        rb.AddForce(direction * jumpForce, ForceMode.Impulse);
    }


    public virtual void TakeDamage(float damage) {
        healthPoints -= damage;
        if (healthPoints <= 0)
            Destroy(gameObject);
    }

    public virtual void Heal(float heal) {
        healthPoints = Mathf.Min(maxHealthPoints, healthPoints + heal);
    }

    protected virtual void OnTriggerEnter(Collider collider) {}

    protected virtual void OnCollisionEnter(Collision collision) {}
}
