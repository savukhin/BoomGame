using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationZone : MonoBehaviour {
    public UnityEngine.Events.UnityEvent activate;
    public UnityEngine.Events.UnityEvent deactivate;
    public GameObject target;

    void OnTriggerEnter(Collider collider) {
        if (!collider.GetComponent<FirstPersonController>())
            return;
        target = collider.gameObject;
        activate.Invoke();
    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject == target) {
            target = null;
            deactivate.Invoke();
        }
    }
}
