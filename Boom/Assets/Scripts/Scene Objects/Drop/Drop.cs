using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public enum TypesEnum {
        weapon = 1,
    }
    public GameObject item;
    public TypesEnum type;
}
