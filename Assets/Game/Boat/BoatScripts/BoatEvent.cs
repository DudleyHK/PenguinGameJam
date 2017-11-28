using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatEvent : MonoBehaviour
{

    public GameObject net;
    public SpriteRenderer netRenderer;
    public Rigidbody2D netBody2D;
    public bool reelIn = false;
    public bool release = false;
    public bool isReady = false;
    public bool randTimeSelected = false;

    public float speed = 5f;
    public float radiusOfEffect = 5f;
    public float pullForce = 9.8f;
    public float randTimer = 0f;
    private float minCatchTime = 5f;
    private float maxCatchTime = 15f;
    public int centreStick = 50;

    private Vector2 netOffset;
    private Vector3 hookPoint;

    private List<string> pullTags = new List<string>(new string[]
    {
       "Player"
    });


    private void Start()
    {
        netRenderer = net.GetComponent<SpriteRenderer>();
        netBody2D = net.GetComponent<Rigidbody2D>();

        SetCurrentOffset();

        hookPoint = new Vector3(
            net.transform.position.x,
            net.transform.position.y,
            net.transform.position.z);

        isReady = true;
    }

    private void Update()
    {
        if(release)
        {
            print("Released");
            if(Water.LayerObjectsIn(net) >= 1)
            {
                netBody2D.gravityScale = 0f;
                CatchPhysics();
                if(StartTimer())
                {
                    release = false;
                }
            }
            else
            {
                netBody2D.gravityScale = 30f;
            }
        }

        if(reelIn)
        {
            netBody2D.gravityScale = 0f;
            StartCoroutine(ReelIn(complete =>
            {
                if(complete)
                {
                    isReady = true;
                    reelIn = false;
                }
                else
                {
                    isReady = false;
                }
            }));
        }

        SetCurrentOffset();
    }


    private void SetCurrentOffset()
    {
        var yPos = (net.transform.position.y + (netRenderer.bounds.size.y / 2f));
        var xPos = net.transform.position.x;
        netOffset = new Vector2(xPos, yPos);
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

            // if the tag is allowed to be dragged
            if(pullTags.Contains(col.tag))
            {
                //Debug.Log("Object near to net is " + col.tag);
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
                    //Debug.Log("Direction from " + col.name + " to the net is " + direction);
                    Debug.DrawLine(net.transform.position, col.transform.position, Color.green);

                    var distPull = (pullForce / distance);
                   // Debug.Log("Direction " + direction);
                   // Debug.Log("distPull " + distPull);
                   // Debug.Log("Pull factor is " + (direction * distPull * Time.deltaTime));

                    col.attachedRigidbody.velocity += direction * distPull * Time.deltaTime;
                }
            }
        }
    }

    private bool StartTimer()
    {
        // Init timer.
        if(!randTimeSelected)
        {
            randTimer = UnityEngine.Random.Range(minCatchTime, maxCatchTime);
            randTimeSelected = true;
        }

        // Decrement the timer.
        if(randTimer <= 0f)
        {
            reelIn = true;
            randTimeSelected = false;
            return true;
        }

        reelIn = false;
        randTimer -= Time.deltaTime;
        return false;
    }


    private IEnumerator ReelIn(Action<bool> complete)
    {
        while(true)
        {
            net.transform.position = Vector2.MoveTowards(net.transform.position, new Vector2(net.transform.position.x, hookPoint.y), speed * Time.deltaTime);
            if(net.transform.position.y >= hookPoint.y)
            {
                break;
            }
            complete(false);
            yield return false;
        }
        complete(true);
        yield return true;
    }
}

