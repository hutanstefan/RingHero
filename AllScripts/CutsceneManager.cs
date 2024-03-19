using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public static bool gameHasStarted = false;
    public PlayableDirector timeline1;
    public PlayableDirector timeline2;
    public Canvas canvas;
    public GameObject canvasUI;

    public GameObject cutscene2;

    public GameObject camera2;
    public bool canvasDone = false;

    private bool skipCutscene = false;
    private bool skipCutscene2 = false;
    private bool done = false;
    public AudioClip soundClip; // Drag your sound clip here in the Unity Editor
    public AudioSource audioSource;
    public GameObject background;
    private bool soundPlayed;
    public GameObject UISkip;
    public GameObject TipUI;
    bool activeTipUI = false;
    void Start()
    {
        audioSource.clip = soundClip;
        timeline1.stopped += OnTimelineStopped;
        timeline2.stopped += OnTimelineStopped;
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        if (director == timeline1)
        {
            skipCutscene = true;
            Debug.Log("AAA");
        }
        if (director == timeline2)
        {
            skipCutscene2 = true;
            Debug.Log("Skip cutscene 2");
        }
    }

    void Update()
    {
        if (!skipCutscene2 && done)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canvasDone)
            {
                timeline2.time = timeline2.duration;
                skipCutscene2 = true;
            }
        }
        if (skipCutscene2 && Horse.pressed == false)
        {
            canvasUI.SetActive(true);
            UISkip.SetActive(false);
            background.SetActive(true);
            gameHasStarted = true;
            playerController.canMove = true;
            if (!activeTipUI)
            {
                TipUI.SetActive(true);
                activeTipUI = true;
                StartCoroutine(HideTip());
            }
            if (!soundPlayed)
            {
                audioSource.PlayOneShot(soundClip);
                soundPlayed = true;
            }

        }
        if (!skipCutscene)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                timeline1.time = timeline1.duration;
                skipCutscene = true;
            }
        }
        if (!done)
            if (skipCutscene)
            {
                done = true;
                UISkip.SetActive(false);
                StartCoroutine(PlayCanvas());
                Debug.Log("BBB");
            }

    }

    IEnumerator PlayCanvas()
    {
        canvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        canvas.gameObject.SetActive(false);
        canvasDone = true;
        //canvasUI.SetActive(true);
        camera2.SetActive(true);
        cutscene2.SetActive(true);
        UISkip.SetActive(true);
    }

    IEnumerator HideTip()
    {
        yield return new WaitForSeconds(7f);
        TipUI.SetActive(false);
    }
}
