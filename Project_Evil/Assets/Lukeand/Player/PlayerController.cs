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

    private void Update()
    {
        if (handler.block.HasBlock(BlockClass.BlockType.Complete)) return;
        

        if (!handler.block.HasBlock(BlockClass.BlockType.UI))
        {
            InventoryInput();
        }

        if (handler.block.HasBlock(BlockClass.BlockType.Partial)) return;

        DashInput();
        
        GunCombatInput();
        MeleeCombatInput();
    }

    private void FixedUpdate()
    {
        if (handler.block.HasBlock(BlockClass.BlockType.Complete)) return;


        if (!handler.block.HasBlock(BlockClass.BlockType.UI))
        {
            return;
        }

        if (handler.block.HasBlock(BlockClass.BlockType.Partial)) return;


        MovementInput();
    }

    void MovementInput()
    {
        Vector2 moveDir = Vector2.zero;
        if (Input.GetKey(key.GetKey(KeyType.MoveLeft))) moveDir += new Vector2(-1, 0);
        if (Input.GetKey(key.GetKey(KeyType.MoveRight))) moveDir += new Vector2(1, 0);
        if (Input.GetKey(key.GetKey(KeyType.MoveUp))) moveDir += new Vector2(0, 1);
        if (Input.GetKey(key.GetKey(KeyType.MoveDown))) moveDir += new Vector2(0, -1);

        handler.playerMove.MovePlayer(moveDir);
    }

    void DashInput()
    {
        if (Input.GetKeyDown(key.GetKey(KeyType.Dash)))
        {
            handler.playerMove.Dash();
        }
    }

    void GunCombatInput()
    {

        if(Input.GetKey(key.GetKey(KeyType.Shoot)))
        {
            handler.playerCombat.Shoot();
        }
        else
        {
            handler.playerCombat.StoppedShooting();
        }

        if (Input.GetKey(key.GetKey(KeyType.Aim)))
        {
            handler.playerCombat.Aim();
        }
        else
        {
            handler.playerCombat.StopAiming();
        }

        if (Input.GetKey(key.GetKey(KeyType.Reload)))
        {
            handler.playerCombat.Reload();
        }

    }

    void MeleeCombatInput()
    {
        if (Input.GetKeyDown(key.GetKey(KeyType.UseSword)))
        {
            handler.playerCombat.UseSword();
        }

        if (Input.GetKeyDown(key.GetKey(KeyType.UseShield)))
        {
            handler.playerCombat.UseShield();
        }
    }


    void InventoryInput()
    {
        if (Input.GetKeyDown(key.GetKey(KeyType.Inventory)))
        {
            //call this.
            UIHolder.instance.uiInventory.ControlUI();
        }
    }

}
