using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterY : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(0, _rotateSpeed, 0);
    }
}
