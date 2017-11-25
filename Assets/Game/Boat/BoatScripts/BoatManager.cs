using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BoatManager : MonoBehaviour 
{
    public NetManager netManager;
    public Animator boatAnimator;
    public Fade fade;

    public int animationKey = 0; // this is used to keep a track of the last used state.
    public float minWaitTime = 5f;
    public float maxWaitTime = 15f;
    public float randTimer = 0f;
    public bool randTimeSelected = false;


    private void Start()
    {
        randTimer = Random.Range(minWaitTime, maxWaitTime);
        netManager = GetComponent<NetManager>();
        boatAnimator = GetComponent<Animator>();
    }



    private void Update()
    {
        if(randTimer >= 0f)
        {
            randTimer -= Time.deltaTime;
        }
        else
        {
            // If the previous fishing session has finished
            if(netManager.netStates == NetManager.NetStates.Off)
            {
                fade.fadeState = Fade.FadeState.FadeIn;
                netManager.startFishing = true;
            }
            else
            {
                netManager.startFishing = false;
            }
            // Select a new wait time. 
            randTimer = Random.Range(minWaitTime, maxWaitTime);
        }

        if(netManager.netStates == NetManager.NetStates.ReelIn)
        {
            fade.fadeState = Fade.FadeState.FadeOut;
        }



        // Animation stuff
        boatAnimator.SetBool("ReelOut", false);
        boatAnimator.SetBool("ReelIn",  false);
        boatAnimator.SetBool("Catching",false);
        if(netManager.netStates == NetManager.NetStates.ReelOut)
        {
            boatAnimator.SetBool("ReelOut", true);
        }
        else if(netManager.netStates == NetManager.NetStates.ReelIn)
        {
            boatAnimator.SetBool("ReelIn", true);
        }
        else if(netManager.netStates == NetManager.NetStates.Catching)
        {
            boatAnimator.SetBool("Catching", true);
        }
    }
}
