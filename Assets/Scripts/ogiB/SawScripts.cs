using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SawScripts : MonoBehaviour
{
    public float _sawRight;
    public float _sawLeft;
    public float _arriveTime;
    void Start()
    {
        
    }
    void LateUpdate()
    {
        if(transform.localPosition.x == _sawRight)
        {
            transform.DOLocalMoveX(_sawLeft, _arriveTime);
        }
        else if(transform.localPosition.x == _sawLeft)
        {
            transform.DOLocalMoveX(_sawRight, _arriveTime);
        }
        transform.Rotate(0,0,10);
    }
}
