using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static Vector2 waterOrigin;


	private void Start ()
    {
		
	}
	
	// Update is called once per frame
	private void Update ()
    {
        var waterCentreX = transform.position.x + (transform.lossyScale.x / 2f);
        var waterCentreY = transform.position.y - (transform.lossyScale.y / 2f);
        waterOrigin = new Vector2(waterCentreX, waterCentreY);

    }
}
