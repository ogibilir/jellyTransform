using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterScript : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(_rotateSpeed, 0, 0);
    }
}
