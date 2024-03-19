using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene1 : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject cameraOfCutscene;

    void Start()
    {
        timeline.stopped += OnTimelineStopped;
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        if (director == timeline)
        {
            Debug.Log("Timeline-ul s-a terminat.");
            cameraOfCutscene.SetActive(false);
        }
    }
}
