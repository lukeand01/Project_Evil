using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryClass 
{
    //its a basic version of an inventory.
    //it will serve for handling the logic.
    //it is going to use itemdata and itmclass always because norammly thats wwhat i use.

    public List<ItemClass> inventoryList { get; private set; }

    public InventoryClass(int quantity)
    {
        inventoryList = new();
        CreateInventorySlot(quantity);
    }

    public void CreateInventorySlot(int quantity)
    {           
        for (int i = 0; i < quantity; i++)
        {
            inventoryList.Add(new ItemClass(null, 0));
        }

    }

    public int TryToAddItem(ItemClass item)
    {

        List<ItemClass> stackableList = GetStackableList(item.data);
        int brake = 0;
        while(item.quantity > 0)
        {
            brake++;
            if (brake > 1000) break;

            if (stackableList.Count > 0)
            {
                StackItem(item, stackableList);
                continue;
            }

            int freeSlot = GetNextFreeSlot();

            if(freeSlot != -1)
            {
                CreateItem(freeSlot, item, stackableList);
                continue;
            }

            return item.quantity;
        }


        return 0;
    }

    void StackItem(ItemClass item, List<ItemClass> rightList)
    {
        int diff = rightList[0].GetAmountToStack();
        float brake = 0;

        while (item.quantity > 0 && diff > 0)
        {
            brake++;
            if (brake > 1000)
            {
                Debug.Log("broke in stackable");
                break;
            }

            rightList[0].IncreaseQuantity();
            diff = rightList[0].GetAmountToStack();
            item.DecreaseQuantity();


            if (diff == 0)
            {
                rightList.RemoveAt(0);
            }
        }


    }

    void CreateItem(int index, ItemClass item, List<ItemClass> list)
    {
        inventoryList[index].ReceiveNewData(item.data);
        item.DecreaseQuantity();
        if (item.quantity > 0) list.Add(inventoryList[index]);
    }

    List<ItemClass> GetStackableList(ItemData data)
    {
        List<ItemClass> newList = new();

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].data == data && inventoryList[i].quantity < data.stackLimit) newList.Add(inventoryList[i]);
        }

        return newList;
    }

    int GetNextFreeSlot()
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].data == null) return i;
        }
        return -1;

    }
}
