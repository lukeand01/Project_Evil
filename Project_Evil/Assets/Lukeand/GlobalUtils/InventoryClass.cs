using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using static UnityEditor.Progress;

public class InventoryClass 
{
    //its a basic version of an inventory.
    //it will serve for handling the logic.
    //it is going to use itemdata and itmclass always because norammly thats wwhat i use.

    public List<ItemClass> inventoryList { get; private set; }

    PlayerInventory handler;


    public InventoryClass(PlayerInventory handler, int quantity)
    {
        this.handler = handler;
        inventoryList = new();
        CreateInventorySlot(quantity);
        CreateAmmoDictionary();
    }

    public void CreateInventorySlot(int quantity)
    {


        for (int i = 0; i < quantity; i++)
        {
            ItemClass newItem = new ItemClass(null, 0);
            newItem.ReceiveNewSlotID(inventoryList.Count);
            inventoryList.Add(newItem);
        }

    }

    public int TryToAddItem(ItemClass item)
    {

        

        List<ItemClass> stackableList = GetStackableList(item.data);
        int brake = 0;
        while(item.quantity > 0)
        {
            brake++;
            if (brake > 1000)
            {
                Debug.Log("broke here in first");
                break;
            }

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

        if(item.quantity <= 0)
        {
            Debug.Log("too little quantity");
        }
        if(diff <= 0)
        {
            rightList.RemoveAt(0);
        }

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

        if(item.data == null)
        {
            return;
        }

        if(item.data.itemType == ItemType.Ammo)
        {
            AddAmmo(inventoryList[index]);
        }
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


    #region AMMO
    public Dictionary<AmmoType, List<ItemClass>> ammoDictionary { get; private set; } = new();
    
    void CreateAmmoDictionary()
    {
        ammoDictionary.Add(AmmoType.Pistol, new List<ItemClass>());
        ammoDictionary.Add(AmmoType.Shotgun, new List<ItemClass>());
    }

    public int GetAmmo(AmmoType ammo)
    {
        int amount = 0;



        List<ItemClass> newList = ammoDictionary[ammo];

        Debug.Log("his was the list found " + newList.Count);

        for (int i = 0; i < newList.Count; i++)
        {
            if (newList[i].quantity <= 0)
            {
                Debug.Log("this was called 1");
                newList.RemoveAt(i);
                i--;
                continue;
            }

            amount += newList[i].quantity;

        }

        return amount;
    }


    void AddAmmo(ItemClass item)
    {

       
        ItemAmmoData ammoData = item.data.GetAmmo();

        if (ammoData == null) return;

        AmmoType ammo = ammoData.ammoType;


        ammoDictionary[ammo].Add(item);
    }

    public void SpendAmmo(AmmoType ammo)
    {
        List<ItemClass> newList = ammoDictionary[ammo];

        for (int i = 0; i < newList.Count; i++)
        {
            if (newList[i].quantity <= 0)
            {
                //delete this fella from the list
                Debug.Log("this was called 2");
                newList.RemoveAt(i);
            }
            else
            {
                newList[i].DecreaseQuantity();
                if (newList[i].quantity <= 0)
                {
                    newList.RemoveAt(i);    
                }

                return;
            }

        }


    }


    #endregion


}
