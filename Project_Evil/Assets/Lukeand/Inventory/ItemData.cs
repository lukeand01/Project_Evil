using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public string itemName;
    [TextArea]public string itemDescription;
    public int stackLimit;
    public Sprite itemSprite;
    public ItemType itemType;

    [Separator("USABLE")]
    public float useTime;

    public virtual void UseItem(ItemClass item)
    {
        //guns are equipped. potions are used. 
        //

    }


    public bool IsUsable()
    {
        return itemType == ItemType.Consumable || itemType == ItemType.Gun || itemType == ItemType.Tool;
    }


    public virtual ItemEquipmentData GetEquipment() => null;

    public virtual ItemGunData GetGun() => null;

    public virtual ItemUsableData GetUsable() => null; //usable 

    public virtual ItemResourceData GetResource() => null;

    public virtual ItemAmmoData GetAmmo() => null;

    public virtual ItemToolData GetTool() => null;

}

public enum ItemType
{
    Consumable, //can be used
    Gun, //can be equipped
    Ammo, //nothing
    Tool, //can be equipped
    Equipment, //can be equipped
    Resource //cacn be crafted
}