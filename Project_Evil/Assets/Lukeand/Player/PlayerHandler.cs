using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
   public static PlayerHandler Instance;

    [HideInInspector] public PlayerMove move;
    [HideInInspector] public PlayerController controller;
    [HideInInspector] public PlayerCombat combat;

    private void Awake()
    {
        Instance = this;

        move = GetComponent<PlayerMove>();
        controller = GetComponent<PlayerController>(); 
        combat = GetComponent<PlayerCombat>();
    }




}
