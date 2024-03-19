using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public int index;
    public DisplaySpell player;
  
    
    void OnTriggerEnter(Collider obj)
    {
        if(obj.tag == "Player")
        {
           player.own[index] = true;
           Destroy(gameObject);
        }
    }
}
