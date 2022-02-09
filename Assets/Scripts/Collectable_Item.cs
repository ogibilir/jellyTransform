using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_Item : MonoBehaviour
{
    [HideInInspector]public MeshRenderer _MeshRenderer;


    void Start()
    {
        _MeshRenderer = gameObject.GetComponent<MeshRenderer>();
        _MeshRenderer.enabled = false;

    }
}
