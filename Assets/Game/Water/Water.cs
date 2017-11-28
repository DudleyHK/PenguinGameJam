using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static List<WaterLayer> waterLayers = new List<WaterLayer>(3);
    public static GameObject waterObject; 
    public static Transform waterTransform;
    public static Vector2 waterOrigin;
    public static Vector2 waterHalfSize;

    public static float left;
    public static float right;
    public static float top;
    public static float down;


    private static SpriteRenderer spriteRenderer;



	private void Start ()
    {
		waterTransform = this.transform;
        waterObject = this.transform.gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();

        waterLayers.Add(GameObject.Find("Layer1").GetComponent<WaterLayer>());
        waterLayers.Add(GameObject.Find("Layer2").GetComponent<WaterLayer>());
        waterLayers.Add(GameObject.Find("Layer3").GetComponent<WaterLayer>());
    }
	


	// Update is called once per frame
	private void Update ()
    {
        waterHalfSize.x = spriteRenderer.bounds.size.x / 2f;
        waterHalfSize.y = spriteRenderer.bounds.size.y / 2f;

        waterOrigin = spriteRenderer.bounds.center;

        left  = waterOrigin.x - waterHalfSize.x;
        right = waterOrigin.x + waterHalfSize.x;
        top   = waterOrigin.y + waterHalfSize.y;
        down  = waterOrigin.y - waterHalfSize.y;


       // print("Water origin " + spriteRenderer.bounds.center);
       // print("WaterLeft is " + left);
       // print("waterRight is " + right);
       // print("waterDown is " + down);
       // print("waterTop is " + top);
       //
    }

    /// <summary>
    /// Return true is the position is above the water.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static bool AboveWater(Vector2 position)
    {
        if(position.y >= top)
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
                Debug.Log("Object " + obj.tag + " is in layer " + (i + 1));
                return (i + 1);
            }
        }

        Debug.Log("Object " + obj.tag + " is not in the water");
        return -1;
    }
}
