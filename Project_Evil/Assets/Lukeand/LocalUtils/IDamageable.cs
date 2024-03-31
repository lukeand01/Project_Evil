using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public string GetDamageableID();
    public void TakeDamage(DamageClass damage);

    public bool IsStaggered();

    public void ChangeCurseValue(float value);

    public bool IsAlive();

}
