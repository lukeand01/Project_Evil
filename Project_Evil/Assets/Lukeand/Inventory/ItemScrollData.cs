using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item / Scroll")]
public class ItemScrollData : ItemData
{

    //each scroll will ref this and create its new stuff
    [Separator("SCROLL")]
    public float curseAmount;
    public float cooldown;


    private void Awake()
    {
        itemType = ItemType.Scroll;
    }

    public override void UseItem(ItemClass item)
    {
        base.UseItem(item);

        //this starts a section for selectng the spell.
        UIHolder.instance.uiInventory.SelectNewScroll(this, item);
        //also pass the id here.
    }

    public virtual void UseScroll()
    {
        Debug.Log("base use scroll");



    }

    public override ItemScrollData GetScroll() => this;
  

}
