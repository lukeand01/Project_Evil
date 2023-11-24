using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    //mopvemnt logic.
    Vector2 moveDir;
    Vector2 lastDir;
    Camera cam;
    private void Awake()
    {
        cam = Camera.main;
    }



    private void FixedUpdate()
    {
        LookAtMouse();
    }
    //move the player.
    #region MOVEMENT   
    public void MovePlayer(Vector3 value)
    {
        transform.position += value * Time.deltaTime;
    }

    void LookAtMouse()
    {
        //always lookig at the mouse unless something happens.
        //

        RotateToTarget(cam.ScreenToWorldPoint(Input.mousePosition));
    }

    private void RotateToTarget(Vector3 targetPos)
    {
        float rotationModifier = 90;
        Vector2 direction = targetPos - transform.position;

        if (direction.magnitude <= 2)
        {
            //we assume the thing to be a bit ahead than it really is.
            //i dont really know how to do this.

        }

        float angle = Vector2.SignedAngle(Vector2.right, direction);



        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, angle + rotationModifier), Time.deltaTime * 25);
    }

    #endregion


}
