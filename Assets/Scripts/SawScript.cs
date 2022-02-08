using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SawScript : MonoBehaviour
{
    private Vector3 posOne, posTwo;

    void Start()
    {
        posOne = new Vector3(0, 0, 0);
        posTwo = new Vector3(-0.130f, 0, 0);
    }

    void Update()
    {
        if(transform.localPosition == posOne)
        {
            transform.DOLocalMoveX(posTwo.x, 2f);
        }
        else if(transform.localPosition == posTwo)
        {
            transform.DOLocalMoveX(posOne.x, 2f);
        }
        transform.Rotate(new Vector3(0, 0, 2));
    }
    public void turnBack()
    {
        transform.DOMoveX(posOne.x, 2f);
    }
}
