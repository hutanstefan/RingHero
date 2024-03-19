using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateStatueWind : MonoBehaviour
{   
    public GameObject particle;
    public BossGate gate;
    public AudioSource audioSource;
    public AudioClip soundclip;
    private bool statueWindActivated;
    
    void Start()
    {
        statueWindActivated = false;
    }
    public void Activate()
    {
        if(!statueWindActivated)
        {

            if (audioSource != null)
            {
            audioSource.clip = soundclip;
            audioSource.Play();
            }
            
            statueWindActivated = true;
            particle.SetActive(true);
            gate.activeStatues++;
        }
    }
}
