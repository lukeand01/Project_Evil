using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

[System.Serializable]
public class ItemClass 
{
    public ItemData data;
    public int quantity;

    public GunClass gun;

    public ItemClass(ItemData data, int quantity)
    {
        this.data = data;
        this.quantity = quantity;
    }

    public void ReceiveNewData(ItemData data)
    {
        this.data = data;
        UpdateAnyLinkedUI();
    }

    #region QUANTITY
    public void IncreaseQuantity(int quantity = 1)
    {
        this.quantity += quantity;
        UpdateAnyLinkedUI();
    }
    public void DecreaseQuantity(int quantity = 1)
    {
        this.quantity -= quantity;
        UpdateAnyLinkedUI();
    }
    public int GetAmountToStack()
    {
        return data.stackLimit - quantity;
    }
    #endregion


    #region UI

    void UpdateAnyLinkedUI()
    {
        if (inventoryUnit != null) inventoryUnit.UpdateUI();
    }

    InventoryUnit inventoryUnit;
    public void SetUpInventoryUnit(InventoryUnit inventoryUnit)
    {
        this.inventoryUnit = inventoryUnit;
    }


    #endregion
}
