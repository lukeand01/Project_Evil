using MyBox;
using MyBox.EditorTools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUnit : ButtonBase
{
    public ItemClass item { get; private set; }
    InventoryUI handler;

    [SerializeField] Image icon;
    [SerializeField] GameObject empty;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI quantityText;

   public void SetUp(ItemClass item, InventoryUI handler)
    {
        this.item = item;
        this.handler = handler;

        item.SetUpInventoryUnit(this);
    }

    public void UpdateUI()
    {
        bool hasItem = ItemExists();
        empty.SetActive(!hasItem);
        if (!hasItem) return;

        icon.sprite = item.data.itemSprite;
        nameText.text = item.data.itemName;
        quantityText.text = item.quantity.ToString();

    }

    bool ItemExists()
    {
        if (item == null) return false;
        if (item.data == null) return false;
        return true;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);


        if (!ItemExists()) return;

       if(eventData.button == PointerEventData.InputButton.Left)
       {
            //this starts the dragging.
            Debug.Log("can start dragging");
            handler.draggableHandler.StartDragging(this);
       }

        
       if(eventData.button == PointerEventData.InputButton.Right && item.data.IsUsable())
       {
            //this starts using the item here.
            Debug.Log("start using the item");
       }

    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!ItemExists()) return;

        base.OnPointerEnter(eventData);
        handler.draggableHandler.Hover(this);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!ItemExists()) return;

        base.OnPointerExit(eventData);
        handler.draggableHandler.StopHover();
    }
}
