using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGate : MonoBehaviour
{
    public int activeStatues;
    public GameObject lightDoor;
    private bool activated;


    void Start()
    {
        activeStatues = 0;
        activated = false;
        lightDoor.SetActive(false);
    }

    void Update()
    {
        if(activeStatues == 4 && !activated)
        {
            activated = true;
            lightDoor.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
