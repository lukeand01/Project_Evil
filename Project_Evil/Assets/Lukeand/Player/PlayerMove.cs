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
    public Vector2 lastDir {  get; private set; }
    Camera cam;
    [SerializeField] float speed;
    private void Awake()
    {
        shouldFollowMouse = true;
        cam = Camera.main;

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
        lastDir = value;
        transform.position += value * Time.deltaTime * speed;
        //handler.rb.velocity = value *  speed;
    }

    void LookAtMouse()
    {
        //always lookig at the mouse unless something happens.
        //

        RotateToTarget(cam.ScreenToWorldPoint(Input.mousePosition));
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
    float currentDashCooldown;

    void HandleDashCooldown()
    {
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

    public void Dash()
    {
        //we always dash towards the lastdir

        if(lastDir == Vector2.zero)
        {
            Debug.Log("last dir is wrong");
            return;
        }
        if (currentDashCooldown > 0) return;



        handler.rb.AddForce(lastDir * dashSpeed);
        currentDashCooldown = totalDashCooldown;

    }


    #endregion

}
