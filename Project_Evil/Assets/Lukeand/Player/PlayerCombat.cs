using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Separator("DEBUG GUNS")]
    [SerializeField] ItemGunData debugGun;
    [SerializeField] ItemGunData debugPistol;
    [SerializeField] ItemGunData debugShotgun;
    [SerializeField] ItemGunData debugRifle;


    [Separator("Line")]
    [SerializeField] GameObject aim;






    Dictionary<GunType, List<ItemClass>> ammoDictionary = new();

    [SerializeField] Transform shootingPos;


    PlayerHandler handler;


    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();
        debugGun = debugPistol;
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            debugGun = debugPistol;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            debugGun = debugShotgun;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            debugGun = debugRifle;
        }

        
    }

  
   void RendLine()
    {
        //aimLineRend.enabled = true;
        //aimLineRend.positionCount = 2;
        //aimLineRend.SetPosition(0, transform.position);
        //aimLineRend.SetPosition(1, transform.position + GetGunPos());
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
        //shoot a projectile.
        //

        if (debugGun == null) return;


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

        
    }

    public void ChangeGun(ItemGunData data)
    {
        debugGun = data;
    }

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


        float distance = debugGun.range;

        RaycastHit2D ray = Physics2D.Raycast(transform.position, mousePos.normalized, distance, LayerMask.GetMask(LayerMaskEnum.Wall.ToString()));

        if(ray.collider != null)
        {
            Debug.Log("hit something " + ray.collider.name);
            distance = ray.distance;
        }

        mousePos = Vector3.ClampMagnitude(mousePos, distance);

        return mousePos;


    }

    #endregion

}
