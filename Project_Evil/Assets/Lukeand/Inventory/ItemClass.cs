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

    public GunClass gun;

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
        UpdateAnyLinkedUI();
    }


    public void ControlEquip(bool choice)
    {
        IsEquipped = choice;
        if(inventoryUnit != null) inventoryUnit.UpdateEquippedUI();
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

    public bool IsInteractable()
    {
        if (data == null) return false;
        if (data.GetGun() != null) return true;


        return false;
    }
}
