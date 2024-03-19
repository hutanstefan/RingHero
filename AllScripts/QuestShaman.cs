using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestShaman : MonoBehaviour
{
    public GameObject inv;
    public Quest quest;
    public GameObject player;
    public GameObject MerlinBook;
    public Animator anim;

    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip sound3;
    public AudioClip sound4;
    public AudioSource audioSource1;
    private float distanceToPlayer;
    private bool talked = false;
    public TextMeshProUGUI textInteract;
    public TextMeshProUGUI textTip;

    public float timeRing;

    public GameObject TipUI;

    public GameObject AbilityUI;


    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        distanceToPlayer = direction.magnitude;

        if (CutsceneManager.gameHasStarted)
        {
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.y = Quaternion.LookRotation(direction).eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(currentRotation);
            transform.rotation = targetRotation;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {


            if (distanceToPlayer < 2.5f)
            {
                if (quest.questMerlinActive)
                {
                    if (GetBook.first == false)
                    {
                        DynamicInterface dym = inv.GetComponent<DynamicInterface>();
                        dym.QuestMerlin();
                    }
                }
                else
                {
                    if (!quest.questMerlinCompleted)
                    {
                        StartCoroutine(PlaySound());
                        Debug.Log("Find Merlin's book of spells");
                        quest.questMerlinActive = true;
                    }
                }
            }
        }

        if (distanceToPlayer < 2.5f)
        {

            if (!quest.questMerlinCompleted)
            {
                if (!quest.questMerlinActive)
                    textInteract.text = "Press E to interact with Merlin";
                else
                {
                    if (GetBook.first == false)
                        textInteract.text = "Press E to interact with Merlin";
                    else
                        textInteract.text = "";
                }
            }
            else textInteract.text = "";
        }
        else
        {
            textInteract.text = "";
        }

        if (quest.questMerlinCompleted && !talked)
        {
            talked = true;
            StartCoroutine(PlaySound3());
        }
    }

    IEnumerator PlaySound()
    {
        audioSource1.PlayOneShot(sound1);
        anim.SetBool("isTalking", true);
        StartCoroutine(RingUI());
        yield return new WaitForSeconds(sound1.length);

        MerlinBook.SetActive(true);
        StartCoroutine(PlaySound2());
    }

    IEnumerator PlaySound2()
    {
        audioSource1.PlayOneShot(sound2);


        yield return new WaitForSeconds(sound2.length);
        anim.SetBool("isTalking", false);
    }

    IEnumerator PlaySound3()
    {
        audioSource1.PlayOneShot(sound3);
        StartCoroutine(SpellUI());
        anim.SetBool("isTalking", true);

        yield return new WaitForSeconds(sound3.length);

        StartCoroutine(PlaySound4());
    }

    IEnumerator PlaySound4()
    {
        audioSource1.PlayOneShot(sound4);

        yield return new WaitForSeconds(sound3.length - 7);
        quest.MerlinTalked = true;
        anim.SetBool("isTalking", false);

    }

    IEnumerator RingUI()
    {
        yield return new WaitForSeconds(timeRing);
        textTip.color = Color.yellow;
        textTip.text = "You received the magic ring!";
        TipUI.SetActive(true);
        StartCoroutine(HideTip2());
    }

    IEnumerator SpellUI()
    {
        yield return new WaitForSeconds(10f);
        AbilityUI.SetActive(true);
        textTip.text = "Tip: Press Right-Click to use spell.";
        TipUI.SetActive(true);
        StartCoroutine(HideTip());
    }

    IEnumerator HideTip()
    {
        yield return new WaitForSeconds(5f);
        TipUI.SetActive(false);
    }

    IEnumerator HideTip2()
    {
        yield return new WaitForSeconds(5f);
        textTip.color = Color.white;
        TipUI.SetActive(false);
    }
}
