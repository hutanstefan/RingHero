using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    public BossStatus bossS;
    public GameObject gate;
    public GameObject sceneLight;
    public AudioSource audioSource;
    public AudioClip soundclipStart;
    public GameObject combatSound;
    public GameObject dungeonSound;
    public GameObject colliderBoss;
    public bool isActiveTrigger;
    public BossGate gateBoss;
    public InventoryObject inventory;
    public InventoryObject equipment;
    public GameObject doorLight;

    void Start()
    {
        isActiveTrigger = false;
    }

   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActiveTrigger)
        {     
            isActiveTrigger = true;     
            inventory.Save();
            equipment.Save();
            StartCoroutine(StartBoss());
        }
    }

    public IEnumerator StartBoss()
    {
        dungeonSound.SetActive(false);
        gate.SetActive(true);
        doorLight.SetActive(false);

        if (audioSource != null)
        {
            audioSource.clip = soundclipStart;
            audioSource.Play();
        }

    yield return new WaitForSeconds(4f);

        colliderBoss.SetActive(false);
        bossS.SetIdleStatus();
        sceneLight.SetActive(true);
        combatSound.SetActive(true);
    }


    public void ResetTrigger()
    {
        isActiveTrigger = false;
        dungeonSound.SetActive(true);
        

        if(gateBoss.activeStatues == 4)
        {
            gate.SetActive(false);
            doorLight.SetActive(true);
        }

        colliderBoss.SetActive(true);
        sceneLight.SetActive(false);
        combatSound.SetActive(false);
    }
}
