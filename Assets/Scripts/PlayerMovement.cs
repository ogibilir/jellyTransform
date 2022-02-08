using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _zSpeed;
    [SerializeField] private float _xSpeed;

    public static float _stopTime;


    void Start()
    {
        _stopTime = -1;
    }
    void LateUpdate()
    {
        if(_stopTime < 1)
        {
            float horizontal = Input.GetAxis("Horizontal") * _xSpeed * Time.deltaTime;
            transform.Translate(horizontal, 0, _zSpeed * Time.deltaTime);
        }
        else if(_stopTime > 0)
        {
            _stopTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }
}
