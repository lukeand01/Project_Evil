using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReferenceHolder : MonoBehaviour
{
    [Separator("Item Type Interactable Icon")]
    [SerializeField] List<Sprite> itemInteractableIconList = new();

    public Sprite GetItemInteractableIcon(ItemType itemType)
    {
        return itemInteractableIconList[(int)itemType];
    }

}
