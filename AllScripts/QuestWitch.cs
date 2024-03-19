using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestWitch : MonoBehaviour
{
    public Quest quest;
    public GameObject player;
    public PlayerStats playerStats;
    public GameObject hpPot;
    private float distanceToPlayer;

    public AudioClip sound1;
    public AudioClip sound2;
    public AudioSource audioSource1;
    public TextMeshProUGUI textInteract;

    public TextMeshProUGUI textTip;

    public GameObject TipUI;

    void Update()
    {

        Vector3 direction = player.transform.position - transform.position;
        distanceToPlayer = direction.magnitude;
        if (!quest.questWitchCompleted && quest.questWitchActive)
        {
            if (playerStats.HP == playerStats.MAX_HP)
            {
                Debug.Log("You complete the quest");
                textTip.color = Color.yellow;
                textTip.text = "QUEST COMPLETED!";
                TipUI.SetActive(true);
                StartCoroutine(HideTip());
                quest.questCompleted++;
                quest.questWitchCompleted = true;
                quest.questWitchActive = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && quest.MerlinTalked && (!quest.questBlacksmithActive || (quest.questBlacksmithActive && quest.questBlacksmithCompleted)))
        {

            if (distanceToPlayer < 2.5f)
            {

                if (!quest.questWitchActive && !quest.questWitchCompleted)
                {
                    playerStats.TakeDMG(90);
                    StartCoroutine(PlaySoundAndActivateHpPot());
                    quest.questWitchActive = true;

                }
            }
        }


        if (distanceToPlayer < 2.5f && quest.MerlinTalked && (!quest.questBlacksmithActive || (quest.questBlacksmithActive && quest.questBlacksmithCompleted)))
        {
            if (!quest.questWitchCompleted && !quest.questWitchActive) textInteract.text = " Press E to interact with Witch";
            else textInteract.text = "";

        }
        else
        {
            textInteract.text = "";
        }
    }

    IEnumerator PlaySoundAndActivateHpPot()
    {

        audioSource1.PlayOneShot(sound1);



        yield return new WaitForSeconds(sound1.length);

        audioSource1.PlayOneShot(sound2);
        hpPot.SetActive(true);


    }

    IEnumerator HideTip()
    {
        yield return new WaitForSeconds(5f);
        textTip.color = Color.white;
        TipUI.SetActive(false);
    }
}