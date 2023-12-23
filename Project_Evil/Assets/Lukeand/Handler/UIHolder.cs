using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHolder : MonoBehaviour
{
    public static UIHolder instance;

    public InventoryUI uiInventory;
    public GunUI uiGun;
    public PlayerResourceUI uiResource;

    private void Awake()
    {
        instance = this;
    }
}
