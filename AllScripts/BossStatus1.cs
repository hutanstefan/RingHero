using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class BossStatus : MonoBehaviour
{
    public int MAX_HP;
    public int HP;
    public DisplayBossHP display;
    public Transform target; 
    public float movementSpeed = 6f; 
    public float attackRange = 3f; 

    public GameObject fireballPrefab;
    public GameObject trigger;

    public GameObject canvas;
    public PlayerStats player;
    public GameObject merlin;
    public AudioSource audioSource;
    public AudioClip soundclipLaught;
    public AudioClip soundclipGrowl;
    public AudioClip soundclipDash;
    public AudioClip soundclipMelle;

    public GameObject combatSound;
    
    private Animator animator;

    private bool isAttacking;
    private bool chooseState;
    private int stateIndex;
    private bool idle;
    private bool startDash;
    private float fireballSpeed = 25f;
    private Vector3 playerPosition;
    public bool died;
    private Vector3 spawnCoordinates = new Vector3(3.35f, 0.78f, -76.52f);
    private bool knockback = false;

    //boss-ul are 3 comportamente alese random

    public enum State{
        //Range -> Trimite un proiectil spre player
        RANGE,
        //Run -> Alearga spre player
        RUN,   
        //Dash -> Dash in directia playerului
        DASH
    }

    void Start()
    {
        display.UpdateBar(HP, MAX_HP);
        isAttacking = false;
        chooseState = false;
        idle = true;
        startDash = false;
        died = false;
        animator = GetComponent<Animator>();
    }

    //Toate metodele de  StartCoroutine ajuta pentru sincronizarea de animatie si comportament al boss-ului
    void Update()
    {
        if(!died)
        {
        if(!idle) 
        {

            //De fiecare daca cand boss-ul termina un tip de 'State' alege unul nou
            if(!chooseState)
            {
                chooseState = true;
                stateIndex = Random.Range(0, 3);
    
                if(stateIndex == 2) 
                {
                    playerPosition = target.position;
                }
            }

            switch ((State)stateIndex)
            {
                case State.RUN:
                    RunTowardsPlayer();
                    break;
                case State.RANGE:
                    AttackRange();
                    break;
                case State.DASH:
                    Dash();
                    break;
            }
          
        }
        }
    }

    //Metoda de primit DMG la boss + verificare daca moare
    public void TakeDMG(int damageAmount)
    {
        HP -= damageAmount;
        display.SetHealth(HP);
        if (HP <= 0 && !died)
        {
            canvas.SetActive(false);
            StartCoroutine(Die());       
        }
    }
    //Coliziuni pentru abilitati
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AbilityWater"))
        {
            TakeDMG(25);
        }

        if (collision.gameObject.CompareTag("AbilityFire"))
        {
            TakeDMG(25);
        }
    }

    //Start 'State" RUN
    //Boss-ul alearga spre player pana ajunge in range de Attack
    void RunTowardsPlayer()
    {
        if (target != null && !isAttacking)
        {
            animator.SetBool("Run", true);
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0f; 
           
            if(!knockback)
                transform.Translate(directionToTarget.normalized * movementSpeed * Time.deltaTime, Space.World);
           
            //Boss-ul incepe sa atace cand a ajuns aproape de player           
            if (directionToTarget.magnitude < attackRange)  
                AttackMelle(); 
               
        }
    }

    //Start 'State' RANGE
    void AttackRange()
    {
        if(!isAttacking)
        {
            isAttacking = true;

            StartCoroutine(StartAttackRange());
        }
    }

    //Boss-ul incepe sa atace playerul
    public void AttackMelle()
    {
        isAttacking = true;
        animator.SetBool("Run", false);
        StartCoroutine(StartAttackMelle());
    }

    //Start 'State' DASH

    public void Dash()
    {
        if(startDash)
        {
            Vector3 directionToTarget = playerPosition - transform.position;
            directionToTarget.y = 0f; 
            transform.Translate(directionToTarget.normalized * movementSpeed *6f * Time.deltaTime, Space.World);
        }


        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(StartDash());
            StartCoroutine(EndDash());
        } 


    }

    //Melle attack , daca playerul este in range de melle la sfarsitul animatiei, acesta primeste dmg
    private IEnumerator StartAttackMelle()
    {
        animator.SetBool("AttackMelle", true);
        if (audioSource != null)
        {
            audioSource.clip = soundclipMelle;
            audioSource.Play();
        }

        yield return new WaitForSeconds(0.5f);
        Vector3 directionToTarget = target.position - transform.position;
        if(directionToTarget.magnitude < 6.5f)
        {
            player.TakeDMG(25);
        }
        isAttacking = false;
        chooseState = false;
        animator.SetBool("AttackMelle", false);
    }

    //Boss-ul arunca cu un proiectil spre player
    private IEnumerator StartAttackRange()
    {
        animator.SetBool("AttackRange", true);

        if (audioSource != null)
        {
            audioSource.clip = soundclipLaught;
            audioSource.Play();
        }

        yield return new WaitForSeconds(1f);

        Vector3 spawnPosition = transform.position + Vector3.up * 2f + transform.forward * 2f; 
        GameObject fireball = Instantiate(fireballPrefab, spawnPosition, transform.rotation);
        Vector3 directionToPlayer = (target.position - spawnPosition).normalized;
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        Vector3 horizontalVelocity = directionToPlayer * fireballSpeed;     
        horizontalVelocity.y = 0;       
        rb.velocity = horizontalVelocity;

        isAttacking = false;
        chooseState = false;
        animator.SetBool("AttackRange", false);
    }

    //Bossul incepe dash-ul spre directa playerului
    private IEnumerator StartDash()
    {  
        yield return new WaitForSeconds(1f);

        if (audioSource != null)
        {
            audioSource.clip = soundclipDash;
            audioSource.Play();
        }

        animator.SetBool("Dash", true);
        startDash = true;  
    }

    //La sfasitul dash-ului bossul apeleza metoda de Attack Melle
    private IEnumerator EndDash()
    {
        
        yield return new WaitForSeconds(1.4f);
        transform.position += Vector3.up * 0.5f;
        animator.SetBool("Dash", false);
    
        startDash = false;
        StartCoroutine(StartAttackMelle());
    }

    //Die, atunci cand bossul ramane fara hp
    private IEnumerator Die()
    {
        animator.SetBool("Die", true);
        died = true;
        if (audioSource != null)
        {
            audioSource.clip = soundclipGrowl;
            audioSource.Play();
        }
        combatSound.SetActive(false);

        yield return new WaitForSeconds(10f);

        merlin.SetActive(true);
        Destroy(gameObject);
    }

    //Metoda in care vantul este aplicat pe boss
    public void ApplyWind()
    {
        StartCoroutine(Knockback());
    }

    private IEnumerator Knockback()
    {
        knockback = true;

        yield return new WaitForSeconds(2f);

        knockback = false;
    }
   

    //Metode de actualizat statusul bossului, pentru a stii daca trebuie sa atace playerul sau nu
    public void SetIdleStatus()
    {     
        this.idle = false;   
        canvas.SetActive(true);
    }

    public void SetIdleStatusTrue()
    {     
        this.idle = true;   
        canvas.SetActive(false);
    }
    
    //Spawneaza bossul la coordonatele propriu-zise
    public void SetSpawn()
    {
       transform.position = spawnCoordinates;
    }

}
