using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;
    public Vector3 distance;
    void LateUpdate()
    {
        if(target != null)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,target.transform.position.y,target.transform.position.z) + distance, Time.deltaTime);
        }
    }
}
