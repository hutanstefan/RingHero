using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ActivateStatue : MonoBehaviour
{
    public string abilityTag;
    public GameObject particle;
    public BossGate gate;
    public AudioSource audioSource;
    public AudioClip soundclip;

    void OnCollisionEnter(Collision ability)
    {
        if(ability.gameObject.tag == abilityTag && !particle.activeSelf)
        {

            if (audioSource != null)
            {
            audioSource.clip = soundclip;
            audioSource.Play();
            }

            particle.SetActive(true);
            gate.activeStatues++;
        }
    }

}
