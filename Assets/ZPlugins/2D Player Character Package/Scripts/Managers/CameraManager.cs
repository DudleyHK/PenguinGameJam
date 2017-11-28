using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraManager : MonoBehaviour 
{
    public GameObject player;
    public float acceleration = 0.1f;
    public Camera cam;


    private void Start()
    {
        if(!player)
        {
            var playerTag = GameObject.FindGameObjectWithTag("Player");
            if(playerTag)
            {
                player = playerTag;
            }
            else
            {
                print("ERROR: Player tag not applied");
            }
        }

        cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        var cameraPosition = this.transform.position;
        var velocity = (player.transform.position - cameraPosition) * acceleration;

        var verticalSize = cam.orthographicSize * 2.0;
        var horizontalSize = verticalSize * Screen.width / Screen.height;


        var camLeft  = transform.position.x - (horizontalSize / 2f);
        var camRight = transform.position.x + (horizontalSize / 2f);
        var camDown  = transform.position.y - (verticalSize / 2f);

       // print("Cameras Y velocity is " + velocity.y);
       // print("CamDown " + camDown + " <= " + Water.down + " is " + (camDown <= Water.down));


        if(camLeft <= Water.left && velocity.x < 0f)
        {
            velocity.x = 0f;
            //Debug.Log("Camera position locked to water left");
        }
        if(camRight >= Water.right && velocity.x > 0f)
        {
            velocity.x = 0f;
            //Debug.Log("Camera position locked to water right");
        }
        if(camDown <= Water.down && velocity.y < 0f)
        {
            velocity.y = 0f;
            //Debug.Log("Camera position locked to water down");
        }

        
        transform.Translate(velocity.x, velocity.y, 0);
    }
}
