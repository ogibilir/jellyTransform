using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;
    public Vector3 distance;
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + distance, Time.deltaTime);
    }
}
