using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : MonoBehaviour
{

    public enum NetStates
    {
        Off,
        ReelIn,
        ReelOut,
        Catching
    }
    public bool startFishing = false;
    public NetStates netStates = NetStates.Off;
    public GameObject net;
    public Rigidbody2D netBody2D;
    public List<string> pullTags = new List<string>();
    public float dropDistance = -150f;
    public float speed = 5f;
    public float distanceBreak = 0.5f;
    public float radiusOfEffect = 5f;
    public float pullForce = 9.8f;
    public float minCatchTime = 5f;
    public float maxCatchTime = 15f;
    public float randTimer = 0f;
    public bool randTimeSelected = false;



    private void Start()
    {
        net.SetActive(false);
    }


    private void Update()
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
                    startFishing = false;
                }
                else
                {
                    net.SetActive(false);
                }

                break;

            case NetStates.ReelOut:
                // Net drop down
                net.transform.position = Vector2.MoveTowards(net.transform.position, new Vector2(net.transform.position.x, dropDistance), speed * Time.deltaTime);
                if((Mathf.Abs(dropDistance - net.transform.position.y)) <= distanceBreak)
                {
                    netStates = NetStates.Catching;
                }

                break;


            case NetStates.ReelIn:
                // Pull net in
                net.transform.position = Vector2.MoveTowards(net.transform.position, new Vector2(net.transform.position.x, Water.waterTopCentre.y), speed * Time.deltaTime);
                if((Mathf.Abs(Water.waterTopCentre.y - net.transform.position.y)) <= distanceBreak)
                {
                    netStates = NetStates.Off;
                }
                break;



            case NetStates.Catching:
                // Selected a random time to stay down.
                if(!randTimeSelected)
                {
                    randTimer = Random.Range(minCatchTime, maxCatchTime);
                    randTimeSelected = true;
                }


                // start appling catching physics.
                CatchPhysics();


                // Decrement the timer.
                if(randTimer >= 0f)
                {
                    randTimer -= Time.deltaTime;
                }
                else
                {
                    netStates = NetStates.ReelIn;
                    randTimeSelected = true;
                    randTimer = 0f;
                }
                break;

            default:
                break;
        }
    }

    private void CatchPhysics()
    {
        // Get all the objects in range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(net.transform.position.x, net.transform.position.y), radiusOfEffect);


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

                Debug.Log("Direction from " + col.name + " to the net is " + direction);
                Debug.DrawRay(net.transform.position, direction, Color.green);

                var distPull = (pullForce / distance);
                // Debug.Log("Distance between is " + distance);
                // Debug.Log("Distance Pull factor is " + distPull);

                col.attachedRigidbody.velocity += direction * distPull * Time.deltaTime;
            }
        }
    }
}
