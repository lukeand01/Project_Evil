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
            ScrollUIInput();
        }

        if (handler.block.HasBlock(BlockClass.BlockType.Partial)) return;

        DashInput();
        MovementInput();
        GunCombatInput();
        MeleeCombatInput();
        InteractInput();
        ScrollInput();
    }

    
    public string GetInputStringValue(KeyType keyValue)
    {
        return key.GetKey(keyValue).ToString();
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


    void InteractInput()
    {
        if (Input.GetKeyDown(key.GetKey(KeyType.Interact)))
        {
            handler.InteractWithInteractable();
        }
    }

    void ScrollUIInput()
    {
        InventoryUI inventory = UIHolder.instance.uiInventory;

        if (Input.GetKeyDown(key.GetKey(KeyType.Slot1)))
        {
            inventory.ConfirmNewScroll(0);
        }
        if (Input.GetKeyDown(key.GetKey(KeyType.Slot2)))
        {
            inventory.ConfirmNewScroll(1);
        }
        if (Input.GetKeyDown(key.GetKey(KeyType.Slot3)))
        {
            inventory.ConfirmNewScroll(2);
        }
    }
    void ScrollInput()
    {
        //it can be either for using the thing or for selecting it.
        if (Input.GetKeyDown(key.GetKey(KeyType.Slot1)))
        {
            handler.playerMagic.UseScroll(0);
        }
        if (Input.GetKeyDown(key.GetKey(KeyType.Slot2)))
        {
            handler.playerMagic.UseScroll(1);
        }
        if (Input.GetKeyDown(key.GetKey(KeyType.Slot3)))
        {
            handler.playerMagic.UseScroll(2);
        }


    }
}
