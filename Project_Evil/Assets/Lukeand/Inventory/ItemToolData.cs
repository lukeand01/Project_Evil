using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item / Tool")]
public class ItemToolData : ItemData
{
    [Separator("TOOL")]
    public bool isSword; //either sword or shield.

    private void Awake()
    {
        itemType = ItemType.Tool;
    }

    public override void UseItem(ItemClass item)
    {
        base.UseItem(item);

        if (item.tool == null)
        {
            Debug.Log("no tool");
            return;
        }

        if(isSword)
        {
            PlayerHandler.Instance.combat.ChangeSword(item);
        }
        else
        {
            PlayerHandler.Instance.combat.ChangeShield(item);
        }
        
    }


    public override ItemToolData GetTool() => this;
    

}
