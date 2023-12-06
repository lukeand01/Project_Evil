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


    bool isDragging;

    private void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        actualDraggingUnit.gameObject.SetActive(isDragging);

        if (isDragging)
        {
            actualDraggingUnit.transform.position = Input.mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("stopped dragging");
                StopDragging();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("give one to this place");
            }

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
            Debug.Log("there are calculations to be made");

        }
        else
        {
            Debug.Log("then we dont care");


        }

        isDragging = false;
        draggingItem = null;
        actualDraggingUnit.gameObject.SetActive(false);
    }

    public void Hover(InventoryUnit item)
    {
        Debug.Log("hover");
        hoverItem = item;
    }

    public void StopHover()
    {
        Debug.Log("stop hover");
        hoverItem = null;
    }

}
