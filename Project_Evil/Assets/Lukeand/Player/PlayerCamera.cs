using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    PlayerHandler handler;
    public Camera mainCam {  get; private set; }

    private void Awake()
    {
        handler = GetComponent<PlayerHandler>();
        mainCam = Camera.main;
    }

    private void Update()
    {
        mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        


    }

    //we ccontrol this things.
    public void ControlCameraZoom(float value)
    {
        mainCam.orthographicSize = value;
    }
    

}
