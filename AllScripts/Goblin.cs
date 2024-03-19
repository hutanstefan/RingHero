using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Mob
{
    private bool speedModified;
    private float originalSpeed;
    private bool hasAttacked;
    private float distanceToPlayer;

    public Material normalMaterial;
    public Material freezingMaterial;
    public Material fireMaterial;
    private Renderer rend;

    public GameObject body;

    public float windForce = 10f;
    public float windUpForce = 2f;
    public float knockbackForce;
    public float knockbackUpForce;
    public float windDuration = 0.2f;
    //public float knockbackDuration = 0.2f;
    private bool knockback;
    private Rigidbody rb;
    private float windTimer;
    private float knockbackTimer;

    public bool isInWindRange = false;

    public Goblin(int _hp, int _attack, float _speed) : base(_hp, _attack) { }

    public override void Attack()
    {
        if (distanceToPlayer < 4f)
            playerStats.TakeDMG(attack);
    }

    void Start()
    {
        rend = body.GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
    }

    public override void TakeDMG(int damageAmount)
    {
        ApplyKnockback();
        HP -= damageAmount;

        if (HP <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        animator.SetBool("dieGoblin", true);
        StartCoroutine(DestroyGameObj());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AbilityWater"))
        {
            TakeDMG(20);
            ModifySpeed(2.0f, 3.0f);
        }
        if (collision.gameObject.CompareTag("AbilityFire"))
        {
            TakeDMG(20);
            rend.material = fireMaterial;
            StartCoroutine(RestoreMaterial(3.0f));

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetBool("run", true);
            inRange = true;
        }
    }

    private IEnumerator RestoreOriginalSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
        speedModified = false;
        animator.SetBool("slow", false);
        rend.material = normalMaterial;
    }

    private IEnumerator RestoreMaterial(float duration)
    {
        yield return new WaitForSeconds(duration);
        rend.material = normalMaterial;
    }

    private void ModifySpeed(float newSpeed, float duration)
    {
        if (!speedModified)
        {
            animator.SetBool("slow", true);
            rend.material = freezingMaterial;
            originalSpeed = speed;
            speed = newSpeed;
            speedModified = true;
            StartCoroutine(RestoreOriginalSpeed(duration));
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
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (distanceToPlayer < 2.5f && !hasAttacked)
            {
                animator.SetTrigger("attack");
                StartCoroutine(AttackAfterDelay(0.5f));
                StartCoroutine(FinalAttack(1.5f));
                hasAttacked = true;
            }
        }
    }

    private IEnumerator AttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Attack();
    }

    private IEnumerator FinalAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        hasAttacked = false;
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