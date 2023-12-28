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

    public virtual ItemScrollData GetScroll() => null;

}

public enum ItemType
{
    Consumable = 0, //can be used and remove liked potions.
    Gun = 1, //can be equipped
    Ammo = 2, //nothing
    Tool = 3, //can be equipped
    Equipment = 4, //can be equipped
    Resource = 5, //cacn be crafted
    ToSell =  6, //cacn only be sold but it has a bettter price
    Scroll = 7 //it can be assigned to slot for magic.
}