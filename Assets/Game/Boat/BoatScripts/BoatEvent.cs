using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatEvent : MonoBehaviour
{
    public List<string> pullTags = new List<string>();
    public GameObject net;
    public SpriteRenderer netRenderer;
    public Rigidbody2D netBody2D;
    public bool release = false;

    public float radiusOfEffect = 5f;
    public float pullForce = 9.8f;
    public int centreStick = 50;

    private Vector2 netOffset;
    private Vector3 hookPoint;


    private void Start()
    {
        netRenderer = net.GetComponent<SpriteRenderer>();
        netBody2D = net.GetComponent<Rigidbody2D>();

        SetCurrentOffset();

        hookPoint = new Vector3(
            net.transform.position.x,
            net.transform.position.y,
            net.transform.position.z);
    }

    private void Update()
    {
        if(release)
        {
            if(Water.LayerObjectsIn(net) >= 1)
            {
                netBody2D.gravityScale = 0f;
                CatchPhysics();

            }
            else
            {
                netBody2D.gravityScale = 30f;
            }
        }
        else
        {
           
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

