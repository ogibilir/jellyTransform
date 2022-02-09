using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _zSpeed;
    [SerializeField] private float _xSpeed;
    public static bool isFinish;

    public static float _stopTime;

    void Start()
    {
        isFinish = false;
        _stopTime = -1;
    }
    void LateUpdate()
    {
        if(_stopTime < 1 && !isFinish)
        {
            float horizontal = Input.GetAxis("Horizontal") * _xSpeed * Time.deltaTime;
            transform.Translate(horizontal, 0, _zSpeed * Time.deltaTime);
        }
        if (isFinish)
        {
            transform.Translate(0, 0, _zSpeed*Time.deltaTime);
        }
        else if(_stopTime > 0)
        {
            _stopTime -= Time.deltaTime;
        }
    }
}
