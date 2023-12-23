using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item / Ammo")]
public class ItemAmmoData : ItemData
{
    [Separator("Ammo")]
    public AmmoType ammoType;


    private void Awake()
    {
        itemType = ItemType.Ammo;
    }

    public override ItemAmmoData GetAmmo() => this;
    
}

public enum AmmoType
{
    Pistol,
    Shotgun,
    
}