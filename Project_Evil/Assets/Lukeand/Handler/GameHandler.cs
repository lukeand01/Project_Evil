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

    public void CreateSFX(AudioClip clip)
    {

    }
}
