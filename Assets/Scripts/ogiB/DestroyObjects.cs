using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Renderer renderer = other.GetComponent<Renderer>();
        if(renderer != null)
        {
            renderer.enabled = false;
        }
    }
}
