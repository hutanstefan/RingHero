using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIQuestDisplay : MonoBehaviour
{
    public Canvas questCanvas;
    public TextMeshProUGUI merlinQuestText;
    public TextMeshProUGUI witchQuestText;
    public TextMeshProUGUI blacksmithQuestText;
    public Quest quest;

    private bool first = true;

    void Start()
    {
        questCanvas.gameObject.SetActive(false); // Initially hide the canvas
    }

    void Update()
    {

        if (quest.questMerlinActive && !quest.questMerlinCompleted && first)
        {
            first = false;
            StartCoroutine(WaitMerlinSpeak());
        }
        else if (quest.questWitchActive && !quest.questWitchCompleted)
        {
            questCanvas.gameObject.SetActive(true);
        }
        else if (quest.questBlacksmithActive && !quest.questBlacksmithCompleted)
        {
            questCanvas.gameObject.SetActive(true);
        }
        if (!quest.questMerlinActive && !quest.questBlacksmithActive && !quest.questWitchActive)
        {
            questCanvas.gameObject.SetActive(false);
        }

        // Check and display each quest text based on their status
        if (quest.questMerlinActive && !quest.questMerlinCompleted)
        {
            merlinQuestText.text = "Find Merlin's Book of Spells";
        }
        else
        {
            merlinQuestText.text = "";
        }

        if (quest.questWitchActive && !quest.questWitchCompleted)
        {
            witchQuestText.text = "Heal yourself with HP pots";
        }
        else
        {
            witchQuestText.text = "";
        }

        if (quest.questBlacksmithActive && !quest.questBlacksmithCompleted)
        {
            blacksmithQuestText.text = "Equip your new armor and sword";
        }
        else
        {
            blacksmithQuestText.text = "";
        }
    }

    IEnumerator WaitMerlinSpeak()
    {
        yield return new WaitForSeconds(27f);
        questCanvas.gameObject.SetActive(true);
    }
}