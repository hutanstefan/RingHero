using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProiectilSpawner : MonoBehaviour
{
    public GameObject proiectilPrefab; // Prefab-ul proiectilului
    public float spawnDistance = 1f; // Distanța la care se spawnează proiectilul față de poziția curentă
    public float speed = 10f; // Viteza proiectilului

    public Animator animator;
    public PlayerStats player;
    public GameObject cameraOfPlayer;
    public GameObject UI;
    public AudioSource audioSource;
    public AudioClip soundclip;
    
    private bool CD = true;


    private void OnEnable()
    {
        CD = true; 
    }

    void Update()
    {
        // Verifică dacă se apasă click dreapta
        if (!PauseMenu.gameIsPaused)
        {
            if (Input.GetMouseButtonDown(1) && UI.activeSelf && CD)
            {
                
                if (player.EnoughMana(2000))
                {
                    CD = false;
                    StartCoroutine(Cooldwn());
                    player.CastSpell(2000);
                    SpawnProiectil();

                    if (cameraOfPlayer.transform.eulerAngles.x <= 16)
                    {
                        animator.SetTrigger("spellDown");
                    }
                    else
                    {
                        animator.SetTrigger("spell");
                    }
                }
            }
        }
    }

    void SpawnProiectil()
    {
        // Calculează poziția de spawn și rotația proiectilului
        Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;
        Quaternion spawnRotation = transform.rotation;

        // Spawnează un nou proiectil
        GameObject newProiectil = Instantiate(proiectilPrefab, spawnPosition, spawnRotation);

       
            if (audioSource != null)
            {
                audioSource.clip = soundclip;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Prefab-ul nu are atașată o componentă Audio Source sau AudioClip.");
            }
        

        // Setează viteza proiectilului
        Rigidbody rb = newProiectil.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * speed;
        }
    }

    IEnumerator Cooldwn()
    {
        yield return new WaitForSeconds(1.2f);
        CD = true;
    }

}

