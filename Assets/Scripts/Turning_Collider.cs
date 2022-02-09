using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning_Collider : MonoBehaviour
{
    public MeshRenderer Mesh;


    // Start is called before the first frame update
    void Start()
    {
        Mesh = GetComponent<MeshRenderer>();
        Mesh.enabled = false;
    }

    void On(Collision collision)
    {
        Debug.Log("Turning Collider Box Inside Trigger");
    }
}
