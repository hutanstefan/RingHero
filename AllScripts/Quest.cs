using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public bool questMerlinActive;
    public bool questMerlinCompleted;
    public bool questWitchActive;
    public bool questWitchCompleted;
    public bool questBlacksmithActive;
    public bool questBlacksmithCompleted;
    public bool MerlinTalked;
    public int questCompleted;
    


    void Start()
    {
        questMerlinActive = false;
        questWitchActive = false;
        questBlacksmithActive = false;
        questWitchCompleted = false;
        questBlacksmithCompleted = false;
        questMerlinCompleted = false;
        MerlinTalked = false;
        questCompleted = 0;
    }

}
