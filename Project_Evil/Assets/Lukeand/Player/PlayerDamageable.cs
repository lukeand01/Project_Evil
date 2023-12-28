using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerDamageable : MonoBehaviour, IDamageable
{

    PlayerHandler handler;

    [SerializeField] float initialHealth;
    [SerializeField] float initialCurse;


    float currentHealth;
    float totalHealth;

    float currentCurse;


    private void Awake()
    {
        id = Guid.NewGuid().ToString();
        handler = GetComponent<PlayerHandler>();    
    }

    private void Start()
    {
        totalHealth = initialHealth;
        currentHealth = totalHealth;
        currentCurse = initialCurse;

        UIHolder.instance.uiResource.UpdateHealth(currentHealth, totalHealth);
        UIHolder.instance.uiResource.UpdateCursed(currentCurse, totalHealth);
    }


    string id = "";

    public string GetDamageableID()
    {
        return id;
    }

    public void TakeDamage(DamageClass damage)
    {

        if (handler.playerCombat.IsUsingShield())
        {
            //we nulify the damage
            //we do a cool effect.
            //we slow down the game for a moment
            //the player rotates towards the last attacker.

            if (!handler.playerCombat.isBlocking)
            {
                handler.playerCombat.ShieldProtectedSomething(damage.attackerRef.transform.position);
            }

            
            return;
        }

        currentHealth -= damage.baseDamage;

        UIHolder.instance.uiResource.UpdateHealth(currentHealth, totalHealth);

        if(currentHealth <= 0 || currentHealth <= currentCurse)
        {
            Die();
        }


    }

    void Die()
    {
        Debug.Log("should die");
    }

    public bool IsStaggered()
    {
        return false;
    }

    public void ChangeCurseValue(float value)
    {
        currentCurse += value;
        currentCurse = Mathf.Clamp(currentCurse, 0, 100);

        if(currentCurse >= currentHealth)
        {
            Debug.Log("maybe it shouldnt kill but should make the character weak");
        }

        UIHolder.instance.uiResource.UpdateCursed(currentCurse, totalHealth);
    }
}
