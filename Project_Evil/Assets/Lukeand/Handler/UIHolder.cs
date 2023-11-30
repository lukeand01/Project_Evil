using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHolder : MonoBehaviour
{
    public static UIHolder instance;

    public InventoryUI inventory;

    private void Awake()
    {
        instance = this;
    }
}
