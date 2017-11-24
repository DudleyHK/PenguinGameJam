using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdding : MonoBehaviour {

    public int thisIndex;

    public bool ControllerActive;

    public SpriteRenderer penguin;


    // Use this for initialization
    void Start () {
        penguin.enabled = false;	
	}
	
	// Update is called once per frame
	void Update () {

        if (ControllerActive)
        {
            if (Input.GetButtonUp("Controller "+ thisIndex +" - B"))
            {
                ControllerActive = false;
                penguin.enabled = false;
            }

        }

        if (!ControllerActive)
        {
            if (Input.GetButtonUp("Controller " + thisIndex + " - A"))
            {
                ControllerActive = true;
                penguin.enabled = true;
            }

        }


    }
}
