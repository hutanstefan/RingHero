using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    
    public PlayerStats playerS;
    
   
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider obj)
    {
        if(obj.tag == "AbilityWater")
        {
            Destroy(gameObject);
        }
        
        if(obj.tag == "Player")
        {
            playerS.TakeDMG(1000);
        }
    }

}
