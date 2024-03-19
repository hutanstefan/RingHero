using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
using TMPro;
public class DisplayBossHP : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI textHP;
    

    public void SetHealth(int health)
    {
        slider.value = health;
        textHP.text = $"{slider.value}/{slider.maxValue}";
    }

    public void UpdateBar(int MAX_HP,int HP)
    {
        slider.maxValue = MAX_HP;
        slider.value = HP;
        textHP.text = $"{slider.value}/{slider.maxValue}";
    }
}
