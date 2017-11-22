using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceburgFloating : MonoBehaviour {

    //Create array of all things touchin iceberg
    [SerializeField]
    List<GameObject> collidingPlayers = new List<GameObject> ();

    //Point where tippage will occur
    [SerializeField]
    Transform centrePoint;

    //Angular Physics
    [SerializeField]
    float angularVelocity, angularAcceleration, speedMultiplyer, angleOfResistance, slowDownSpeed;

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
            foreach (GameObject guyOnIce in collidingPlayers)
            {
                if(centrePoint.position.x > guyOnIce.transform.position.x)
                {
                    angularAcceleration += guyOnIce.GetComponent<PlayerData>().mass * Vector2.Distance(centrePoint.position, guyOnIce.transform.position);
                }
                else if( guyOnIce.transform.position.x > centrePoint.position.x)
                {
                    angularAcceleration -= guyOnIce.GetComponent<PlayerData>().mass * Vector2.Distance(centrePoint.position, guyOnIce.transform.position);
                }
                else
                {
                    Debug.Log("Equilibrium ting");
                }

            }

        }
        else
        {      
            if(180 - angleOfResistance < this.transform.eulerAngles.z  
                &&  180 + angleOfResistance > this.transform.eulerAngles.z
                || 0 - angleOfResistance < this.transform.eulerAngles.z
                && 0 + angleOfResistance > this.transform.eulerAngles.z)
            {
                Debug.Log(transform.eulerAngles.z);
                angularVelocity = 
                //angularVelocity *= slowDownSpeed * Time.deltaTime;
            }

        }

        angularVelocity += angularAcceleration * Time.deltaTime * speedMultiplyer;

        this.GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;

        angularAcceleration = 0;

    }


    //When a player touches the ber, it'll be added to list of touching objects
    void OnCollisionEnter2D(Collision2D col)
    {
        collidingPlayers.Add(col.gameObject);
    }



    //Object will bepulled when it stops touching
    void OnCollisionExit2D(Collision2D col)
    {
        collidingPlayers.Remove(col.gameObject);
    }

}
