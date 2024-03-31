using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    //this will carry much more informatio and now the enemybase will be the only monobehavior script
    //behavior list, perform attack



    public string enemyName;

    [Separator("STATS")]
    public float enemyHealth;
    public float enemySpeed;
    public List<EnemyAttackClass> enemyAttackList = new(); //this information is used by the enemy, because the enemy knows what it has

    public abstract Sequence2 GetBehavior(EnemyBase enemy);

    //

}

[System.Serializable]
public class EnemyAttackClass
{
    public float attackDamage;
    public float attackRangeMax;
    public float attackRangeMin;
}

//imagine an enemy has two moves.
//a charge move and a slash move
//teh charge should only trigger when the enemy is far away enough.
//while the slash attack only should be triggered from up close
//and how to trigger especial behavior? like the charge movement.
//maybe i should do different monobehavior scripts. because that allows bigger flexibility.
//but how would the behavior call these scripts?
//the problem is that i probrably require a courotine for this
//