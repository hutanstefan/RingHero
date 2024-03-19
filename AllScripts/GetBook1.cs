using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetBook : MonoBehaviour
{
    public GameObject book;
    public Quest quest;
    private bool taken = false;

    public static bool first = true;

    public TextMeshProUGUI textTip;

    public GameObject TipUI;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (quest.questMerlinActive)
            {
                if (!taken)
                {
                    textTip.text = "Tip: To see collected items, press I to open inventory";
                    TipUI.SetActive(true);
                    taken = true;
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (taken && first)
            {
                TipUI.SetActive(false);
                first = false;
            }
        }
    }

}
