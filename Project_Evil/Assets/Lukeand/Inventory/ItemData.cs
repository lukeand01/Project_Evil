using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public string itemName;
    public int stackLimit;
    public Sprite itemSprite;


    public virtual ItemGunData GetGun() => null;
}
