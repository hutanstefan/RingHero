using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class ManaDisplay : MonoBehaviour
{
    public Slider slider;

    public void SetMana(int mana)
    {
        slider.value = mana;
    }

    public void UpdateBar(int MAX_MANA, int MANA)
    {
        slider.maxValue = MAX_MANA;
        slider.value = MANA;   
    }

}
