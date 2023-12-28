using MyBox;
using MyBox.EditorTools;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUnit : ButtonBase, IDraggable, IDragHandler
{
    public ItemClass item;
    InventoryUI handler;

    [SerializeField] Image icon;
    [SerializeField] GameObject empty;
    [SerializeField] GameObject selected; 
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI quantityText;
    [SerializeField] TextMeshProUGUI equippedText;



    [SerializeField] Image useAnimationImage;
    [SerializeField] Image chargeImage;
    [SerializeField] GameObject hoverImage;
    [SerializeField] bool notInteractable;

    Vector3 originalScale;

    public string id {  get; private set; }

    float timeModifier;

    private void Awake()
    {
        originalScale = transform.localScale - new Vector3(0.3f, 0.3f,0);
        id = Guid.NewGuid().ToString();
    }

    private void Start()
    {
        timeModifier = GameHandler.instance.timeModifier;
    }

    public void SetUp(ItemClass item, InventoryUI handler)
    {
        item.SetUpInventoryUnit(this);
        this.item = item;
        this.handler = handler;
        UpdateUI();
    }

    public void ReceiveNewItem(ItemClass item)
    {
        item.SetUpInventoryUnit(this);
        this.item = item;
        UpdateUI();
    }


    public void UpdateUI(string debug = "")
    {

        if(debug != "")
        {
            Debug.Log("item " + item.quantity);
        }

        bool hasItem = ItemExists();
        empty.SetActive(!hasItem);
        chargeImage.gameObject.SetActive(hasItem);

        
        if (!hasItem) return;
        icon.sprite = item.data.itemSprite;
        nameText.text = item.data.itemName;
        quantityText.text = item.quantity.ToString();
        UpdateEquippedUI();


        
    }

    public void UpdateEquippedUI()
    {

        equippedText.gameObject.SetActive(item.IsEquipped);
    }


    public bool ItemExists()
    {
        if (item == null) return false;
        if (item.data == null) return false;
        return true;
    }

   
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (notInteractable) return ;
        base.OnPointerEnter(eventData);      

        handler.Hover(this);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        if (notInteractable) return;
        if (!ItemExists()) return;
        base.OnPointerExit(eventData);
        handler.StopHover();
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    private void OnDisable()
    {
        //return to normal state.
        useAnimationImage.gameObject.SetActive(false);
    }

    #region USE ANIMATION 
    public void AnimateItemUse()
    {
        //it grows a bit and reduces back.
        StopAllCoroutines();
        StartCoroutine(GrowProcess());
    }

    IEnumerator GrowProcess()
    {

        useAnimationImage.transform.localScale = new Vector3(0.05f, 0.05f, 0);
        useAnimationImage.gameObject.SetActive(true);
        var alpha = useAnimationImage.color;
        alpha.a = 0.15f;
        useAnimationImage.color = alpha;

        while(originalScale.x > useAnimationImage.transform.localScale.x)
        {
            useAnimationImage.transform.localScale += new Vector3(0.003f, 0.003f, 0);
            yield return new WaitForSeconds(0.0002f);
        }

        StartCoroutine(FadeOutProcess());
    }
    IEnumerator FadeOutProcess()
    {
        while(useAnimationImage.color.a > 0)
        {
            useAnimationImage.color -= new Color(0, 0, 0, 0.001f);
            yield return new WaitForSeconds(0.0005f);
        }
    }

    #endregion

    public void UpdateCharge(float current, float total)
    {
        chargeImage.fillAmount = current / total;
    }

    public void ControlSelected(bool showSelected)
    {
        selected.gameObject.SetActive(showSelected);
    }

    public void ControlHover(bool choice)
    {
        hoverImage.gameObject.SetActive(choice);
    }

}
