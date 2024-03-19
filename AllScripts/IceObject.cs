using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceObject : MonoBehaviour
{
   
    void OnTriggerEnter(Collider obj)
    {
        if(obj.tag == "AbilityFire")
        {
            Destroy(gameObject);
        }
        
    }
}
