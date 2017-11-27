using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WaterLayer : MonoBehaviour 
{
    public List<string> allowedTags = new List<string>(new string[] 
    { 
        "Player"    
    });

    public List <GameObject> objectsInLayer = new List<GameObject>();



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!allowedTags.Contains(other.tag)) return;

        objectsInLayer.Add(other.gameObject);
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if(!allowedTags.Contains(other.tag))
            return;

        objectsInLayer.Remove(other.gameObject);
    }



}
