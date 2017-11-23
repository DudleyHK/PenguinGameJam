using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceburgManager : MonoBehaviour 
{
    // Get the centre of the Iceburg
    // Place a node the iceburgs size + the iceburgs position above.
    // Split the water into lanes.

    public ushort numberOfLanes = 0;
    public Dictionary<int, GameObject> iceburgList = new Dictionary<int, GameObject>();
    public List<float> lanes = new List<float>();
    public RectTransform upperWaterRectTransform;
    public float laneWidth = 0f;

    private float width = 0f;
    private Vector3 topLeft;

    public GameObject debugObj;




    private void Start()
    {
        width = upperWaterRectTransform.localScale.x;
        topLeft = upperWaterRectTransform.anchoredPosition3D;

        laneWidth = width / numberOfLanes;


        float laneOriginX = topLeft.x;

        // One extra for the end lane position.
        for(var i = 0; i < (numberOfLanes + 1); i++)
        {
            var clone = Instantiate(debugObj, new Vector3(laneOriginX, topLeft.y, topLeft.z), Quaternion.identity);
            lanes.Add(laneOriginX);
            laneOriginX += laneWidth;
        }

        var iceburgs = GameObject.FindGameObjectsWithTag("Iceburg");
        AddIceburg(iceburgs);
    }


    public void AddIceburg(GameObject iceburg) { AddIceburg(new GameObject[] { iceburg });}
    public void AddIceburg(GameObject[] iceburgs)
    {
        // add the iceburg to the list
        foreach(var iceburg in iceburgs)
        {
            var lane = GetLane(iceburg.transform.position);
            iceburgList.Add(lane, iceburg);
            //Debug.Log("Iceburg at position " + iceburg.transform.position + " at the lane of " + lane);
        }
    }


    public int GetLane(Vector2 position)
    {
        int prevID = -1;
        for(var i = 0; i < lanes.Count; i++)
        {
            if(position.x > lanes[i])
            {
                prevID = i;
            }
            else
            {
                return (prevID + 1);
            }
        }
        Debug.Log("ERROR: Invaild lane (" + (prevID + 1) + ")");
        return -1;

    }


    public Vector2 GetIceburgJumpNode(int lane)
    {
        var iceburg = iceburgList[lane];
        if(iceburg == null)
        {
            Debug.Log("No iceburg in lane " + lane);
            return Vector2.zero;
        }

        var jumpNodeY = iceburg.transform.position.y + iceburg.transform.localScale.y;
       // var clone = Instantiate(debugObj, new Vector2(iceburg.transform.position.x, jumpNodeY), Quaternion.identity);
        return new Vector2(iceburg.transform.position.x, jumpNodeY);
        
    }
}
