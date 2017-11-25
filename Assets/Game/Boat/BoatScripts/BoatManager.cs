using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BoatManager : MonoBehaviour 
{
    public enum BoatStates
    {
        Idle = 0,
        ReelOut = 1,
        ReelIn = 2,
        Catching = 3
    }
    public BoatStates boatStates = BoatStates.Idle;
    public NetManager netManager;
    public Animator boatAnimator;

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
                netManager.startFishing = true;
            }

            // Select a new wait time. 
            randTimer = Random.Range(minWaitTime, maxWaitTime);
        }
    }


    public void SetBoatAnimation()
    {
        switch(boatStates)
        {
            case BoatStates.Idle:
                boatStates = BoatStates.ReelOut;
                break;

            case BoatStates.ReelOut:
                boatStates = BoatStates.Catching;
                break;

            case BoatStates.ReelIn:
                boatStates = BoatStates.Idle;
                break;

            case BoatStates.Catching:
                boatStates = BoatStates.ReelIn;
                break;
        }
        boatAnimator.SetInteger("AnimState", (int)boatStates);
    }






}
