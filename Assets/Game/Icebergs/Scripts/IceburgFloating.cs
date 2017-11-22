using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceburgFloating : MonoBehaviour {

    //Create array of all things touchin iceberg
    List<GameObject> collidingPlayers = new List<GameObject> ();

    //Point where tippage will occur
    [SerializeField]
    Transform centrePoint;

    //Angular Physics
    float angularVelocity, angularAcceleration;

	// Use this for initialization
	void Start ()
    {
		
	}
	

	// Update is called once per frame
	void FixedUpdate ()
    {
		if(collidingPlayers.Count > 0)
        {
            
            //Make it turn based off whichever mass causes more force in the given direction



        }
        else
        {
            //turn back
        }
	}


    //When a player touches the ber, it'll be added to list of touching objects
    void OnCollisionEnter(Collision col)
    {
        collidingPlayers.Add(col.gameObject);
    }



    //Object will bepulled when it stops touching
    void OnCollisionExit(Collision col)
    {
        collidingPlayers.Remove(col.gameObject);
    }

}
