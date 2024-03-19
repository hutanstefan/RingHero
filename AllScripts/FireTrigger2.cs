using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FireTrigger : MonoBehaviour
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
                textTip.text = "You unlocked Fire ability!";
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
    }
}
