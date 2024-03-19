using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRotate : MonoBehaviour
{
    public Transform target;
    public BossStatus boss; 

    void Update()
    {
        
        if (target != null && !boss.died)
        {      
            transform.LookAt(target);
        }
    }
}
