using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class HPDisplay : MonoBehaviour
{
        public Slider slider;

        public void SetHealth(int health)
        {
            slider.value = health;
        }

        public void UpdateBar(int MAX_HP,int HP)
        {
            slider.maxValue = MAX_HP;
            slider.value = HP;
        }

}     
