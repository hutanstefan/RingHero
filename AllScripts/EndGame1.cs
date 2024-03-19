using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject screen;
    public AudioSource audioSource;
    public AudioClip soundclip;

    void Start()
    {
       StartCoroutine(End()); 
    }

    private IEnumerator End()
    {
        if (audioSource != null)
        {
            audioSource.clip = soundclip;
            audioSource.Play();
        }
        yield return new WaitForSeconds(20f);
        
        screen.SetActive(true);
    }
   
}
