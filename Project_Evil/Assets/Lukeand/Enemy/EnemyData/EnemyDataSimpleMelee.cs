using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy / SimpleMelee")]
public class EnemyDataSimpleMelee : EnemyData
{

    public override Sequence2 GetBehavior(EnemyBase enemy)
    {

        return new Sequence2(new List<Node>
        {
            new BehaviorSimpleDetect(enemy),
            new BehaviorSimpleChase(enemy)

        });



    }

    

}


//behavior
//