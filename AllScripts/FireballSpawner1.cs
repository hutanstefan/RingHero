using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : MonoBehaviour
{
    public GameObject fireballPrefab; 
    public float fireballSpeed = 10f; 
    public float timeBetweenFireballs = 1f; 

    void Start()
    {
        StartCoroutine(SpawnFireballs());
    }

    IEnumerator SpawnFireballs()
    { 
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            Vector3 direction = transform.forward;
            Rigidbody rb = fireball.GetComponent<Rigidbody>();  
            rb.velocity = direction * fireballSpeed;        

            yield return new WaitForSeconds(timeBetweenFireballs);

            StartCoroutine(SpawnFireballs());
     
    }
}
