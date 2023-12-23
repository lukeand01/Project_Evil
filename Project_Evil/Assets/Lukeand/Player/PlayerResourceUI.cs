using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourceUI : MonoBehaviour
{
    [Separator("HEALTH AND CURSE")]
    [SerializeField] GameObject healthHolder;
    [SerializeField] Image healthBar;
    [SerializeField] Image curseBar;

    public void UpdateHealth(float current, float total)
    {
        healthBar.fillAmount = current/ total;

    }

    public void UpdateCursed(float current, float total)
    {
        curseBar.fillAmount = current / total;
    }


    [Separator("DASH")]
    [SerializeField] Image dashImage;

    public void UpdateDash(float current, float total)
    {
        dashImage.fillAmount = current/ total;
    }
}
