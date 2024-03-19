using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlayer : MonoBehaviour
{
   
   public GameObject invUI;
   
    void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;  
      Cursor.visible = false;   
    }

    
    void Update()
    {
        
    }
}
