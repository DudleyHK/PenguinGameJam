using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BoatManager : MonoBehaviour 
{
    public enum BoatState
    {
        ReelOut, 
        ReelIn, 
        Catching,
        Off
    }
    public BoatState boatState = BoatState.Off;

    public NetManager netManager;
    public Animator boatAnimator;
    public Fade fade;

    public float minWaitTime = 5f;
    public float maxWaitTime = 15f;
    public float randTimer = 0f;


    private void Start()
    {
        randTimer = Random.Range(minWaitTime, maxWaitTime);
        netManager = GetComponent<NetManager>();
        boatAnimator = GetComponent<Animator>();
    }



    private void Update()
    {
        RunBoatEvent();
    }


    private void RunBoatEvent()
    {
        switch(boatState)
        {
            case BoatState.ReelOut:  ReelOut();   break;
            case BoatState.ReelIn:   ReelIn();    break;
            case BoatState.Catching: Catching();  break;
            case BoatState.Off:      Off();       break;
        }
    }

    float reelOutTimer = 3f;
    bool ReelOutFlag = false;
    private void ReelOut()
    {
        if(!ReelOutFlag)
        {
            reelOutTimer = 3f;
            ReelOutFlag = true;
        }

        if(reelOutTimer >= 0f)
        {
            // Set the boat animation to reel out. 
            reelOutTimer -= Time.deltaTime;
            boatAnimator.SetInteger("AnimState", 1);
        }
        else
        { 
            //fade.fadeState = Fade.FadeState.FadeIn;
            netManager.netStates = NetManager.NetStates.ReelOut;
            boatState = BoatState.Catching;
        }
        
    }

    float timer = 3f;
    bool ReelInFlag = false;
    private void ReelIn()
    {
        print("Boat Reeling In State");
        

        if(!ReelInFlag)
        {
            timer = 3f;
            ReelInFlag = true;
        }

        if(timer >= 0f)
        {
            timer -= Time.deltaTime;
            boatAnimator.SetInteger("AnimState", 3);
        }
        else
        {
            // End the animation loop.
            boatState = BoatState.Off;

            //fade.fadeState = Fade.FadeState.FadeOut;

            // Select a new time before going into off state.
            randTimer = Random.Range(minWaitTime, maxWaitTime);
            randTimer = 0f;

            ReelInFlag = false;
        }
    }


    private void Catching()
    {
        // Has the net functionality stopped?
        if(netManager.reelingInEnded)
        {
            boatState = BoatState.ReelIn;
        }
            // Run the catching animation
            boatAnimator.SetInteger("AnimState", 2);
        
    }


    private void Off()
    {
        if(randTimer >= 0f)
        {
            randTimer -= Time.deltaTime;
            boatAnimator.SetInteger("AnimState", 0); // Set to idle animation.
            fade.fadeState = Fade.FadeState.Off;
        }
        else
        {
            print("Boat state is reel out");
            boatState = BoatState.ReelOut;
        }
    }



}
