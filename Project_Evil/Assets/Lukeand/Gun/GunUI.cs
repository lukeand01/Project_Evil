using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{
    GameObject holder;

    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] TextMeshProUGUI reserveAmmoText;
    [SerializeField] TextMeshProUGUI gunNameText;
    [SerializeField] Image reloadingImage;
    [SerializeField] TextMeshProUGUI reloadingText;

    private void Awake()
    {
        holder = transform.GetChild(0).gameObject;
        totalTickForReloadText = 300;
    }

    public void UpdateGun(string name)
    {
        //we update icon
        //and we updater
        gunNameText.text = name;
    }

    public void UpdateReserveAmmo(int reserve)
    {
        
        if (reserve <= 0)
        {
            reserveAmmoText.color = Color.red;
        }
        else
        {
            reserveAmmoText.color = Color.white;
        }
        reserveAmmoText.text = reserve.ToString();

    }

    public void UpdateCurrentAmmo(int current)
    {
        if (current <= 0)
        {
            currentAmmoText.color = Color.red;
        }
        else
        {
            currentAmmoText.color = Color.white;
        }
        currentAmmoText.text = current.ToString();

    }


    int totalTickForReloadText;
    int currentTickForReloadText;
    int reloadTextState;

    public void ReloadImageUpdate(float current, float total)
    {
        reloadingImage.gameObject.SetActive(total > 0);
        reloadingImage.fillAmount = current / total;
        

        if(total <= 0)
        {
            currentTickForReloadText = 0;
            reloadTextState = 0;
        }
        else
        {
            currentTickForReloadText += 1;

            if(currentTickForReloadText >= totalTickForReloadText)
            {
                currentTickForReloadText = 0;
                ReloadTextAnimation();
            }
        }
    }

    void ReloadTextAnimation()
    {
        reloadTextState += 1;

        if(reloadTextState == 1)
        {
            reloadingText.text = "Reloading.";
        }
        if(reloadTextState == 2)
        {
            reloadingText.text = "Reloading..";
        }
        if(reloadTextState == 3)
        {
            reloadingText.text = "Reloading...";
        }
        if(reloadTextState >= 4)
        {
            reloadTextState = 0;
            ReloadTextAnimation();
        }

    }


}
