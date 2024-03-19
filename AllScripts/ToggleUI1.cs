using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{

    public GameObject UI;
    public static bool isActive;
    public playerController player;
    public bool isDungeon = false;

    void Start()
    {
        
        if (isDungeon)
        {
            isActive = true;
            UI.SetActive(true);
            CutsceneManager.gameHasStarted = true;
        }
        else 
        {
            isActive = false;
            UI.SetActive(false);
        }
    }


    void Update()
    {
        if (!PauseMenu.gameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.I) && CutsceneManager.gameHasStarted)
                if (isActive)
                {
                    closeUI();
                }
                else
                {
                    UI.SetActive(true);
                    isActive = true;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    player.sensitivity = 0;
                }
        }
    }
    public void closeUI()
    {
        UI.SetActive(false);
        isActive = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.sensitivity = 2;
    }
}
