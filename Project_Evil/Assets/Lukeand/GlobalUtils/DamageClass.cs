using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageClass 
{
    public GameObject attackerRef {  get; private set; }

    public float baseDamage { get; private set; }
    public float staggerDamage {  get; private set; }   
    float critChance;
    float critDamage;
    float damageBasedInHealth;

    //we get the health scaling.

    public bool cannotFinishEntity { get; private set;}

    //List<BDClass> bdList = new();

    public DamageClass(float baseDamage)
    {
        this.baseDamage = baseDamage;       
    }
    
    public DamageClass(ItemGunData gunData)
    {
        baseDamage = gunData.damage;
        staggerDamage = 50;


    }

    #region MAKE

    public void MakeBlockFromFinishingEntity()
    {
        cannotFinishEntity = true;
    }

    public void MakeCritChance(float critChance)
    {
        this.critChance = critChance;
    }

    public void MakeAttackerRef(GameObject attackerRef)
    {
        this.attackerRef = attackerRef; 
    }
    

    #endregion


    

    public float GetDamage()
    {
        return baseDamage;
    }
}
