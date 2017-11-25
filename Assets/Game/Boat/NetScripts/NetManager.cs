﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoBehaviour
{
    public bool startFishing = false;
    public enum NetStates
    {
        Off,
        ReelOut,
        ReelIn,
        Catching
    }
    public NetStates netStates = NetStates.Off;
    public GameObject net;
    public List<string> pullTags = new List<string>();
    public float dropDistance = -150f;
    public float speed = 5f;
    public float distanceBreak = 0.5f;
    public float radiusOfEffect = 5f;
    public float pullForce = 9.8f;
    public int centreStick = 50;
    public float minCatchTime = 5f;
    public float maxCatchTime = 15f;
    public float randTimer = 0f;
    public bool randTimeSelected = false;

    private Rigidbody2D netBody2D;
    private CircleCollider2D netCollider2D;
    private Animator netAnimator;


    private void Start()
    {
        net.SetActive(false);
        netBody2D = net.GetComponent<Rigidbody2D>();
        netCollider2D = net.GetComponent<CircleCollider2D>();
        netAnimator = net.GetComponent<Animator>();
    }


    private void Update()
    {
        ManageNetStates();
    }




    private void ManageNetStates()
    {
        switch(netStates)
        {
            case NetStates.Off:
                // In boat
                if(startFishing)
                {
                    netStates = NetStates.ReelOut;

                    // Select a random position along the x axis of the water.
                    var xStart = Random.Range(Water.waterTransform.position.x, (Water.waterOrigin.x + Water.waterTransform.lossyScale.x / 2f));
                    net.transform.position = new Vector2(xStart, net.transform.position.y);

                    net.SetActive(true);
                }
                else
                {
                    // Do this here so it only does it once before the startFishing flag is turned off.
                    if(startFishing)
                    {
                        netAnimator.SetInteger("AnimState", 0);
                    }
                    // This has to come after this if statment.
                    net.SetActive(false);
                    
                }

                break;

            case NetStates.ReelOut:
                // Net drop down
                net.transform.position = Vector2.MoveTowards(net.transform.position, new Vector2(net.transform.position.x, dropDistance), speed * Time.deltaTime);
                if((Mathf.Abs(dropDistance - net.transform.position.y)) <= distanceBreak)
                {
                    netStates = NetStates.Catching;
                    netAnimator.SetInteger("AnimState", 1);
                }

                break;


            case NetStates.ReelIn:
                // Pull net in
                net.transform.position = Vector2.MoveTowards(net.transform.position, new Vector2(net.transform.position.x, Water.waterTopCentre.y), speed * Time.deltaTime);
                if((Mathf.Abs(Water.waterTopCentre.y - net.transform.position.y)) <= distanceBreak)
                {
                    netStates = NetStates.Off;
                }

                CatchPhysics(50, netCollider2D.radius);
                break;



            case NetStates.Catching:
                // Selected a random time to stay down.
                if(!randTimeSelected)
                {
                    randTimer = Random.Range(minCatchTime, maxCatchTime);
                    randTimeSelected = true;
                }


                // start appling catching physics.
                CatchPhysics(1, radiusOfEffect);


                // Decrement the timer.
                if(randTimer >= 0f)
                {
                    randTimer -= Time.deltaTime;
                }
                else
                {
                    netStates = NetStates.ReelIn;
                    netAnimator.SetInteger("AnimState", 2);
                    startFishing = false;
                    randTimeSelected = true;
                    randTimer = 0f;
                }
                break;
        }
    }

    private void CatchPhysics(int pullSpeed, float radius)
    {
        // Get all the objects in range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(net.transform.position.x, net.transform.position.y), radius);


        foreach(var col in colliders)
        {
            if(col.gameObject == net)
                continue;

            if(pullTags.Count <= 0)
                Debug.Log("PullTags list has no Tags in.. Fill the list with objects which can be pulled towards the net");

            // Debug.Log("Object near to net is " + col.tag);
            // if the tag is allowed to be dragged
            if(pullTags.Contains(col.tag))
            {
                Vector2 heading = net.transform.position - col.transform.position;
                float distance = heading.magnitude;
                Vector2 direction = heading / distance;

                if(distance <= 5f)
                {
                    // Inside
                    col.transform.position = Vector2.MoveTowards(col.transform.position, new Vector2(net.transform.position.x, net.transform.position.y), centreStick * Time.deltaTime);

                }
                else
                {
                    Debug.Log("Direction from " + col.name + " to the net is " + direction);
                    Debug.DrawRay(net.transform.position, direction, Color.green);

                    var distPull = (pullForce / distance);
                    Debug.Log("Distance between is " + distance);
                    Debug.Log("Distance Pull factor is " + distPull);

                    col.attachedRigidbody.velocity += direction * distPull * Time.deltaTime;
                }
            }
        }
    }


}
