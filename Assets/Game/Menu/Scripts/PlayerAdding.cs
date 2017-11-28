using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdding : MonoBehaviour {

    public int thisIndex;

    public bool ControllerActive, lockedIn;

    public Animator pengAnim;


    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {

        if (ControllerActive && !lockedIn)
        {
            if (Input.GetButtonUp("Controller " + thisIndex + " - A"))
            {
                lockedIn = true;
                pengAnim.SetTrigger("startWalking");
            }
        }

        if (ControllerActive && !lockedIn)
        {
            if (Input.GetButtonUp("Controller "+ thisIndex +" - B"))
            {
                ControllerActive = false;
                pengAnim.SetTrigger("unJoined");
            }

        }

        if (!ControllerActive && !lockedIn)
        {
            if (Input.GetButtonUp("Controller " + thisIndex + " - A"))
            {
                ControllerActive = true;
                pengAnim.SetTrigger("joined");
            }

        }



        if(lockedIn)
        {
            if (Input.GetButtonUp("Controller " + thisIndex + " - B"))
            {
                lockedIn = false;
                pengAnim.SetTrigger("startWaiting");
            }
        }

    }
}
