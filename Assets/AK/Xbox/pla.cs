using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pla : MonoBehaviour
{
    public float rotationSpeed = -30f; // Degrees per second

    void Update()
    {
        // Rotate around the Y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
