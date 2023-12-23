using MyBox;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class PlayerCombat : MonoBehaviour
{
    PlayerHandler handler;  




    [Separator("REFERENCES")]
    [SerializeField] Transform shootingPos; //where the bullet comes from
    [SerializeField] Transform shootingPosInverse;
    [SerializeField] GameObject gunAim;
    [SerializeField] GameObject handAim;

    [Separator("TOOL REFERENCES")]
    [SerializeField] GameObject swordHolder;
    [SerializeField] GameObject shieldHolder;

    float currentShootingCooldown;
    float totalShootCooldown;
    bool hasPressed;


    Camera cam;

    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();

        cam = Camera.main;


        totalShieldCooldown = 0.5f;

    }

    

    //on for the mouse and another for the aim?
    //1 - range is based in weapon selected
    //i just dont care about colllider that seems like too mucch trouble.
    //

    private void Update()
    {

        

        if (handler.block.HasBlock(BlockClass.BlockType.Complete)) return;
        if (isBlocking) handAim.SetActive(false);
        if (handler.block.HasBlock(BlockClass.BlockType.Partial)) return;
        

        handAim.SetActive(true);
        handAim.transform.position = GetGunPos(false);

      
    }

    private void FixedUpdate()
    {
        if (totalShootCooldown > currentShootingCooldown)
        {
            currentShootingCooldown += 0.02f;
        }

        HandleToolCooldowns();
    }

    #region AIM AND SHOOTING

    public void Aim()
    {
        //we need to lmit the aim by the range of the gun.

        if (isBlocking)
        {
            gunAim.SetActive(false);
            return;
        }

        gunAim.SetActive(currentGun != null);
       
        if(currentGun != null) gunAim.transform.position = GetGunPos(true);
        
       
           
    }
    public void StopAiming()
    {
        gunAim.SetActive(false);
    }

    public void Shoot()
    {

        if (!gunAim.activeInHierarchy) return;
        if(currentGun == null) return;
        if (hasPressed && !currentGun.data.canHoldButton) return;
        if (totalShootCooldown > currentShootingCooldown) return;
        if (!currentGun.HasAmmo())
        {
            Debug.Log("has no ammo");
            return;
        }
        if (isReloading) return;



        currentGun.SpendAmmo();
        UIHolder.instance.uiGun.UpdateCurrentAmmo(currentGun.currentAmmo);
        //it comes from the same point but it hange a bit the target.
        hasPressed = true;
        totalShootCooldown = currentGun.data.timeBtwShoot;

        Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        dir.z = 0;

        for (int i = 0; i < currentGun.data.bulletPerShot; i++)
        {
            //we change a bit the dir;

            Vector3 offset = Vector2.zero;

            if(i != 0)
            {
                //then we can change teh thing but the first one is always in the center.
                float x = Random.Range(-0.15f, 0.15f);
                float y = Random.Range(-0.15f, 0.15f);
                offset = new Vector3(x, y, 0);
            }

            Bullet newObject = Instantiate(currentGun.data.bullet, shootingPos.position, Quaternion.identity);

            if(handler == null)
            {
                Debug.Log("no handler");
            }
            if (handler.playerDamageable == null)
            {
                Debug.Log("no damageable");
            }

            string ownID = handler.playerDamageable.GetDamageableID();

            DamageClass damageClass = new DamageClass(currentGun.data);
            damageClass.MakeAttackerRef(gameObject);

            newObject.SetUp(ownID, dir + offset, damageClass, currentGun.data.bulletSpeed);
            newObject.SetDestroySelf(currentGun.data.range);

        }
       
        
    }

    public void StoppedShooting()
    {
        hasPressed = false;
    }

    #endregion

    #region SWORD AND SHIELD

    //you can always attack with sword.
    // but when you attack a target that is staggered it pushes and does more damage around the staggered
    //

    float totalShieldCooldown;
    float currentShieldCooldown;
    bool isStaggeredAhead;
    bool isBlocking;

    void HandleToolCooldowns()
    {
        if(currentShieldCooldown > 0)
        {
            currentShieldCooldown -= 0.02f;
        }
       



    }


    void DetectStaggeredAhead()
    {
        //we check at every frame ahead if there is anyone that is staggered ahead.
    }

    public void UseSword()
    {
        if (currentSword == null) return;

        //play an animation for the sword.
        //this only does the thing if there is someone targettable 
        //for now it will just show it for a second and stop. but it will have dealt thinkgs.

        //check if there is anyone ahead.
        //

        DealDamageToAllEnemiesAhead();
    }

    void DealDamageToAllEnemiesAhead()
    {

        RaycastHit2D[] ray = Physics2D.CircleCastAll(transform.position, 5, handler.playerMove.lastDir, 15, 6);

        float damageAhead = 10;


        foreach (var item in ray)
        {
            if (isStaggeredAhead)
            {
                //push the enemy.
            }           

            IDamageable damageable = item.collider.gameObject.GetComponent<IDamageable>();

            if (damageable == null) continue;

            if (damageable.IsStaggered())
            {
                //deal more damage
            }
            else
            {

            }        

        }


    }

    
    public void UseShield()
    {
        if (currentShield == null) return;
        if (shieldHolder.activeInHierarchy) return;
        if (currentShieldCooldown > 0) return;

        //at any time. if in teh right moment it will block an attack.
        //the shields stays on for a short duration
        //there is a short cooldown to use it again.
        //you reduce speed for the duration.

        StartCoroutine(ShieldProcess());

    }

    IEnumerator ShieldProcess()
    {
        shieldHolder.SetActive(true);

        yield return new WaitForSeconds(0.7f);

        shieldHolder.SetActive(false);
        currentShieldCooldown = totalShieldCooldown;

    }

    public void ShieldProtectedSomething(Vector3 attackerPos)
    {
        handler.playerMove.RotateToTarget(attackerPos);
        StartCoroutine(ShieldProtectedSomethingProcess());
    }

    IEnumerator ShieldProtectedSomethingProcess()
    {
        GameHandler.instance.SlowGameTime();
        handler.playerMove.ControlIfShouldNotFollowMouse(false);
        handler.playerCamera.ControlCameraZoom(5);
        handler.block.AddBlock("Shield", BlockClass.BlockType.Partial);
        isBlocking = true;
        //zoom in the player
        //do vfx
        //if its a melee attacker then it should push hte attacker.
        

        yield return new WaitForSeconds(1.5f * Time.timeScale);

        isBlocking = false;
        handler.block.RemoveBlock("Shield");
        handler.playerCamera.ControlCameraZoom(7);
        GameHandler.instance.ResumeGameTime();
        handler.playerMove.ControlIfShouldNotFollowMouse(true);
    }

    public bool IsUsingShield()
    {
        return shieldHolder.activeInHierarchy;
    }

    #endregion

    #region CHANGE

    ItemClass currentGunItem;
    GunClass currentGun;
    int currentGunReserveAmmo;


    ItemClass currentSwordItem;
    ToolClass currentSword;

    ItemClass currentShieldItem;
    ToolClass currentShield;

    public void ChangeGun(ItemClass newGun)
    {
        //we get the gun from this itemclass. it needs to have one.
        //put the gun in the hand.


        if(currentGunItem != null)
        {
            currentGunItem.ControlEquip(false);
        }


        stopReloading();
        currentGunItem = newGun;
        currentGun = newGun.gun;
        currentGunItem.ControlEquip(true);
        ChangeGunGraphic(currentGun.data.graphicalTemplate, currentGun.data.graphicalOffsetValue, currentGun.data.graphicShouldRotate);
        RequestAmmoUpdate();
        ChangeGunUI();
        

    }
    public void ChangeSword(ItemClass newSword)
    {
       if(currentSwordItem != null)
        {
            currentSwordItem.ControlEquip(false);
        }

        currentSwordItem = newSword;
        currentSwordItem.ControlEquip(true);
        currentSword = currentSwordItem.tool;

    }
    public void ChangeShield(ItemClass newShield)
    {
        if (currentShieldItem != null)
        {
            currentShieldItem.ControlEquip(false);
        }

        currentShieldItem = newShield;
        currentShieldItem.ControlEquip(true);
        currentShield = currentShieldItem.tool;
    }
    public void EquipIfEmpty(ItemClass item)
    {
        bool isGun = item.data.GetGun() != null && currentGunItem == null;

        if (isGun)
        {
            ChangeGun(item);
            return;
        }

        bool isSword = item.data.GetTool() != null && currentSwordItem == null && item.data.GetTool().isSword;
        
        if(isSword) 
        {
            ChangeSword(item); 
            return;
        }

        bool isShield = item.data.GetTool() != null && currentShieldItem == null && !item.data.GetTool().isSword;

        if(isShield)
        {
            ChangeShield(item);
            return;
        }
    }

    void ChangeGunUI()
    {
       UIHolder.instance.uiGun.UpdateGun(currentGun.data.itemName);
       UIHolder.instance.uiGun.UpdateCurrentAmmo(currentGun.currentAmmo);
       UIHolder.instance.uiGun.UpdateReserveAmmo(currentGunReserveAmmo);
    }

    //this is called when i add or remove an ammoitem.



    #endregion

    #region AMMO
    bool isReloading;

    public void Reload()
    {
        if (currentGun == null) return;
        if (!currentGun.CanReload())
        {
            Debug.Log("cannot reload gun");
            return;
        }
        //we need to have at least some ammo.
        if(currentGunReserveAmmo <= 0)
        {
            Debug.Log("not enough reserve ammo");
            return;
        }

        if (isReloading) return;

        isReloading = true;

        int amountToReload = currentGun.GetAmountRemainingForFullReload();
        amountToReload = Mathf.Clamp(amountToReload, 0, currentGunReserveAmmo);  
        

        //then we spend that amount
        StartCoroutine(ReloadProcess(amountToReload));
    }

    void stopReloading()
    {
        isReloading = false;
        StopAllCoroutines();
    }

    IEnumerator ReloadProcess(int amountToReload)
    {

        float current = 0;
        float total = currentGun.data.reloadingSpeed;

        while(current < total)
        {
            current += 0.001f;
            UIHolder.instance.uiGun.ReloadImageUpdate(current, total);
            yield return new WaitForSeconds(0.001f);
        }


        UIHolder.instance.uiGun.ReloadImageUpdate(0,0);
        handler.playerInventory.SpendAmmo(currentGun.data.ammoType, amountToReload);
        RequestAmmoUpdate();
        currentGun.Reload(amountToReload);
        UIHolder.instance.uiGun.UpdateCurrentAmmo(currentGun.currentAmmo);
        isReloading = false;
    }



    public void AmmoWasUpdate(AmmoType ammo, int newValue)
    {
        if (currentGun == null) return;

        if (currentGun.data.ammoType == ammo)
        {
            currentGunReserveAmmo = newValue;
            UIHolder.instance.uiGun.UpdateReserveAmmo(currentGunReserveAmmo);
        }

    }
    //this is called when i change an weapon
    public void RequestAmmoUpdate()
    {
        currentGunReserveAmmo = handler.playerInventory.GetAmmo(currentGun.data.ammoType);
        UIHolder.instance.uiGun.UpdateReserveAmmo(currentGunReserveAmmo);
    }

    


    #endregion

    #region GRAPHICALCHANGE

    GameObject currentGunGraphic;

    void ChangeGunGraphic(GameObject newGraphic, float offsetValue, bool shouldRotate)
    {
        Destroy(currentGunGraphic);

        Vector3 offsetDir = (shootingPos.position - transform.position).normalized;
        offsetDir *= offsetValue;

        Transform targetTransform = shootingPos;

        if (shouldRotate)
        {
            targetTransform = shootingPosInverse;

        }
        
        GameObject newObject = Instantiate(newGraphic, targetTransform.transform.position + offsetDir, Quaternion.identity);
        newObject.transform.parent = targetTransform;
        newObject.transform.rotation = new Quaternion(0, 0, 0, 0);

           
        currentGunGraphic = newObject;
    }

    #endregion


    #region GET VECTORS
    Vector3 AttackDir()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return mousePos - transform.position ;
    }
    Vector3 GetGunPos(bool clampPosBasedInGunRange)
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (clampPosBasedInGunRange)
        {
            float distance = currentGun.data.range;
            Vector3 dir = mousePos - transform.position;

            if(dir.magnitude > distance)
            {
                dir = Vector3.ClampMagnitude(dir, distance);    
            }

            return transform.position + dir;


        }     

        return mousePos;


    }

    //the problem is that ts not moving farther away together with the aim. it should only move if its in the reach 
   

    //it should be  

    #endregion

  
}

//when you shoot
// when you equip
//when you gain ammo.