using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item / Ammo")]
public class ItemAmmoData : ItemData
{
    [Separator("Ammo")]
    [SerializeField] AmmoType ammoType;

    public override ItemAmmoData GetAmmo() => this;
    
}

public enum AmmoType
{
    Pistol,
    Shotgun,
    
}