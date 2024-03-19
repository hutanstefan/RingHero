using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotirea obiectului pe toate axele
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // rotație pe axa Y
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime); // rotație pe axa X
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime); // rotație pe axa Z
    }
}
