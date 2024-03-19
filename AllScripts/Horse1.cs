using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.ExceptionServices;

public class Horse : MonoBehaviour
{
    public GameObject player;
    private float distanceToPlayer;
    public TextMeshProUGUI textInteract;

    public Quest quest;
    private bool first = true;

    public TextMeshProUGUI textTip;

    public GameObject TipUI;

    public GameObject LoadingScreen;

    public GameObject UIToHide;
    public static bool pressed = false;

    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        distanceToPlayer = direction.magnitude;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (distanceToPlayer < 2.5f && quest.questCompleted == 3)
            {
                pressed = true;
                LoadingScreen.SetActive(true);
                UIToHide.SetActive(false);
                textInteract.text = "";
               
                StartCoroutine(BeginToLoad());
            }
        }

        if (quest.questCompleted == 3)
        {
            if (first)
            {
                first = false;
                StartCoroutine(horseUI());
            }
            if (distanceToPlayer < 2.5f && pressed == false)
            {
                textInteract.text = "Press E to go to the Cave";
            }
            else
            {
                textInteract.text = "";
            }
        }
    }

    IEnumerator HideTip()
    {
        yield return new WaitForSeconds(5f);
        TipUI.SetActive(false);
    }

    IEnumerator horseUI()
    {
        yield return new WaitForSeconds(5f);
        textTip.text = "Take the horse and go to the cave";
        TipUI.SetActive(true);
        StartCoroutine(HideTip());
    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);



        while (!operation.isDone)
        {

            yield return null;
        }
    }

    IEnumerator BeginToLoad()
    {
        yield return new WaitForSeconds(2f);
        LoadScene(2);
    }
}
