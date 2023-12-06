using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    private void Awake()
    {
        instance = this; 
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0001f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }



    public void CreateSFX(AudioClip clip)
    {

    }
}
