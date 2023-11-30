using MyBox;
using MyBox.EditorTools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUnit : ButtonBase
{
    ItemClass item;


    [SerializeField] Image icon;
    [SerializeField] GameObject empty;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI quantityText;

   public void SetUp(ItemClass item)
    {
        this.item = item;
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
        



    }
}
