using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorSimpleChase : Sequence2
{

    //it will chase the first target in sight.
    //i dont want it to be just the enemy.

    MyAIBase ai;

    public BehaviorSimpleChase(MyAIBase ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        //you can be chasing the player or an ai.
        //

        Transform targetToChase = ai.GetTarget();

        if(targetToChase == null)
        {
            //you can never chase something does not exist
            return NodeState.Failure;
        }

        if (ai.IsTargetAlive() == false)
        {
            return NodeState.Failure;
        }

        //so we get here where we order the fella to chase.
        ai.StartMoveToTarget(targetToChase.position);

        //and if its in range to attack we stop and go to the next.



        return NodeState.Running;
    }

}

//how am i going to handle the attack system?