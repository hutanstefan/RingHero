using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindApply : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {           
            Goblin goblin = collider.GetComponent<Goblin>();
            
            if (goblin != null)
            {
                Wind(goblin);
            }

        }
        if (collider.CompareTag("Boss"))
        {
            BossStatus boss = collider.GetComponent<BossStatus>();
            Debug.Log("ColliderBoss");
            if(boss !=null)
            {
                Wind(boss);
            }
        }
    }

    public void Wind(Goblin goblin)
    {
        goblin.ApplyWind();
    }
    public void Wind(BossStatus boss)
    {
        boss.ApplyWind();
    }
}
