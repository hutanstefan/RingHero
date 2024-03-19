using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDmgToPlayer : MonoBehaviour
{
   void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            PlayerStats playerStats = other.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDMG(25);
            }
        }
    }
    
}
