using System.Collections;using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour 
{
    public NetManager netManager;
    public LineRenderer line;


    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
    }


    private void Update()
    {
        // Chaneg this to be based on states
        if(netManager.netStates != NetManager.NetStates.Off)
        {
            line.SetPosition(0, this.transform.position);
            line.SetPosition(1, new Vector3(
                netManager.net.transform.position.x, 
                netManager.net.transform.position.y + (netManager.net.GetComponent<SpriteRenderer>().size.y / 2f), 
                netManager.net.transform.position.z));
            line.startColor = Color.black;
            line.endColor = Color.black;
        }
        else
        {
            //destroy line
            line.SetPosition(1, transform.position);
        }
    }





}
