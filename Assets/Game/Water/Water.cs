using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static GameObject waterObject; 
    public static Transform waterTransform;
    public static Vector2 waterTopCentre;
    public static Vector2 waterOrigin;
    public static List<WaterLayer> waterLayers = new List<WaterLayer>(3);


	private void Start ()
    {
		waterTransform = this.transform;
        waterObject = this.transform.gameObject;

        waterLayers.Add(GameObject.Find("Layer1").GetComponent<WaterLayer>());
        waterLayers.Add(GameObject.Find("Layer2").GetComponent<WaterLayer>());
        waterLayers.Add(GameObject.Find("Layer3").GetComponent<WaterLayer>());

    }
	
	// Update is called once per frame
	private void Update ()
    {
        var waterCentreX = transform.position.x + (transform.lossyScale.x / 2f);
        var waterCentreY = transform.position.y - (transform.lossyScale.y / 2f);
        waterOrigin = new Vector2(waterCentreX, waterCentreY);
        waterTopCentre = new Vector2(waterCentreX, transform.position.y);
    }

    /// <summary>
    /// Return true is the position is above the water.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static bool AboveWater(Vector2 position)
    {
        if(position.y >= waterOrigin.y + (waterTransform.lossyScale.y / 2f))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Return (index + 1) so its human readable.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static int LayerObjectsIn(GameObject obj)
    {
        if(!obj || obj.tag == "")
        {
            Debug.Log("Object " +  obj.tag + " is null or does not have a tag set");
            return -1;
        }

        // Run through each water layer and check if the player is in the list.
        for(int i = 0; i < waterLayers.Count; i++)
        {
            var findObject = waterLayers[i].objectsInLayer.Find(p => { return p.tag == obj.tag; });
            if(findObject)
            {
                Debug.Log("Object " + obj.tag + " is in layer " + i);
                return (i + 1);
            }
        }

        Debug.Log("Object " + obj.tag + " is not in the water");
        return -1;
    }
}
