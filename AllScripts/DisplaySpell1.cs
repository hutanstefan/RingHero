using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySpell : MonoBehaviour
{
    public Sprite[] sprites; // Array to hold the sprites you want to switch between

    public bool[] own;
    private Image imageComponent;
    private int currentIndex = 0;

    public GameObject[] abilitys;

    void Start()
    {
        imageComponent = GetComponent<Image>(); // Getting the Image component attached to this GameObject
        if (imageComponent == null)
        {
            Debug.LogError("No Image component found on the GameObject!");
            enabled = false; // Disabling the script to prevent further errors
        }
        else
        {
            // Set the initial sprite
            if (sprites.Length > 0)
            {
                imageComponent.sprite = sprites[currentIndex];
            }
            else
            {
                Debug.LogError("No sprites added to the array!");
                enabled = false; // Disabling the script to prevent further errors
            }
        }

        for (int i = 0; i < abilitys.Length; i++)
        {
            abilitys[i].SetActive(false);
        }
        abilitys[0].SetActive(true);

    }

    void Update()
    {
        // Change sprite on mouse wheel scroll
        if (!PauseMenu.gameIsPaused)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                // Change sprite based on scroll direction
                if (scroll > 0)
                {
                    ChangeToPreviousSprite();
                }
                else
                {
                    ChangeToNextSprite();
                }
            }
        }




    }

    void ChangeToNextSprite()
    {
        currentIndex = (currentIndex + 1) % sprites.Length; // Loop back to the first sprite if reached the end

        while (!own[currentIndex])
        {
            currentIndex = (currentIndex + 1) % sprites.Length;
        }

        imageComponent.sprite = sprites[currentIndex];

        for (int i = 0; i < abilitys.Length; i++)
        {
            abilitys[i].SetActive(false);
        }

        abilitys[currentIndex].SetActive(true);
    }

    void ChangeToPreviousSprite()
    {
        currentIndex = (currentIndex - 1 + sprites.Length) % sprites.Length; // Loop back to the last sprite if reached the beginning

        while (!own[currentIndex])
        {
            currentIndex = (currentIndex - 1 + sprites.Length) % sprites.Length;
        }

        imageComponent.sprite = sprites[currentIndex];

        for (int i = 0; i < abilitys.Length; i++)
        {
            abilitys[i].SetActive(false);
        }

        abilitys[currentIndex].SetActive(true);
    }
}
