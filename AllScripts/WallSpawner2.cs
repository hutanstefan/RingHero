using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject proiectilPrefab; // Prefab-ul proiectilului
    public float spawnDistance = 1f; // Distanța la care se spawnează proiectilul față de poziția curentă
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
        if (!PauseMenu.gameIsPaused)
        {
            if (Input.GetMouseButtonDown(1) && UI.activeSelf && CD)
            {
                if (player.EnoughMana(2000))
                {
                    player.CastSpell(2000);
                    StartCoroutine(Cooldwn());
                    CD = false;
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
        Vector3 spawnPosition = transform.position + transform.forward * spawnDistance;
        Quaternion spawnRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        Instantiate(proiectilPrefab, spawnPosition, spawnRotation);

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

    IEnumerator Cooldwn()
    {
        yield return new WaitForSeconds(1.2f);
        CD = true;
    }
}
