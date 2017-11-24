using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IceburgFloating : MonoBehaviour {

    //Create array of all things touchin iceberg
    [SerializeField]
    List<GameObject> collidingPlayers = new List<GameObject> ();

    //Angular Physics
    [SerializeField]
    float angularVelocity, angularAcceleration, speedMultiplyer, slowDownSpeed;

	// Use this for initialization
	void Start ()
    {
		
	}
	

	// Update is called once per frame
	void FixedUpdate ()
    {
		if(collidingPlayers.Count > 0)
        {
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            //Make it turn based off whichever mass causes more force in the given direction
            foreach (GameObject guyOnIce in collidingPlayers)
            {
                if(transform.position.x > guyOnIce.transform.position.x)
                {
                    angularAcceleration += guyOnIce.GetComponent<PlayerData>().mass * Vector2.Distance(this.transform.position, guyOnIce.transform.position);
                }
                else if( guyOnIce.transform.position.x > transform.position.x)
                {
                    angularAcceleration -= guyOnIce.GetComponent<PlayerData>().mass * Vector2.Distance(this.transform.position, guyOnIce.transform.position);
                }
                else
                {
                    Debug.Log("Equilibrium ting");
                }

            }

            angularVelocity += angularAcceleration * Time.deltaTime * speedMultiplyer;
            this.GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;

        }
        else
        {
            transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            transform.GetComponent<Rigidbody2D>().angularDrag = Mathf.Cos(this.transform.eulerAngles.z) + slowDownSpeed * slowDownSpeed;
            //Debug.Log(Mathf.Cos(this.transform.localRotation.z) + slowDownSpeed * slowDownSpeed);
        }

        

        

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


//    if (180 - angleOfResistance < this.transform.eulerAngles.z &&
//        180 + angleOfResistance > this.transform.eulerAngles.z)
//    {

//        if (180 - angleOfResistance < this.transform.eulerAngles.z && 
//            180 > this.transform.eulerAngles.z
//            //&& angularVelocity > slowDownSpeed
//            )
//        {
//            Debug.Log("aaa");

//            angularAcceleration = slowDownSpeed * (transform.eulerAngles.z - 180 / angleOfResistance);
//            Debug.Log("Angle = " + transform.eulerAngles.z);
//            Debug.Log("Angle of RAngle is " + (transform.eulerAngles.z / angleOfResistance));
//            Debug.Log("Acceleration is " + angularAcceleration);


//        }
//        else if (180 + angleOfResistance > this.transform.eulerAngles.z &&
//            180 < this.transform.eulerAngles.z
//            // && angularVelocity < slowDownSpeed
//            )
//        {
//            Debug.Log("bbb");

//            angularAcceleration = -slowDownSpeed * (transform.eulerAngles.z / angleOfResistance);
//            Debug.Log("Angle = " + transform.eulerAngles.z);
//            Debug.Log("Angle of RAngle is " + (transform.eulerAngles.z / angleOfResistance));
//            Debug.Log("Acceleration is " + angularAcceleration);

//        }
//    }

//    else if(0 + angleOfResistance > this.transform.eulerAngles.z )
//    {
//        if (0 - angleOfResistance < this.transform.eulerAngles.z //&&
//            //0 < this.transform.eulerAngles.z 
//            //&& angularVelocity < slowDownSpeed
//            )
//        {
//            Debug.Log("ccc");

//            angularAcceleration = slowDownSpeed * 10 *(transform.eulerAngles.z  / angleOfResistance);
//            Debug.Log("Angle = " + transform.eulerAngles.z);
//            Debug.Log("Angle of RAngle is " + (transform.eulerAngles.z / angleOfResistance));
//            Debug.Log("Acceleration is " + angularAcceleration);

//        }


//    }

//    else if (360 - angleOfResistance < this.transform.eulerAngles.z)
//    {
//        {
//            Debug.Log("ddd");

//            angularAcceleration = slowDownSpeed*  (transform.eulerAngles.z / angleOfResistance);
//            Debug.Log("Angle = " + transform.eulerAngles.z);
//            Debug.Log("Angle of RAngle is " + (transform.eulerAngles.z / angleOfResistance));
//            Debug.Log("Acceleration is " + angularAcceleration);

//        }
//    }

//    //Debug.Log(transform.eulerAngles.z);
//    //Debug.Log(transform.localRotation.z);