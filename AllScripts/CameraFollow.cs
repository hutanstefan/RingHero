using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Transformul pe care camera îl va urmări
    public float smoothTime = 0.1f; // Timpul de interpolare smooth
    private Vector3 velocity = Vector3.zero; // Viteza curentă a mișcării, inițializată cu zero
    public float offsetX;
    public float offsetY;
    public float offsetZ;

    void LateUpdate()
    {
        if (target != null) // Verificăm dacă avem un target
        {
            // Calculăm poziția țintei offset cu o anumită distanță și înălțime
            Vector3 targetPosition = target.position + new Vector3(offsetX, offsetY, offsetZ);

            // Interpolăm poziția curentă a camerei către poziția țintei folosind SmoothDamp
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);


        }
    }
}
