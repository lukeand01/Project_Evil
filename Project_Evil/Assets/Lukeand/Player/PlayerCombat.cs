using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    PlayerHandler handler;


    [Separator("CLASSES")]
    GunClass equippedGun;


    [Separator("AMMO")]
    Dictionary<GunType, List<ItemClass>> ammoDictionary = new();


    [Separator("REFERENCES")]
    [SerializeField] Transform shootingPos; //where the bullet comes from
    [SerializeField] GameObject aim;

    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();
    }
    

    public void Aim()
    {
        //we need to lmit the aim by the range of the gun.
        aim.SetActive(true);
        aim.transform.position = GetGunPos();       
    }
    public void StopAiming()
    {
        aim.SetActive(false);
    }

    public void Shoot()
    {
 
        /*
        for (int i = 0; i < debugGun.bulletPerShot; i++)
        {
            Vector3 offset = Vector3.zero;

            if(i > 0)
            {

            }

            Bullet newObject = Instantiate(debugGun.bullet, shootingPos.position, Quaternion.identity);
            string ownID = handler.entityHandler.damageable.id;
            newObject.SetUp(ownID, AttackDir(), 5, debugGun.bulletSpeed);
            newObject.SetDestroySelf(debugGun.range);
        }
        */
        
    }

    #region CHANGE
    public void ChangeGun(GunClass newGun)
    {
        
    }
    public void ChangeSword()
    {

    }
    public void ChangeShield()
    {

    }

    #endregion

    #region AMMO

    public void AddAmmo(GunType gunType, ItemClass item)
    {
        if (ammoDictionary.ContainsKey(gunType))
        {
            ammoDictionary[gunType].Add(item);
        }
        else
        {
            List<ItemClass> itemList = new()
            {
                item
            };
            ammoDictionary.Add(gunType, itemList);
        }
    }
    public void RemoveAmmo()
    {

    }

    public int GetTotalAmmo()
    {
        return 0;
    }

    #endregion

    #region GET VECTORS
    Vector3 AttackDir()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePos - transform.position ;
    }
    Vector3 GetGunPos()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        float distance = 5;

        RaycastHit2D ray = Physics2D.Raycast(transform.position, mousePos.normalized, distance, LayerMask.GetMask(LayerMaskEnum.Wall.ToString()));

        if(ray.collider != null)
        {

            distance = ray.distance;
        }

        mousePos = Vector3.ClampMagnitude(mousePos, distance);

        return mousePos;


    }

    #endregion



}
