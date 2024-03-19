using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestBlackSmith : MonoBehaviour
{
    public GameObject inv;
    public Quest quest;
    public GameObject player;
    public GameObject arrmorSet;
    private float distanceToPlayer;
    private StaticInterface dym;
    public Animator anim;

    public AudioClip sound1;
    public AudioClip sound2;
    public AudioSource audioSource1;
    public TextMeshProUGUI textInteract;
    private bool first = true;

    public TextMeshProUGUI textTip;

    public GameObject TipUI;

    private bool hasActivatedArmorSet = false;

    void Start()
    {
        dym = inv.GetComponent<StaticInterface>();
    }

    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        distanceToPlayer = direction.magnitude;

        if (quest.questBlacksmithActive && !quest.questBlacksmithCompleted)
        {
            dym.QuestBlackSmith();
        }

        if (quest.questBlacksmithCompleted)
        {
            if (first)
            {
                first = false;
                textTip.color = Color.yellow;
                textTip.text = "QUEST COMPLETED!";
                TipUI.SetActive(true);
                StartCoroutine(HideTip());
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && quest.MerlinTalked && (!quest.questWitchActive || (quest.questWitchActive && quest.questWitchCompleted)))
        {

            if (distanceToPlayer < 2.5f)
            {
                if (!quest.questBlacksmithCompleted && !quest.questBlacksmithActive) textInteract.text = " Press E to interact with BlackSmith";
                else textInteract.text = "";

                if (!quest.questBlacksmithActive && !quest.questBlacksmithCompleted)
                {
                    quest.questBlacksmithActive = true;

                    StartCoroutine(PlaySoundAndActivateArmorSet());
                }
            }
        }

        if (distanceToPlayer < 2.5f)
        {
            if (!quest.questBlacksmithCompleted && !quest.questBlacksmithActive && quest.MerlinTalked && (!quest.questWitchActive || (quest.questWitchActive && quest.questWitchCompleted)))
                textInteract.text = " Press E to interact with BlackSmith";
            else textInteract.text = "";
        }
        else
        {
            textInteract.text = "";
        }

    }

    IEnumerator PlaySoundAndActivateArmorSet()
    {

        audioSource1.PlayOneShot(sound1);
        anim.SetBool("isTalking", true);

        yield return new WaitForSeconds(sound1.length);


        if (!hasActivatedArmorSet)
        {
            StartCoroutine(PlaySound2());

            arrmorSet.SetActive(true);
            hasActivatedArmorSet = true;
        }
    }

    IEnumerator PlaySound2()
    {
        audioSource1.PlayOneShot(sound2);

        yield return new WaitForSeconds(sound2.length);

        anim.SetBool("isTalking", false);
    }
    IEnumerator HideTip()
    {
        yield return new WaitForSeconds(5f);
        textTip.color = Color.white;
        TipUI.SetActive(false);
    }
}