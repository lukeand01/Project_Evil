using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{

    PlayerHandler handler;
    KeyClass key;


    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();  
        key = new KeyClass();
    }

    private void FixedUpdate()
    {
        MovementInput();
        CombatInput();
        InventoryInput();
    }

    void MovementInput()
    {
        Vector2 moveDir = Vector2.zero;
        if (Input.GetKey(key.GetKey(KeyType.MoveLeft))) moveDir += new Vector2(-1, 0);
        if (Input.GetKey(key.GetKey(KeyType.MoveRight))) moveDir += new Vector2(1, 0);
        if (Input.GetKey(key.GetKey(KeyType.MoveUp))) moveDir += new Vector2(0, 1);
        if (Input.GetKey(key.GetKey(KeyType.MoveDown))) moveDir += new Vector2(0, -1);

        handler.move.MovePlayer(moveDir);
    }

    void CombatInput()
    {
        if(Input.GetKey(key.GetKey(KeyType.Shoot)))
        {
            handler.combat.Shoot();
        }

        if (Input.GetKey(key.GetKey(KeyType.Aim)))
        {
            handler.combat.Aim();
        }
        else
        {
            handler.combat.StopAiming();
        }


    }

    void InventoryInput()
    {
        if (Input.GetKeyDown(key.GetKey(KeyType.Inventory)))
        {
            //call this.
            UIHolder.instance.inventory.Open();
        }
    }

}
