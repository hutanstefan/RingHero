using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WindTrigger : MonoBehaviour
{
    public TextMeshProUGUI textTip;

    public GameObject TipUI;

    private bool activated = false;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (activated == false)
            {
                activated = true;
                textTip.color = Color.yellow;
                textTip.text = "You unlocked Wind ability!";
                TipUI.SetActive(true);
                StartCoroutine(HideTip());
            }
        }
    }

    IEnumerator HideTip()
    {
        yield return new WaitForSeconds(5f);
        textTip.color = Color.white;
        TipUI.SetActive(false);
        StartCoroutine(ShowTip2());
    }

    IEnumerator ShowTip2()
    {
        yield return new WaitForSeconds(2f);
        textTip.text = "Now, activate all statues!";
        TipUI.SetActive(true);
        StartCoroutine(HideTip2());
    }

    IEnumerator HideTip2()
    {
        yield return new WaitForSeconds(5f);
        TipUI.SetActive(false);
    }
}
