using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    PlayerHandler handler;
    //mopvemnt logic.
    Vector2 moveDir;
    public Vector3 lastDir {  get; private set; }

    [SerializeField] float speed;
    private void Awake()
    {
        shouldFollowMouse = true;


        handler = GetComponent<PlayerHandler>();    
    }

    bool shouldFollowMouse;

    private void FixedUpdate()
    {
       if(shouldFollowMouse) LookAtMouse();

        HandleDashCooldown();
    }
    //move the player.
    #region MOVEMENT   
    public void MovePlayer(Vector3 value)
    {

        if (isDashing)
        {

            return;
        }

        if(value != Vector3.zero)
        {
            lastDir = value;
        }

        moveDir = value;
        //transform.position += value * Time.deltaTime * speed;
        handler.rb.velocity = value *  speed;
    }

    void LookAtMouse()
    {
        //always lookig at the mouse unless something happens.
        //

        RotateToTarget(handler.cam.ScreenToWorldPoint(Input.mousePosition));
    }

    public void RotateToTarget(Vector3 targetPos)
    {
        float rotationModifier = 90;
        Vector2 direction = targetPos - transform.position;

        if (direction.magnitude <= 0.15f)
        {
            //we assume the thing to be a bit ahead than it really is.
            //i dont really know how to do this.

        }
        else
        {
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            transform.eulerAngles = new Vector3(0, 0, angle + rotationModifier);
        }

            
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, angle + rotationModifier), Time.deltaTime * 25);
    }


    public void ControlIfShouldNotFollowMouse(bool choice)
    {
        shouldFollowMouse = choice;
    }

    #endregion

    #region DASH
    [Separator("DASH")]
    [SerializeField] float dashSpeed;
    [SerializeField] float totalDashCooldown;
    [SerializeField] float totalDashDistance;
    float currentDashCooldown;
    float currentDashDistance;
    bool isDashing;



    void HandleDashCooldown()
    {

        if(currentDashDistance > 0)
        {
            currentDashDistance -= 0.02f;
            return;
        }

        if (isDashing)
        {
            StopDashing();
        }

        
        if(currentDashCooldown > 0)
        {
            currentDashCooldown -= 0.02f;
            UIHolder.instance.uiResource.UpdateDash(currentDashCooldown, totalDashCooldown);
        }
        else
        {
            UIHolder.instance.uiResource.UpdateDash(0, 1);
        }
    }

    void StopDashing()
    {
        isDashing = false;
        gameObject.layer = 3;
    }
    public void Dash()
    {
        //we always dash towards the lastdir

        if(lastDir == Vector3.zero)
        {
            Debug.Log("last dir is wrong");
            return;
        }
        if (currentDashCooldown > 0) return;

        isDashing = true;
        Debug.Log("dash");
        gameObject.layer = 7;
        handler.rb.AddForce(lastDir * dashSpeed, ForceMode2D.Impulse);
        currentDashCooldown = totalDashCooldown;
        currentDashDistance = totalDashDistance;
    }


    #endregion

}
