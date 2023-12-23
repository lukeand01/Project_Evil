using MyBox;
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
    public float reloadingSpeed;
    public GunType gunType;
    public AmmoType ammoType;


    [Separator("Graphical")]
    public Bullet bullet;
    public GameObject graphicalTemplate;
    public float graphicalOffsetValue;
    public bool graphicShouldRotate;


    private void Awake()
    {
        itemType = ItemType.Gun;
    }
    public override void UseItem(ItemClass item)
    {
        base.UseItem(item);

        //pass this to the char.

        if(item.gun == null)
        {
            Debug.Log("gun");
            return;
        }
        

        PlayerHandler.Instance.playerCombat.ChangeGun(item);
        
    }


    public override ItemGunData GetGun() => this;
    
}

public enum GunType
{
    Pistol,
    Shotgun,
    Rifle
}