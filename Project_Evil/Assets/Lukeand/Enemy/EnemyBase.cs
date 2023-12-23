using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    //this current verisonm will be a test.
    [SerializeField] TextMeshProUGUI DEBUGstaggerText;
    private void Awake()
    {
        id = Guid.NewGuid().ToString();
        gameObject.layer = 6; //sets this layer to enemylayer

        currentHealth = totalHealth;
        totalStagger = 10;
    }

    private void Update()
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

    private void FixedUpdate()
    {
        HandleStagger();
    }



    #region TAKE DAMAGE
    //we roll 



    string id;
    public string GetDamageableID()
    {
        return id;
    }

    [SerializeField] float totalHealth;
    float currentHealth;

    float currentStaggerProgress;
    bool isStaggered;
    bool hasBeenStaggered;

    public void TakeDamage(DamageClass damage)
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

    public bool IsStaggered()
    {
        return isStaggered;
    }

    public void ChangeCurseValue(float value)
    {
        
    }

    #endregion


    #region STAGGER

    float currentStagger;
    float totalStagger;

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
}
