using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public List<string> triggerTags = new List<string>() {"Player"};
    
    public virtual void Activate(GameObject subject) {}

    void OnTriggerEnter(Collider collider) {
        if (triggerTags.Contains(collider.tag))
            Activate(collider.gameObject);
    }
}
