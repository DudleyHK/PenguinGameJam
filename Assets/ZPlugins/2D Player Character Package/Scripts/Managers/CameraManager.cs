using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraManager : MonoBehaviour 
{
    public GameObject player;
    public float acceleration = 0.1f;
    public Camera cam;
    public int camIndex;


    private void Start()
    {
        foreach(var possiblePlayer in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(possiblePlayer.GetComponent<PlayerData>().PlayerIndex == camIndex)
            {
                player = possiblePlayer;
            }
        }

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


        if(GameObject.FindGameObjectsWithTag("Player").Length == 4 )
        {
            switch (camIndex)
            {
                case 1:
                    cam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                    break;
                case 2:
                    cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                    break;
                case 3:
                    cam.rect = new Rect(0, 0, 0.5f, 0.5f);
                    break;
                case 4:
                    cam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                    break;
            }
        }

        else if(GameObject.FindGameObjectsWithTag("Player").Length == 3)
        {
            switch (camIndex)
            {
                case 1:
                    cam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                    break;
                case 2:
                    cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                    break;
                case 3:
                    cam.rect = new Rect(0, 0, 1, 0.5f);
                    break;
                case 4:
                    cam.rect = new Rect(0.5f, 0, 0.0f, 0.0f);
                    break;
            }
        }

        else if (GameObject.FindGameObjectsWithTag("Player").Length == 2)
        {
            switch (camIndex)
            {
                case 1:
                    cam.rect = new Rect(0, 0.5f, 1.0f, 0.5f);
                    break;
                case 2:
                    cam.rect = new Rect(0.0f, 0, 1.0f, 0.5f);
                    break;
                case 3:
                    cam.rect = new Rect(0, 0, 0, 0);
                    break;
                case 4:
                    cam.rect = new Rect(0.0f, 0, 0.0f, 0.0f);
                    break;
            }
        }



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
