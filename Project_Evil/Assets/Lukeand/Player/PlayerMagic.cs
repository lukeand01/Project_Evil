using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    [SerializeField] int scrollSlotAllowed;

    List<ScrollClass> scrollList = new();

    private void Start()
    {
        for (int i = 0; i < scrollSlotAllowed; i++) 
        {
            ScrollClass scroll = new ScrollClass(null, i);
            UIHolder.instance.uiScroll.CreateScrollUnit(scroll);
            scrollList.Add(scroll);            
        }
    }

    private void FixedUpdate()
    {
        foreach (var item in scrollList)
        {
            item.DealWithCooldown();
        }
    }



    //and if you ever stop hovering this fella then we call it off.
    public void ReceiveNewScroll(ItemScrollData data, int index, ItemClass inventoryItemRef)
    {

        for (int i = 0; i < scrollList.Count; i++)
        {
            if(i != index && scrollList[i].data == data)
            {
                scrollList[i].ReceiveNewScroll(null);
            }
        }

        scrollList[index].ReceiveNewScroll(data);
        scrollList[index].SetInventoryRef(inventoryItemRef);
    }

    public void ControlUnusedScrollSlots(bool isInventoryOn)
    {
        //sccroll slots that are not beiong used they are invisible in gameplay and visible in inventory.

      
        foreach (ScrollClass c in scrollList)
        {
            c.ControlHolder(isInventoryOn);
        }
    }


    public void UseScroll(int scrollIndex)
    {
        scrollList[scrollIndex].Use();
    }
}
