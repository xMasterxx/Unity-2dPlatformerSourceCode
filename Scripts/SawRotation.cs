using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawRotation : MonoBehaviour
{
    public float sawRotationSpeed = 5f;
   
    void Update()
    {
        transform.Rotate(Vector3.forward * sawRotationSpeed);
    }
}
