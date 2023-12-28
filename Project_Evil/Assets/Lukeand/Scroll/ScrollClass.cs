using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollClass 
{
    public ItemScrollData data {  get; private set; }
    ScrollUIUnit scrollUnit;
    ItemClass inventoryItemRef;
    //when nwe assign a new data to this we should also assing something related to the inventory.


    public float currentCooldown;
    float totalCooldown;

    public int index { get; private set; }

    public ScrollClass(ItemScrollData data, int index)
    {
        this.data = data;
        this.index = index;

        if (data == null) return;

        totalCooldown = data.cooldown;
        currentCooldown = 0;
    }

    public void SetUI(ScrollUIUnit scrollUnit)
    {
        this.scrollUnit = scrollUnit;
        scrollUnit.UpdateCooldown(currentCooldown, totalCooldown);
    }

    void EmptyScroll()
    {
        data = null;
        inventoryItemRef = null;
        scrollUnit.UpdateUI();
    }

    public void Use()
    {
        //check if the ccooldown is good.


        if(currentCooldown > 0)
        {
            return;
        }


        if(inventoryItemRef != null)
        {
            inventoryItemRef.DecreaseQuantity();
            scrollUnit.UpdateResourceQuantity(inventoryItemRef.quantity);

            if (inventoryItemRef.quantity <= 0)
            {
                EmptyScroll();
            }        
        }



        currentCooldown = totalCooldown;


    }

    public void DealWithCooldown()
    {
        if (data == null)
        {

            return;
        }


        if (currentCooldown > 0)
        {
            currentCooldown -= 0.02f;
            scrollUnit.UpdateCooldown(currentCooldown, totalCooldown);
        }
    }


    public void ReceiveNewScroll(ItemScrollData data)
    {
        this.data = data;
        if (data == null)
        {
            inventoryItemRef = null;
        }
        else
        {
            totalCooldown = data.cooldown;
        }
        
        scrollUnit.UpdateUI();

        

    }
    public  void SetInventoryRef(ItemClass item)
    {
        inventoryItemRef = item;
        scrollUnit.UpdateResourceQuantity(inventoryItemRef.quantity);
    }

    public void ControlHolder(bool isInventoryOn)
    {
        if(isInventoryOn)
        {
            scrollUnit.ControlHolder(true);
        }
        else
        {
            if (data != null) scrollUnit.ControlHolder(true);
            else scrollUnit.ControlHolder(false);
            
        }
    }

}
