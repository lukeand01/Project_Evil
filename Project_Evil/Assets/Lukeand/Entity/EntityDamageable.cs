using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageable : MonoBehaviour
{
    float current;
    float total;
    public string id { get; private set; }


    private void Awake()
    {
        id = Guid.NewGuid().ToString();
    }

    public void TakeDamage(float damage)
    {

    }
}
