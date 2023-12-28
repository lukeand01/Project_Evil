using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHolder : MonoBehaviour
{
    public static UIHolder instance;

    public InventoryUI uiInventory;
    public GunUI uiGun;
    public PlayerResourceUI uiResource;
    public ScrollUI uiScroll;

    private void Awake()
    {
        instance = this;
    }
}
