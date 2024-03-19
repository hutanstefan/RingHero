using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target; // părintele la care obiectul trebuie să fie atașat
    public float smoothSpeed = 0.125f; // viteza de interpolare

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position; // poziția dorită este poziția părintelui
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // interpolare între poziția curentă și poziția dorită
        transform.position = smoothedPosition; // actualizare poziție

        // Aceasta este doar pentru rotație, comentează/elimină această secțiune dacă nu ai nevoie de rotație smooth.
        Quaternion desiredRotation = target.rotation; // rotația dorită este rotația părintelui
        Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed); // interpolare între rotația curentă și rotația dorită
        transform.rotation = smoothedRotation; // actualizare rotație
    }
}
