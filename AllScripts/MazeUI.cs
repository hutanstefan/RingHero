using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MazeUI : MonoBehaviour
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
                textTip.text = "Solve the maze!";
                TipUI.SetActive(true);
                StartCoroutine(HideTip());
            }
        }
    }

    IEnumerator HideTip()
    {
        yield return new WaitForSeconds(7f);
        TipUI.SetActive(false);
    }
}
