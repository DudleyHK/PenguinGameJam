using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour {

    public bool InWater;

    Rect OxygenRect;
    Texture OxygenTex;

	// Use this for initialization
	void Start () {
        InWater = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (InWater)
        {
            
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        InWater = true;
        Debug.Log("InWater");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        InWater = false;
        Debug.Log("OutWater");
    }

}
