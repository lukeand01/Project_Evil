using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BehaviorPatrol : Node
{
    public BehaviorPatrol()
    {
        
    }

    public override NodeState Evaluate()
    {
        //every frame i am looking for enemy or sound.
        //




        return base.Evaluate();
    }


    

}


//behavior patrol we keep walking around at intervals
//we need to get a location around us 
//i dont want to use stealth because it will be wasted. 
//but maybe the enemy still needs to stpo the player. but that isnt made easy because it is not the pioint

//it will work as following. 
//i dont want to set transforms for each fella. 
//maybe i will have universal patrol points?
//i will ignore patrol and all enemies will either know the player is there or not.