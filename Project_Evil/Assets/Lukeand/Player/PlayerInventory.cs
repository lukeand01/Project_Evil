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
        UIHolder.instance.inventory.UpdateUnitList(inventory.inventoryList);
    }

    public int TryToAddItem(ItemClass item) => inventory.TryToAddItem(item);

    
}
