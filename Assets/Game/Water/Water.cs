using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static GameObject waterObject; 
    public static Transform waterTransform;
    public static Vector2 waterTopCentre;
    public static Vector2 waterOrigin;


	private void Start ()
    {
		waterTransform = this.transform;
        waterObject = this.transform.gameObject;
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
}
