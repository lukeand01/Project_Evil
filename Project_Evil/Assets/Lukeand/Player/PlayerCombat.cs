using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{


    [SerializeField]LineRenderer aimLineRend;

    private void FixedUpdate()
    {
        ControlAimLineRend();
    }

    void ControlAimLineRend()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 

            aimLineRend.enabled = true;
            aimLineRend.positionCount = 2;
            aimLineRend.SetPosition(0, transform.position);
            aimLineRend.SetPosition(1, mousePos);

        }



    }


}
