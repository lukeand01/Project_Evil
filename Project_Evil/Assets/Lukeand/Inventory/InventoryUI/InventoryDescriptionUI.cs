using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDescriptionUI : MonoBehaviour
{
    //how to do the crafting?
    //click on button and it will open the menu with its only options.

    [SerializeField] GameObject descriptionHolder;
    [SerializeField] Image portrait;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;

    public void Open()
    {
        descriptionHolder.SetActive(true);
    }
    public void Close()
    {
        descriptionHolder?.SetActive(false);
    }

    public void Describe(InventoryUnit itemUnit)
    {
        bool success = itemUnit.ItemExists();

        descriptionHolder.SetActive(success);

        if (!success)
        {
            return;
        }

        Open();
        ItemClass item = itemUnit.item;

        
    }

    public void StopDescribe()
    {
        Close();
    }

    void DescribeResource()
    {
        
    }
    void DescribeGun()
    {

    }
}
