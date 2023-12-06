using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    GameObject holder;
    [SerializeField] InventoryUnit unitTemplate;
    [SerializeField] Transform container;
    [SerializeField] Image chargingBar;

    InventoryUnit currentUsingItem;

    float total;
    float current;
    float chargingModifier;


    public InventoryDraggableHandler draggableHandler { get; private set; }

    private void Awake()
    {
        chargingModifier = 10000;
        holder = transform.GetChild(0).gameObject;
        draggableHandler = GetComponent<InventoryDraggableHandler>();
    }

    private void FixedUpdate()
    {
        if(currentUsingItem != null)
        {
            if (Input.GetMouseButtonUp(1))
            {
                currentUsingItem = null;
                Debug.Log("stop this");
                return;
            }
            current += chargingModifier;
            chargingBar.fillAmount = current / total;
        }
        else
        {
            if(chargingBar.fillAmount > 0)
            {
                current -= chargingModifier;
                chargingBar.fillAmount = current / total;
            }
        }
    }

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
       newObject.SetUp(item, this);    
    }
    
    public void StartUseItem(InventoryUnit currentUnit)
    {
        currentUsingItem = currentUnit;

    }
    public void StopUseItem()
    {
        currentUsingItem = null;

    }

}
