using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float selfDestructTime = 3f;

    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(selfDestructTime);
        Destroy(gameObject);
    }
}
