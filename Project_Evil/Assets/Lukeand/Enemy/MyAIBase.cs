
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAIBase : Tree, IDamageable
{

    //how can i be able to chase both ai and player. 

    protected void SetUpAIBase()
    {
        aiPathController = GetComponent<AIPath>();
    }


   
    protected virtual Sequence2 GetBehavior()
    {
        return null;
    }

    #region TARGETTING

    MyAIBase aiTarget;
    PlayerHandler playerTarget;


    public void SetPlayerTarget(PlayerHandler target)
    {
        playerTarget = target;
        aiTarget = null;
    }
    public void SetAITarget(MyAIBase ai)
    {
        aiTarget = ai;
        playerTarget = null;
    }

    public Transform GetTarget()
    {
        if(aiTarget != null)
        {
            return aiTarget.transform;  
        }
        if(playerTarget != null)
        {
            return playerTarget.transform;
        }
        return null;
    }

    public bool IsTargetAlive()
    {
        if (aiTarget != null)
        {
            return aiTarget.IsAlive();
        }
        if (playerTarget != null)
        {
            return playerTarget.playerDamageable.IsAlive();
        }

        return false;
    }


    #endregion

    #region PATHFIND

    AIPath aiPathController;


    public void StartMoveToTarget(Vector3 pos)
    {
        if (aiPathController == null) return;

        aiPathController.destination = pos;
    }
    public void StopMove()
    {
        aiPathController.isStopped = true;
    }


    #endregion


    #region IDAMAGEABLE
    public virtual void ChangeCurseValue(float value)
    {
        throw new System.NotImplementedException();
    }

    public virtual string GetDamageableID()
    {
        throw new System.NotImplementedException();
    }

    public virtual bool IsStaggered()
    {
        throw new System.NotImplementedException();
    }

    public virtual void TakeDamage(DamageClass damage)
    {
        throw new System.NotImplementedException();
    }

    public virtual bool IsAlive()
    {
        throw new System.NotImplementedException();
    }

    #endregion
}
