using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField] int initialCredit;
    int currentCredits;
    [SerializeField] int initialSlotQuantity;
    public InventoryClass inventory;
    PlayerHandler handler;
    [SerializeField] List<ItemClass> initialItem;
    [SerializeField] List<ItemClass> DEBUGshowItemList;

    

    private void Start()
    {

        inventory = new InventoryClass(this, initialSlotQuantity);
        handler = GetComponent<PlayerHandler>();

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

        UIHolder.instance.uiInventory.UpdateUnitList(inventory.inventoryList);

        DEBUGshowItemList = inventory.inventoryList;

        //we create all the lists.

    }

   

    //
    public int TryToAddItem(ItemClass item)
    {

        if (item.data == null)
        {
            return 0;
        }

        bool shouldUpdateAmmo = item.data.itemType == ItemType.Ammo;

       
        int remaining = inventory.TryToAddItem(item);

        if(shouldUpdateAmmo)
        {
            handler.playerCombat.RequestAmmoUpdate();
        }

        
        return remaining;
    }



    #region CHECK FOR TOOL 
    public void CheckForAnotherSwordToEquip()
    {
        //we send the first.
        foreach (ItemClass item in inventory.inventoryList)
        {
            ItemToolData data = item.data.GetTool();
            if (data == null) continue;
            if (data.isSword)
            {
                handler.playerCombat.ChangeSword(item);
                return;
            }


        }
    }

    public void CheckForAnotherShieldToEquip()
    {
        //we send the first.
        foreach (ItemClass item in inventory.inventoryList)
        {
            ItemToolData data = item.data.GetTool();
            if (data == null) continue;
            if (!data.isSword)
            {
                handler.playerCombat.ChangeShield(item);
                return;
            }
            


        }
    }

    #endregion


    public int GetAmmo(AmmoType ammo)
    {
        int amount = 0;


        List<ItemClass> newList = inventory.ammoDictionary[ammo];

        for (int i = 0; i < newList.Count; i++)
        {
            if (newList[i].quantity <= 0)
            {
                newList.RemoveAt(i);
                i--;
                continue;
            }

            amount += newList[i].quantity;

        }

        return amount;
    }

    //this is just for reloading
    public void SpendAmmo(AmmoType ammo, int quantity = 1)
    {
        for (int i = 0; i < quantity; i++)
        {
            inventory.SpendAmmo(ammo);
        }
        
    }


    



    [SerializeField] ItemClass ammoPistol;

    [ContextMenu("Add Pistol Ammo")]
    public void DEBUGAddPistolAmmo()
    {
        TryToAddItem(new ItemClass(ammoPistol.data, ammoPistol.quantity));
    }

    

}

//alright to fix this will have all operation done here but the combat will have an copy of this fella.


//things are updated after reloading.
//and after cchanging an item.


//