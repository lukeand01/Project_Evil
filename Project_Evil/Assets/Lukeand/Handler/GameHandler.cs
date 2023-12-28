using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    public UIReferenceHolder uiRef {  get; private set; }

    public float timeModifier {  get; private set; }
    public float rightTimeScale { get; private set; }

    private void Awake()
    {
        uiRef = GetComponent<UIReferenceHolder>();  
        instance = this;
    }




    public void PauseGameTime()
    {
        Time.timeScale = timeModifier;
        rightTimeScale = 1000;
    }
    public void ResumeGameTime()
    {
        Time.timeScale = 1f;
        rightTimeScale = 1;
    }
    public void SlowGameTime()
    {
        Time.timeScale = 0.05f;
        rightTimeScale = 20;
    }

    

    public void CreateSFX(AudioClip clip)
    {

    }
}
