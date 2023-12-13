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

    public GunClass gun { get; private set; } = null;
    public ToolClass tool { get; private set; } = null;

    public bool IsEquipped { get; private set; } //if this is ever case then we show something.


    public ItemClass(ItemData data, int quantity)
    {
        this.data = data;
        this.quantity = quantity;
    }

    public void ReceiveNewData(ItemData data)
    {
        this.data = data;
        this.quantity = 1;

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
            Debug.Log("did");
            PlayerHandler.Instance.combat.EquipIfEmpty(this);
        }
        else
        {
            Debug.Log("couldnt do this");
        }
        
        UpdateAnyLinkedUI();
    }


    public void ControlEquip(bool choice)
    {
        IsEquipped = choice;
        if (inventoryUnit != null) inventoryUnit.UpdateEquippedUI();
        else Debug.Log("this was the problem");
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

        if(this.quantity <= 0)
        {

            data = null;
            this.quantity = 0;
            
        }

        UpdateAnyLinkedUI();

        if (debug != "")
        {
            Debug.Log("quantity " + quantity + " " + this.quantity);
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

    

    public void CreateGun()
    {

    }
   
    public void CreateTool()
    {

    }

}

