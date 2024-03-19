using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if (!other.isTrigger && other.CompareTag("Enemy"))
        {
            other.GetComponent<Goblin>().TakeDMG(25);
        }

        if (!other.isTrigger && other.CompareTag("Archer"))
        {
            other.GetComponent<Archer>().TakeDMG(25);
        }
        if (!other.isTrigger && other.CompareTag("Boss"))
        {
            other.GetComponent<BossStatus>().TakeDMG(25);
        }

    }
}
