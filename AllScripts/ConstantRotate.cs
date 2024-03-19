using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotate : MonoBehaviour
{
    public float vitezaRotatie = 10f; // Viteza de rotație în grade pe secundă

    void Update()
    {
        // Calculează un unghi de rotație proporțional cu timpul trecut între cadre
        float unghiRotație = vitezaRotatie * Time.deltaTime;

        // Rotește obiectul în jurul axei sale (de exemplu, axa verticală, asemeni Pământului)
        transform.Rotate(Vector3.up, unghiRotație);
    }
}
