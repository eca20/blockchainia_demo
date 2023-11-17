using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByPlane : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Elimination Plane Reached");
      //  Destroy(other.gameObject);
    }
}

