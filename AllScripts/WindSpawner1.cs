using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    public GameObject wind1;
    public GameObject wind2;
    public GameObject wind3;
    public float spawnDistance = 10f; // Distanța la care se spawnează proiectilul față de poziția curentă
    public float speed = 1f; // Viteza proiectilului

    public Animator animator;
    public PlayerStats player;
    public GameObject cameraOfPlayer;

    public GameObject thePlayer;
    public GameObject UI;
    public AudioSource audioSource;
    public AudioClip soundclip;

    [SerializeField] Vector3 offset1 = new Vector3(1f, 0f, 0f); // Offset pentru wind1
    [SerializeField] Vector3 offset2 = new Vector3(0f, 1f, 0f); // Offset pentru wind2
    [SerializeField] Vector3 offset3 = new Vector3(0f, 0f, 1f);

    void Update()
    {
        // Verifică dacă se apasă click dreapta
        if (!PauseMenu.gameIsPaused)
        {
            if (Input.GetMouseButtonDown(1) && UI.activeSelf)
            {
                if (player.EnoughMana(2000))
                {
                    player.CastSpell(2000);
                    SpawnProiectil();
                    Wind();

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
        Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;
        Quaternion spawnRotation = Quaternion.Euler(0f, thePlayer.transform.rotation.eulerAngles.y - 90f, 0f);
        Instantiate(wind1, spawnPosition + offset1, spawnRotation);
        Instantiate(wind2, spawnPosition + offset2, spawnRotation);
        Instantiate(wind3, spawnPosition + offset3, spawnRotation);

        if (audioSource != null)
        {
            audioSource.clip = soundclip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Prefab-ul nu are atașată o componentă Audio Source sau AudioClip.");
        }

    }

    void Wind()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
        foreach(Collider c in colliders)
        {
            
            if(c.GetComponent<Goblin>())
            {
                c.GetComponent<Goblin>().ApplyWind();
            }

            if(c.GetComponent<BossStatus>())
            {
                c.GetComponent<BossStatus>().ApplyWind();
            }

            if(c.GetComponent<ActivateStatueWind>())
            {
                c.GetComponent<ActivateStatueWind>().Activate();
            }
        }
    }
}
