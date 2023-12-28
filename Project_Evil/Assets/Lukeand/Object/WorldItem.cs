using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldItem : MonoBehaviour, IInteractable
{
    [SerializeField] ItemClass item;
    [SerializeField] GameObject uiHolder;
    [SerializeField] TextMeshProUGUI inputText;
    [SerializeField] Image itemTypeImage;
    [SerializeField] TextMeshProUGUI itemNameText;

    string id;

    bool cannotInteract;

    private void Awake()
    {
        id = Guid.NewGuid().ToString();

        

    }

    private void Start()
    {
        if (item != null)
        {
            itemNameText.text = item.data.name;
            itemTypeImage.sprite = GameHandler.instance.uiRef.GetItemInteractableIcon(item.data.itemType);
        }
        else
        {
            itemNameText.text = "Unknown Item";
            itemTypeImage.gameObject.SetActive(false);
        }
    }

    public void CallUI(bool choice)
    {
        if(choice == true)
        {
            inputText.text = PlayerHandler.Instance.playerController.GetInputStringValue(KeyType.Interact);
        }

        if (cannotInteract) return;

        uiHolder.SetActive(choice);
    }

    public string GetID()
    {
        return gameObject.name;
    }

    public void Interact()
    {
        PlayerHandler.Instance.playerInventory.TryToAddItem(item);
        cannotInteract = true;
        Destroy(gameObject);
    }

    public bool IsInteractable()
    {
        return !cannotInteract;
    }
}
