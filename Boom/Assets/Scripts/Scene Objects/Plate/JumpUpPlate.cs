using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUpPlate : Plate
{
    public float power = 100;
    public override void Activate(GameObject subject)
    {
        base.Activate(subject);
        //subject.GetComponent<Rigidbody>().velocity += (Vector3.up + transform.forward) * power;
        subject.GetComponent<Rigidbody>().AddForce((Vector3.up + transform.forward) * power, ForceMode.Impulse);
    }
}
