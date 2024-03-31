using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorSimpleDetect : Sequence2
{

    MyAIBase ai;

    public BehaviorSimpleDetect(MyAIBase ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {




        return base.Evaluate();
    }

}


//this detects any tht might be hostile to this fella.
//for now it detects by range. if it detects someone it passes to the aibase as target. the next behavior will use this to start acting,.
//for now it detects by range. if it detects someone it passes to the aibase as target. the next behavior will use this to start acting,.