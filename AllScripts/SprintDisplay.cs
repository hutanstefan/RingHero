using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintDisplay : MonoBehaviour
{

    public Slider slider;
    public PlayerStats playerS;
    bool use;

    public void UseSprint(int cs)
    {
        if (cs == 1)
        {
            playerS.StaminaUse();
            use = true;
        }
        else use = false;

    }

    public bool GetValue()
    {
        if (slider.value == 0) return true;
        return false;
    }

    public void SetStamina(int stamina)
    {
        slider.value = stamina;
    }
    void Update()
    {
        if (!PauseMenu.gameIsPaused)
        {
            if (playerS.Stamina < 5000 && !(use))
                playerS.Stamina += 2;
        }
    }

}