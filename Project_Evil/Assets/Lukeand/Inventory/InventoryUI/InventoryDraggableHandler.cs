using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDraggableHandler : MonoBehaviour
{
    //this handles th information.
    //


    [SerializeField] InventoryUnit actualDraggingUnit;

    InventoryUnit draggingItem;
    InventoryUnit hoverItem;

    

    public bool isDragging {  get; private set; }

    private void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        

        if (isDragging)
        {

            actualDraggingUnit.transform.position = Input.mousePosition;
            actualDraggingUnit.gameObject.SetActive(true);
            if (!Input.GetMouseButton(0))
            {
                StopDragging();
            }

            if (Input.GetMouseButtonDown(1))
            {
                TransferItem();
            }

        }
        else
        {
            //if not dragging
            if(hoverItem != null )
            {
                if(hoverItem.ItemExists() && Input.GetMouseButton(0))
                {
                    StartDragging(hoverItem);
                }
            }

            actualDraggingUnit.gameObject.SetActive(false);
        }
    }

    public void StartDragging(InventoryUnit item)
    {
        isDragging = true;
        draggingItem = item;

        actualDraggingUnit.gameObject.SetActive(true);
        actualDraggingUnit.SetUp(item.item, null);
    }
    public void StopDragging()
    {
        //in here wee check evrything we can give to the hovering fella.

        if(hoverItem != null)
        {
            if(draggingItem.id != hoverItem.id)
            {
                Swap();
            }
            


          
        }
        

        isDragging = false;
        draggingItem = null;
        actualDraggingUnit.gameObject.SetActive(false);
    }

    public void Hover(InventoryUnit item)
    {
        hoverItem = item;
        hoverItem.ControlHover(true);

        
    }

    public void StopHover()
    {
        if(hoverItem != null)
        {
            hoverItem.UpdateCharge(0, 1);
            hoverItem.ControlHover(false);
        }
        hoverItem = null;
    }


    void TransferItem(int quantity = 1)
    {
        //we give the current
        //to the hover
        //

        Debug.Log("transfer item");
        if(!isDragging)
        {
            Debug.Log("cannot trasnfer without be dragging");
            return;
        }


        //if its the same item then we look at 
        if (!IsSameItem())
        {
            return;
        }
        

    }

    void Swap()
    {
        
        if(IsSameItem())
        {
            //we stack as muhc as we can.
            int quantityToGive = hoverItem.item.GetAmountToStackClamped(draggingItem.item.quantity);
      
            draggingItem.item.DecreaseQuantity(quantityToGive);
            draggingItem.UpdateUI();
            hoverItem.item.IncreaseQuantity(quantityToGive);
            hoverItem.UpdateUI();
        }
        else
        {
            //we simple change positions.

            ItemClass dragItem = draggingItem.item;
            ItemClass hoverItem = this.hoverItem.item;           

            this.hoverItem.ReceiveNewItem(dragItem);
            draggingItem.ReceiveNewItem(hoverItem);
        }
    }


    bool IsSameItem()
    {
        if (draggingItem == null) return false;
        if (hoverItem == null) return false;

        if (!draggingItem.ItemExists())
        {
            return false;
        }
        if (!hoverItem.ItemExists())
        {
            return false;
        }

        if (draggingItem.item.data != hoverItem.item.data)
        {
            return false;
        }

        return true;
    }


    public InventoryUnit GetItemToBeUsed()
    {
        if (draggingItem != null) return null;

        if (hoverItem == null) return null;

        if (!hoverItem.ItemExists()) return null;

        return hoverItem;
    }
}
