using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
    
    Transform fishTransform;
    private SpriteRenderer fishSpriteRenderer;

    float maxXPos = 190f;
    float minXPos = -190f;

    float fishSpeed = 12f;

    Rigidbody2D fishBody;

	// Use this for initialization
	void Start ()
    {
        fishTransform = this.transform;
        fishBody = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {       
        // Check to see if the fish has hit the boundaries
        // If it has hit the right boundary, teleport to the left side
        //if (fishTransform.position.x >= maxXPos)
        //{
        //  fishTransform.position = fishTransform.position - new Vector3 (380f, 0f, 0f);
        //}

        // Always move foward
        Vector2 fishVel = fishBody.velocity;
        fishVel.x = fishSpeed;
        fishBody.velocity = fishVel;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Water") 
		{
			fishTransform.position = fishTransform.position - new Vector3 (520f, 0f, 0f);
		}
	}
}
