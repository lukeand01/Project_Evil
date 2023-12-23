using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GunClass 
{

    public ItemGunData data { get; private set; }

    public int currentAmmo {  get; private set; }   


    public GunClass(ItemGunData data)
    {
        this.data = data;

        currentAmmo = data.clipSize;
    }
    

    public bool HasAmmo()
    {
        return currentAmmo > 0;
    }

    public bool CanReload()
    {
        return currentAmmo < data.clipSize;
    }
    public void Reload(int ammo)
    {

        currentAmmo += ammo;

        if(currentAmmo > data.clipSize)
        {
            Debug.Log("something wrong here");
        }
    }

    public void SpendAmmo()
    {
        currentAmmo--;
    }

    public int GetAmountRemainingForFullReload()
    {
        return data.clipSize - currentAmmo;
    }

}
