using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour 
{
    public List<InputAxisState> inputs = new List<InputAxisState>();
    public InputState inputState;
    public string playerTag = "Player";

    public int controllerID;

    private void Start()
    {
        foreach(var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(player.GetComponent<PlayerData>().PlayerIndex == controllerID)
            {
                inputState = player.GetComponent<InputState>();
            }
        }

        //if (!inputstate)
        //{
        //    debug.log("inputmanager players inputstate not set.");
        //    var player = gameobject.findgameobjectwithtag(playertag);
        //    if (player)
        //    {
        //        inputstate = player.getcomponent<inputstate>();
        //    }
        //    else
        //    {
        //        print("error: player tag not applied");
        //    }
        //}
    }

    void Update () 
    {
        foreach(var input in inputs)
        {
            inputState.SetButtonValue(input.button, input.value);
        }
	}
}
