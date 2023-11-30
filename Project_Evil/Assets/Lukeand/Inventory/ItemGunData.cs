using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item / Gun")]
public class ItemGunData : ItemData
{
    public int clipSize;
    public int bulletPerShot = 1;
    public float damage;
    public float penetrationPower;
    public float range;
    public float timeBtwShoot;
    public bool canHoldButton;
    public float bulletSpeed;
    public GunType gunType;
    public Bullet bullet;
    public override ItemGunData GetGun() => this;
    
}

public enum GunType
{
    Pistol,
    Shotgun,
    Rifle
}