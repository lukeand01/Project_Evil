using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    GameObject holder;
    [SerializeField] InventoryUnit unitTemplate;
    [SerializeField] Transform container;


    float total;
    float current;
    float chargingModifier;

    float currentUseCooldown;
    float totalUseCooldown;

    public InventoryDraggableHandler draggableHandler { get; private set; }
    public InventoryDescriptionUI descriptionUI { get; private set; }
    public InventoryCraftUI craftUI { get; private set; }   



    bool isStuckInHoldMouse;

    [Separator("Guide Texts")]
    [SerializeField] TextMeshProUGUI m1Text;
    [SerializeField] TextMeshProUGUI m2Text;

    private void Awake()
    {
        
        currentUseCooldown = totalUseCooldown;
        holder = transform.GetChild(0).gameObject;


        draggableHandler = GetComponent<InventoryDraggableHandler>();
        descriptionUI = GetComponent<InventoryDescriptionUI>(); 
        craftUI = GetComponent<InventoryCraftUI>(); 
    }

    private void Start()
    {
        chargingModifier = 0.5f * Time.timeScale * Time.deltaTime ;
        total = 5 * Time.timeScale;
        totalUseCooldown = 0.5f * Time.timeScale;
    }

    private void Update()
    {

        if (GameHandler.instance == null) return;

        if(draggableHandler.isDragging)
        {
            m1Text.text = "M1: Release to swap";
            m2Text.text = "M2: Stack on item";
        }
        else
        {
            m1Text.text = "M1: Drag item";
            m2Text.text = "M2: Use Item";
        }


        HandleUseLogic();
        
        
    }

    void HandleUseLogic()
    {
        InventoryUnit currentUsingItem = draggableHandler.GetItemToBeUsed();

        if (!Input.GetMouseButton(1) && isStuckInHoldMouse)
        {
            isStuckInHoldMouse = false;
        }


        if (currentUseCooldown < totalUseCooldown)
        {
            currentUseCooldown += chargingModifier;
        }



        if (currentUsingItem != null)
        {

            if (!currentUsingItem.item.IsInteractable())
            {
                return;
            }


            if (currentUseCooldown >= totalUseCooldown && !isStuckInHoldMouse)
            {
                if (Input.GetMouseButton(1))
                {
                    UsingItem(currentUsingItem);
                }
                else
                {
                    StopUsingItem(currentUsingItem);
                }
            }
            else
            {
                StopUsingItem(currentUsingItem);
            }
        }
    }

    void UsingItem(InventoryUnit refUnit)
    {

        current += chargingModifier;

        refUnit.UpdateCharge(current, total);

        if(current > total) 
        {
            //can use the item.
            UseItem(refUnit);    
        }
    }

    void UseItem(InventoryUnit refUnit)
    {
        refUnit.item.UseItem();

        isStuckInHoldMouse = true;
        currentUseCooldown = 0;
        //current = 0;
        refUnit.AnimateItemUse();
    }


    void StopUsingItem(InventoryUnit refUnit)
    {

        if (refUnit == null)
        {
            current = 0;
        }

        if (current > 0)
        {
            current -= chargingModifier;
            if (refUnit != null) refUnit.UpdateCharge(current, total);
        }
        
    }

    public void ControlUI()
    {
        if(holder.activeInHierarchy)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

     void Open()
    {
        holder.SetActive(true);
        current = 0;
        currentUseCooldown = totalUseCooldown;


        if (GameHandler.instance != null)
        {
            GameHandler.instance.PauseGameTime();
            PlayerHandler.Instance.block.AddBlock("Inventory", BlockClass.BlockType.Partial);
        }
        
    }
     void Close()
    {
        holder.SetActive(false);
        if (GameHandler.instance != null)
        {
            GameHandler.instance.ResumeGameTime();
            PlayerHandler.Instance.block.RemoveBlock("Inventory");
        }
    }

    public void UpdateUnitList(List<ItemClass> itemList)
    {

        ClearUI(container);
        foreach (var item in itemList)
        {
            CreateUnit(item);
        }
    }

    void ClearUI(Transform target)
    {
        for (int i = 0; i < target.transform.childCount; i++)
        {
            Destroy(target.transform.GetChild(i).gameObject);
        }
    }

    public void CreateUnit(ItemClass item)
    {     
       InventoryUnit newObject = Instantiate(unitTemplate, Vector3.zero, Quaternion.identity);
       newObject.transform.parent = container;
       newObject.SetUp(item, this);    
    }
    
   
    public void Hover(InventoryUnit unit)
    {
        draggableHandler.Hover(unit);
        descriptionUI.Describe(unit);
    }

    public void StopHover()
    {
        current = 0;
        draggableHandler.StopHover();
        descriptionUI.StopDescribe();
    }

    
}
