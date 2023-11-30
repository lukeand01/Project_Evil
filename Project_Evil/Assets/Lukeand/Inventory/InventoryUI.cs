using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    GameObject holder;
    [SerializeField] InventoryUnit unitTemplate;
    [SerializeField] Transform container;

    public void Open()
    {
        holder.SetActive(true);
    }
    public void Close()
    {
        holder.SetActive(false);
    }

    public void UpdateUnitList(List<ItemClass> itemList)
    {
        foreach (var item in itemList)
        {
            CreateUnit(item);
        }
    }
    public void CreateUnit(ItemClass item)
    {     
       InventoryUnit newObject = Instantiate(unitTemplate, Vector3.zero, Quaternion.identity);
       newObject.transform.parent = container;
       newObject.SetUp(item);    
    }

}
