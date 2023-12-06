using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item / Usable")]
public class ItemUsableData : ItemData
{


   

    public override ItemUsableData GetUsable() => this;
    
}
