using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Archer : Mob
{

    public Archer(int _hp, int _attack, float _speed) : base(_hp, _attack) { }
    private float distanceToPlayer;

    public GameObject arrowPrefab;
    public float spawnDistance = 1f;
    public float arrowSpeed = 10f;
    private bool spawningAllowed = true;
    public float spawnHeight = 5f;

    private bool knockback;

    public float waitTime;

    private float windTimer;
    public float windForce = 10f;
    public float windUpForce = 2f;
    private Rigidbody rb;
    public float windDuration = 0.2f;
    public float knockbackForce;
    public float knockbackUpForce;

    private bool hasDied = false;

    void Start()
    {
        spawningAllowed = true;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AbilityWater"))
        {
            TakeDMG(20);
            //ModifySpeed(2.0f, 3.0f);
        }
        if (collision.gameObject.CompareTag("AbilityFire"))
        {
            TakeDMG(20);
            //rend.material = fireMaterial;
            //StartCoroutine(RestoreMaterial(3.0f));

        }
    }
    void FixedUpdate()
    {

        if (windTimer > 0)
        {
            // Aplică knockback
            rb.AddForce(-transform.forward * windForce, ForceMode.Impulse);
            rb.AddForce(-transform.up * windUpForce, ForceMode.Impulse);
            windTimer -= Time.deltaTime;
        }

        if (knockback)
        {
            Vector3 knockbackDirection = -transform.forward; // Direcția în care este împins obiectul
            Vector3 knockbackForceVector = knockbackDirection * knockbackForce;

            // Adăugarea unei componente verticale la forța de knockback
            knockbackForceVector.y = knockbackUpForce;

            rb.AddForce(knockbackForceVector, ForceMode.Impulse);
            knockback = !knockback;
        }

        Vector3 direction = player.transform.position - transform.position;
        distanceToPlayer = direction.magnitude;
        if (inRange)
        {

            transform.rotation = Quaternion.LookRotation(direction);
            if (spawningAllowed)
            {
                StartCoroutine(SpawnArrowCoroutine());
                spawningAllowed = false;
            }

        }
    }
    IEnumerator SpawnArrowCoroutine()
    {
        if (!hasDied)
            SpawnSageata();
        Debug.Log("spawn");
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(SpawnArrowCoroutine());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("attackArcher", true);
            inRange = true;
        }
    }

    public override void Attack()
    {

    }

    public override void TakeDMG(int damageAmount)
    {
        ApplyKnockback();
        HP -= damageAmount;

        if (HP <= 0)
        {
            hasDied = true;
            Die();
        }
    }

    public override void Die()
    {
        animator.SetBool("dieArcher", true);
        StartCoroutine(DestroyGameObj());
    }

    void SpawnSageata()
    {
        Vector3 spawnPosition = transform.position + transform.forward * spawnDistance + Vector3.up * spawnHeight;
        Quaternion spawnRotation = transform.rotation;

        // Spawnează un nou proiectil
        GameObject newProiectil = Instantiate(arrowPrefab, spawnPosition, spawnRotation);
        Rigidbody rb = newProiectil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * arrowSpeed;
        }
    }

    public void ApplyWind()
    {
        windTimer = windDuration;
        Debug.Log("Wind applied");
    }

    public void ApplyKnockback()
    {
        //knockbackTimer = knockbackDuration;
        knockback = !knockback;
    }

    IEnumerator DestroyGameObj()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
