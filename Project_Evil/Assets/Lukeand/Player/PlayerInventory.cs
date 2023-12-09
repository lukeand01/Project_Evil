using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int initialSlotQuantity;
    InventoryClass inventory;
    PlayerHandler handler;
    [SerializeField] List<ItemClass> initialItem;

    private void Awake()
    {
        inventory = new InventoryClass(initialSlotQuantity);
        handler = GetComponent<PlayerHandler>();
    }

    private void Start()
    {


        foreach (ItemClass item in initialItem)
        {

            for (int i = 0; i < item.quantity; i++)
            {
                int success = TryToAddItem(item);
                if (success > 0)
                {
                    Debug.Log("there was something wrong here");
                }
            }
           
            
        }

        UIHolder.instance.inventory.UpdateUnitList(inventory.inventoryList);
    }



    public int TryToAddItem(ItemClass item)
    {
       return inventory.TryToAddItem(item);
    }

    
}
