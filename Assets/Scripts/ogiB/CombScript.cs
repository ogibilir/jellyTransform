using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CombScript : MonoBehaviour
{
   [SerializeField] private float _CombSpeed;
   [SerializeField] private float _sawLeft;
   [SerializeField] private float _arriveTime;
   [SerializeField] private float _sawRight;

    void Start()
    {
        StartCoroutine(DestroyObject());
    }
    void Update()
    {
        if (transform.localPosition.y == _sawRight)
        {
            transform.DOLocalMoveY(_sawLeft, _arriveTime);
        }
        else if (transform.localPosition.y == _sawLeft)
        {
            transform.DOLocalMoveY(_sawRight, _arriveTime);
        }
    }
    public IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
