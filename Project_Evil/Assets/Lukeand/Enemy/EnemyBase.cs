using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Pathfinding;
using MyBox;
public class EnemyBase : MyAIBase
{
    //this current verisonm will be a test.
    [SerializeField] TextMeshProUGUI DEBUGstaggerText;
    private void Awake()
    {
        id = Guid.NewGuid().ToString();
        gameObject.layer = 6; //sets this layer to enemylayer

        currentHealth = totalHealth;
        totalStagger = 10;

        SetUpAIBase();
    }

    private void Start()
    {
        SetUp();
    }

    private void Update()
    {
        DebugStagger();
    }

    private void FixedUpdate()
    {
        HandleStagger();
    }

    #region SET UP
    [Separator("SET UP")]
    [SerializeField] protected EnemyData data;


    protected virtual void SetUp()
    {

    }

    protected override Sequence2 GetBehavior()
    {
        return null;
    }


    protected bool IsAttacking;
    public virtual void CallAttack()
    {
        //in here we are going to handle all the logic for attacking.
        //but here is the thing: i might want enemey to do a bunch of different things.
        //like target enemy other enemy, 
        //what dictates if it wanna target someone else 

        //how do i decide when th



    }


    #endregion


    #region TAKE DAMAGE
    //we roll 


    string id;
    [SerializeField] float totalHealth;
    float currentHealth;

    float currentStaggerProgress;
    bool isStaggered;
    bool hasBeenStaggered;

    public override string GetDamageableID()
    {
        return id;
    }

    public override void TakeDamage(DamageClass damage)
    {
        //enough damage will make thisc char stunned for a brief momeent. getting close to a character suchc as this will give the opportunity for the champ to attack 

        currentHealth -= damage.baseDamage;
        currentStaggerProgress += damage.staggerDamage;

        if(isStaggered )
        {
            StopStagger();
        }

        if(currentHealth <= 0 )
        {
            //die

            return;
        }

        if(currentStaggerProgress >= 100 && !hasBeenStaggered)
        {
            //stagger the oponent.
            StartStagger();
        }

    }

    public override bool IsStaggered()
    {
        return isStaggered;
    }

    public override void ChangeCurseValue(float value)
    {
        
    }

    #endregion

    #region STAGGER

    float currentStagger;
    float totalStagger;

    void DebugStagger()
    {
        if (isStaggered)
        {
            DEBUGstaggerText.text = "IsStaggered";
        }
        else
        {
            DEBUGstaggerText.text = "";
        }

    }
    void HandleStagger()
    {
        if (!isStaggered) return;

        if (currentStagger >= totalStagger)
        {
            StopStagger();
        }
        else
        {
            currentStagger += 0.02f;
        }
    }
    
    void StartStagger()
    {
        isStaggered = true; 
        hasBeenStaggered = true;
    }

    void StopStagger()
    {
        isStaggered = false;
    }




    #endregion

    #region ANIMATION


    #endregion


    #region DEBUG
    [SerializeField] Transform debugTargetPos;
    [SerializeField] AIPath debugPathfind;

    [ContextMenu("DEBUG START MOVING")]
    public void DebugStartMoving()
    {
        debugPathfind.destination = debugTargetPos.position;
    }

    


    #endregion

}
