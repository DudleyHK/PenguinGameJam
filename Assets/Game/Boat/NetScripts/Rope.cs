using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public BoatEvent boatEvent;

    public GameObject linePointOne;
    public GameObject linePointTwo;
    public GameObject linePointThree;


    private void Start()
    {
        
    }


    private void Update()
    {
        // Update line pos three
        var yPos = (boatEvent.net.transform.position.y + (boatEvent.netRenderer.bounds.size.y / 2f));
        var xPos = boatEvent.net.transform.position.x;

        linePointThree.transform.position = new Vector3(xPos, yPos, boatEvent.net.transform.position.z);

        // linepoint2 position is somewhere between linethree and line1

    }
}