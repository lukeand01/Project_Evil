using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
   public static PlayerHandler Instance;

     public PlayerMove move { get; private set; }
     public PlayerController controller { get; private set; }
     public PlayerCombat combat { get; private set; }
     public EntityHandler entityHandler { get; private set; }


    private void Awake()
    {
        Instance = this;

        move = GetComponent<PlayerMove>();
        controller = GetComponent<PlayerController>(); 
        combat = GetComponent<PlayerCombat>();

        entityHandler = GetComponent<EntityHandler>();
    }




}
