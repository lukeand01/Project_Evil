using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIComponent : MyAIBase
{

    //the problem is that this might be identified asidamageable and i dont want that.
    //and this needs to have access for the health.

    public override bool IsAlive()
    {
        return true;
    }


}
