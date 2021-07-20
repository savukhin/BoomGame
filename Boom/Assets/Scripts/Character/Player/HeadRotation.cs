using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotation : MonoBehaviour
{
    public float sensitivity = 5;
    private float minRotation = -Mathf.Sqrt(2) / 2;
    private float maxRotation = Mathf.Sqrt(2) / 2;
    private float extremeW = Mathf.Sqrt(2) / 2;

    void Update() 
    {
        transform.Rotate(-Input.GetAxis("Mouse Y") * sensitivity, 0, 0f);
        if (transform.localRotation.x > maxRotation)
            transform.localRotation = new Quaternion(maxRotation, 0, 0, extremeW);
        if (transform.localRotation.x < minRotation)
            transform.localRotation = new Quaternion(minRotation, 0, 0, extremeW);
    }
}
