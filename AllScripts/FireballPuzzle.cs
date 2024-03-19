using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballPuzzle : MonoBehaviour
{
   void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            PlayerStats playerStats = other.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDMG(1000);
            }
        }

        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "AbilityEarth")
            Destroy(gameObject);
    }

    
}
