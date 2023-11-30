using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunClass 
{
    //
    public ItemGunData data { get; private set; }
    float current;
    float total;

    public void Shoot(bool isHolding = false)
    {
        if (!CanShoot())
        {
            return;
        }

        

    }

    bool CanShoot()
    {
        return current <= 0;
    }

    void HandleShootingCooldown()
    {
        if(current > 0)
        {
            current -= 0.01f;
        }
       
    }

}
