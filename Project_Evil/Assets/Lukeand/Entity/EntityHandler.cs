using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHandler : MonoBehaviour
{

    public EntityDamageable damageable { get; private set; }


    private void Awake()
    {
        damageable = GetComponent<EntityDamageable>();
    }


}
