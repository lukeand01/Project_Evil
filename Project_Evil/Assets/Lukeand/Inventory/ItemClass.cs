using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

[System.Serializable]
public class ItemClass 
{
    public ItemData data;
    public int quantity;

    PlayerInventory inventoryHandler;

    public int slotID {  get; private set; }    


    public GunClass gun { get; private set; } = null;
    public ToolClass tool { get; private set; } = null;

    public bool IsEquipped { get; private set; } //if this is ever case then we show something.


    public bool isResourceBeingUsed { get; private set; }


    public void SetResourceAsBeingUsed() => isResourceBeingUsed = true;

    public ItemClass(ItemData data, int quantity)
    {
        this.data = data;
        this.quantity = quantity;

        isResourceBeingUsed = false;
    }



    #region RECEIVE
    public void ReceiveNewSlotID(int slotID)
    {
        this.slotID = slotID;
    }

    public void ReceiveNewData(ItemData data)
    {
        this.data = data;
        this.quantity = 1;

        isResourceBeingUsed = false;
        //create gun or tool

        ItemGunData gunData = data.GetGun();

        if (gunData != null)
        {
            gun = new GunClass(gunData);
        }
        else
        {
            gun = null;
        }

        ItemToolData toolData = data.GetTool();

        if (toolData != null)
        {
            tool = new ToolClass(toolData);
        }
        else
        {
            tool = null;
        }

        if(PlayerHandler.Instance != null)
        {

            PlayerHandler.Instance.playerCombat.EquipIfEmpty(this);
        }
        else
        {
            Debug.Log("couldnt do this");
        }
        
        UpdateAnyLinkedUI();
    }

    public void ReceiveNewHandler(PlayerInventory handler)
    {
       inventoryHandler = handler;    
    }

    #endregion

    public void ControlEquip(bool choice)
    {
        IsEquipped = choice;
        if (inventoryUnit != null) inventoryUnit.UpdateEquippedUI();
        
    }
    void EmptyData()
    {
        if(data.itemType == ItemType.Ammo)
        {
            //then we inform the handler that this fella was changed.

        }


        data = null;
        quantity = 0;
        isResourceBeingUsed = false;
        

    }

    #region QUANTITY
    public void IncreaseQuantity(int quantity = 1)
    {
        this.quantity += quantity;
        UpdateAnyLinkedUI();
    }
    public void DecreaseQuantity(int quantity = 1, string debug = "")
    {


        this.quantity -= quantity;
        if (data.itemType == ItemType.Ammo)
        {
            //Debug.Log("this is being reduced");
        }

        if (this.quantity <= 0)
        {

            EmptyData();
            
        }

        UpdateAnyLinkedUI();

        if (debug != "")
        {
            Debug.Log("quantity " + quantity + " " + this.quantity + " " + debug);
        }

        
    }

    public void DecideIfShouldReduce()
    {
        if(data.GetGun() != null)
        {

        }
        else
        {
            DecreaseQuantity();
        }
    }


    public int GetAmountToStack()
    {
        return data.stackLimit - quantity;
    }

    public int GetAmountToStackClamped(int amount)
    {
        int value = data.stackLimit - quantity;
        value = Mathf.Clamp(value, 0, amount);
        return value;
    }
    #endregion

    #region UI

    void UpdateAnyLinkedUI(string debug = "")
    {
        

        if (inventoryUnit != null)
        {
            if (debug != "")
            {
              Debug.Log("update the fella ");
            }

            inventoryUnit.UpdateUI();
        }
    }

    InventoryUnit inventoryUnit;
    public void SetUpInventoryUnit(InventoryUnit inventoryUnit)
    {
        this.inventoryUnit = inventoryUnit;
    }


    #endregion


    public void UseItem()
    {
        data.UseItem(this);
        if (data.GetUsable() != null)
        {
            DecreaseQuantity(1);
        }
        //reduce quantity if it is reduceable
    }

    public bool IsInteractable()
    {
        if (data == null) return false;
        if(data.GetEquipment() != null) return true;
        if(data.GetUsable() != null) return true;   
        if(IsEquippable()) return true;

        return false;
    }

    public bool IsEquippable()
    {
        if (data.GetTool() != null && !IsEquipped) return true;
        if (data.GetGun() != null && !IsEquipped) return true;
        return false;
    }


    #region CREATE
    public void CreateGun()
    {

    }
   
    public void CreateTool()
    {

    }

    #endregion
}


//i dont want to cehck everything everytime.
//so i need to add to a list to check it better 
//we add and when anything happens we just cchecck if someone was rmeoved.

//the moments wwe check
//


//i would like to create an int value about it.
//it should reflect what there is 


//its going to go as following.
//we will only hold the int value in the combat part.
//everytime we add an item we will update that int. everytime we spend ammo