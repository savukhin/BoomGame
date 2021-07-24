using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float desiredRot;
    public float rotSpeed = 100;
    
    // Update is called once per frame
    void Update()
    {
        desiredRot = transform.localRotation.eulerAngles.y + rotSpeed * Time.deltaTime * 10;

        var desiredRotQ = Quaternion.Euler(transform.localEulerAngles.x, desiredRot, transform.localEulerAngles.z);
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, desiredRotQ, 10*Time.deltaTime);
    }
}
