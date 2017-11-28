using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public BoatManager boatManager;
    public NetManager netManager;
    public LineRenderer line;

   // public GameObject linePointTwo;
    public GameObject linePointThree;




    private void Update()
    {
        if(boatManager.boatState == BoatManager.BoatState.Off)
        {
            //linePointTwo.transform.position = transform.position;
            linePointThree.transform.position = transform.position;
        }
        else
        {
            var yPos = (netManager.net.transform.position.y + (netManager.netRenderer.bounds.size.y / 2f));
            var xPos = netManager.net.transform.position.x;

            linePointThree.transform.position = new Vector3(xPos, yPos, netManager.net.transform.position.z);
            //linePointTwo.transform.position = transform.position + linePointThree.transform.position;
            //Debug.Log("half way between line pint 1 and 3 is " + linePointTwo.transform.position);

        }
    }
}