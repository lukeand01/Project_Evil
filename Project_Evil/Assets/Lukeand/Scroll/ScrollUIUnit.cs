using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScrollUIUnit : MonoBehaviour
{
    ScrollClass scroll;
    GameObject holder;
    [SerializeField] Image portrait;
    [SerializeField] Image cooldown;
    [SerializeField] TextMeshProUGUI curseText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject empty;
    [SerializeField] TextMeshProUGUI indexText;
    [SerializeField] TextMeshProUGUI resourceText;
    Vector3 originalIndexTextPos;

    //maybe a cool effet weould to be spawn the new version a bit above. fade it in and oerlay with the ccurrent image.
    [SerializeField] float debugCooldown;

    private void Update()
    {
        debugCooldown = scroll.currentCooldown;
    }


    public void SetUp(ScrollClass scroll)
    {
        holder = transform.GetChild(0).gameObject;
        this.scroll = scroll;
        scroll.SetUI(this); 
        UpdateUI();



        originalIndexTextPos = indexText.transform.localPosition;
        indexText.text = (scroll.index + 1).ToString();
        indexText.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        
        if(scroll.data != null)
        {
            holder.SetActive(true);
        }
        else
        {

            InventoryUI inventory = UIHolder.instance.uiInventory;

            if(inventory != null )
            {
                if (inventory.IsOpen())
                {
                    Debug.Log("this was called");
                    holder.SetActive(true);
                }
                else
                {
                    holder.SetActive(false);
                }
            }
            else
            {
                holder.SetActive(false);
            }

            
            
        }
              
        empty.SetActive(scroll.data == null);

        if (scroll.data == null)
        {
            return;
        }

        portrait.sprite = scroll.data.itemSprite;
        curseText.text = scroll.data.curseAmount.ToString();
        nameText.text = scroll.data.itemName.ToString();    

    }
    

    public void UpdateResourceQuantity(int resourceQuantity)
    {
        resourceText.text = resourceQuantity.ToString();
    }
    public void UpdateCooldown(float current, float total)
    {
        cooldown.fillAmount = current / total;
    }
    public void ControlHolder(bool choice)
    {
        holder.SetActive(choice);
    }

    public void StartSelectingScroll()
    {
        StartCoroutine(SelectionProcess());
    }

    public void StopSelectingScroll()
    {
        indexText.gameObject.SetActive(false);
        StopAllCoroutines();
    }


    private void OnDisable()
    {
        indexText.gameObject.SetActive(true);
        StopAllCoroutines();
    }

    IEnumerator SelectionProcess()
    {
        indexText.transform.localPosition = originalIndexTextPos;
        indexText.gameObject.SetActive(true);


        for (int i = 0; i < 400; i++)
        {
            
            indexText.transform.localPosition += new Vector3(0, 0.02f, 0);
            yield return new WaitForSeconds(GameHandler.instance.timeModifier);
        }
        for (int i = 0; i < 400; i++)
        {
            indexText.transform.localPosition -= new Vector3(0, 0.02f, 0);
            yield return new WaitForSeconds(GameHandler.instance.timeModifier);
        }

        StartCoroutine(SelectionProcess());
    }


    

}
