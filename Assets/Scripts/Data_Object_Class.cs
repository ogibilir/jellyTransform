using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_object", menuName = "Examples/")]
public class Data_Object_Class : ScriptableObject
{
    [SerializeField] public int max_reachable_level = 0;
}
