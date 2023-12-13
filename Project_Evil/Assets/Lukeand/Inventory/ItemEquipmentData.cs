using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipmentData : ItemData
{
    //this can be equipped as well.

    private void Awake()
    {
        itemType = ItemType.Equipment;
    }


    public override ItemEquipmentData GetEquipment() => this;
    
}
