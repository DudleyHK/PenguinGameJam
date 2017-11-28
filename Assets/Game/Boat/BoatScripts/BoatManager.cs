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
    public Directions direction = Directions.Right;

   // public NetManager netManager;
    public Animator boatAnimator;
    //public Fade fade;

    public BoatEvent boatEvent;

    public float absVelX = 0f;
    public float moveSpeed = 10f;
    public float leftPoint = 0f;
    public float rightPoint = 0f;
    public float minWaitTime = 5f;
    public float maxWaitTime = 15f;
    public float randTimer = 0f;
    public bool  timerSelected = false;

    private InputState inputState;
    private Rigidbody2D body2D;

    private void Start()
    {
        randTimer = Random.Range(minWaitTime, maxWaitTime);
        //netManager = GetComponent<NetManager>();
        boatAnimator = GetComponent<Animator>();
        inputState = GetComponent<InputState>();
        body2D = GetComponent<Rigidbody2D>();
    }



    private void Update()
    {
        // If the boat is ready to release the line.
        if(boatEvent.isReady)
        {
            // If the boat is not currently active and they havent set the timer.
            if(!timerSelected && !boatEvent.release && !boatEvent.reelIn)
            {
                print("selecting random time");
                randTimer = Random.Range(minWaitTime, maxWaitTime);
                timerSelected = true;
            }

            // Start the count down for releasing the net.
            if(randTimer >= 0f)
            {
                randTimer -= Time.deltaTime;
            }
            else
            {
                boatEvent.release = true;
            }

            // Stop the boat while the boat is doing actions.
            if(boatEvent.release || boatEvent.reelIn)
            {
                StopBoatMovement();
            }
            else
            {
            StartBoatMovement();

            }

        }
        else
        {
            timerSelected = false;
        }
    }


    private void FixedUpdate()
    {
        absVelX = Mathf.Abs(body2D.velocity.x);
    }


    private void StartBoatMovement()
    {
        print("Boat moving");
        // Select a direction of travel.
        if(transform.position.x <= leftPoint)
        {
            direction = Directions.Right;
        }

        if(transform.position.x >= rightPoint)
        {
            direction = Directions.Left;
        }

        // move towards direction of travel.
        switch(direction)
        {
            case Directions.Right:
                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    new Vector3(rightPoint, transform.position.y, transform.position.z), 
                    moveSpeed * Time.deltaTime);
                break;

            case Directions.Left:
                transform.position = Vector3.MoveTowards(
                 transform.position,
                 new Vector3(leftPoint, transform.position.y, transform.position.z),
                 moveSpeed * Time.deltaTime);
                break;
        }
        // Flip the sprite
        transform.localScale = new Vector3(-(float)direction * 0.5f, 0.5f, 0.5f);



    }


    private void StopBoatMovement()
    {
       print("Boat stopped");
       absVelX = 0f;
    }















    /// ///////////////////////////////////////////////////////  UNUSED CODE /////////////////////////////////////////////////////



    //private void RunBoatEvent()
    //{
       





    //    //switch(boatState)
    //    //{
    //    //    case BoatState.ReelOut:  ReelOut();   break;
    //    //    case BoatState.ReelIn:   ReelIn();    break;
    //    //    case BoatState.Catching: Catching();  break;
    //    //    case BoatState.Off:      Off();       break;
    //    //}
    //}

    //float reelOutTimer = 3f;
    //bool ReelOutFlag = false;
    //private void ReelOut()
    //{
    //    if(!ReelOutFlag)
    //    {
    //        reelOutTimer = 3f;
    //        ReelOutFlag = true;
    //    }

    //    if(reelOutTimer >= 0f)
    //    {
    //        // Set the boat animation to reel out. 
    //        reelOutTimer -= Time.deltaTime;
    //        boatAnimator.SetInteger("AnimState", 1);
    //    }
    //    else
    //    { 
    //        //fade.fadeState = Fade.FadeState.FadeIn;
    //      //  netManager.netStates = NetManager.NetStates.ReelOut;
    //        boatState = BoatState.Catching;
    //    }
        
    //}

    //float timer = 3f;
    //bool ReelInFlag = false;
    //private void ReelIn()
    //{
    //    print("Boat Reeling In State");
        

    //    if(!ReelInFlag)
    //    {
    //        timer = 3f;
    //        ReelInFlag = true;
    //    }

    //    if(timer >= 0f)
    //    {
    //        timer -= Time.deltaTime;
    //        boatAnimator.SetInteger("AnimState", 3);
    //    }
    //    else
    //    {
    //        // End the animation loop.
    //        boatState = BoatState.Off;

    //        //fade.fadeState = Fade.FadeState.FadeOut;

    //        // Select a new time before going into off state.
    //        randTimer = Random.Range(minWaitTime, maxWaitTime);
    //        randTimer = 0f;

    //        ReelInFlag = false;
    //    }
    //}


    //private void Catching()
    //{
    //    // Has the net functionality stopped?
    //    //if(netManager.reelingInEnded)
    //   // {
    //        boatState = BoatState.ReelIn;
    //   // }
    //        // Run the catching animation
    //        boatAnimator.SetInteger("AnimState", 2);
        
    //}


    //private void Off()
    //{
    //    if(randTimer >= 0f)
    //    {
    //        randTimer -= Time.deltaTime;
    //        boatAnimator.SetInteger("AnimState", 0); // Set to idle animation.
    //        //fade.fadeState = Fade.FadeState.Off;
    //    }
    //    else
    //    {
    //        print("Boat state is reel out");
    //        boatState = BoatState.ReelOut;
    //    }
    //}



}
