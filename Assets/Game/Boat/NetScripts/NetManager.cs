using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NetManager : MonoBehaviour
{
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
    public SpriteRenderer netRenderer;
    public float speed = 5f;
    public float distanceBreak = 0.5f;

    public float radiusOfEffect = 5f;
    public float pullForce = 9.8f;
    public int centreStick = 50;

    private float minCatchTime = 5f;
    private float maxCatchTime = 15f;
    private float randTimer = 0f;
    public bool randTimeSelected = false;
    public bool reelingInEnded = false;


    private Animator netAnimator;
    private CircleCollider2D netCollider2D;
    private float leftStart = -305f;
    private float rightStart = 295f;
    private bool startFishing = false;
    private float dropDistance = -540f;


    private void Start()
    {
        net.SetActive(false);
        netCollider2D = net.GetComponent<CircleCollider2D>();
        netAnimator = net.GetComponent<Animator>();
        netRenderer = net.GetComponent<SpriteRenderer>();
}


    private void Update()
    {
        ManageNetStates();
    }
    

    private void ManageNetStates()
    {
        switch(netStates)
        {
            case NetStates.Off:      Off();      break;
            case NetStates.ReelOut:  ReelOut();  break;
            case NetStates.ReelIn:   ReelIn();   break;
            case NetStates.Catching: Catching(); break;
        }
    }




    private void ReelOut()
    {
        if(!startFishing)
        {
            var xStart = Random.Range(leftStart, rightStart);
            net.transform.position = new Vector2(xStart, net.transform.position.y);
            net.SetActive(true);
            startFishing = true;
        }

        // Net drop down
        net.transform.position = Vector2.MoveTowards(net.transform.position, new Vector2(net.transform.position.x, dropDistance), speed * Time.deltaTime);
        if((Mathf.Abs(dropDistance - net.transform.position.y)) <= distanceBreak)
        {
            netStates = NetStates.Catching;
        }

        netAnimator.SetInteger("AnimState", 1);

    }


    private void ReelIn()
    {
        net.transform.position = Vector2.MoveTowards(net.transform.position, new Vector2(net.transform.position.x, Water.top), speed * Time.deltaTime);
        if(net.transform.position.y >= Water.top)
        {
            netStates = NetStates.Off;

            randTimeSelected = false;
            reelingInEnded = true;
            startFishing = false;
        }
        else
        {
            reelingInEnded = false;
        }
        

        netAnimator.SetInteger("AnimState", 3);
        CatchPhysics(50, netCollider2D.radius);
    }


    private void Catching()
    {
        print("Net state is " + netStates);
        //  // Selected a random time to stay down.
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
            netAnimator.SetInteger("AnimState", 2);
        }
        else
        {
            netStates = NetStates.ReelIn;
        }
    }


    private void Off()
    {
        print("Net state is " + netStates);

        // Set to idle if the animation is active.
        if(netAnimator.gameObject.activeSelf)
        {
            netAnimator.SetInteger("AnimState", 0);
        }

        randTimer = 0f;
        net.SetActive(false);
        startFishing = false;
        
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
                    //Debug.Log("Direction from " + col.name + " to the net is " + direction);
                    Debug.DrawRay(net.transform.position, direction, Color.green);

                    var distPull = (pullForce / distance);
                    //Debug.Log("Distance between is " + distance);
                    //Debug.Log("Distance Pull factor is " + distPull);

                    col.attachedRigidbody.velocity += direction * distPull * Time.deltaTime;
                }
            }
        }
    }


}
