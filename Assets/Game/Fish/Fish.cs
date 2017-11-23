using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
    
    Transform fishTransform;
    private SpriteRenderer fishSpriteRenderer;

    float maxXPos = 190f;
    float minXPos = -190f;

    [SerializeField]
    float fishSpeed = 12f;

    Rigidbody2D fishBody;

    [SerializeField]
    bool fishWay;

	// Use this for initialization
	void Start ()
    {
        fishTransform = this.transform;
        fishBody = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {       
        // Always move foward
		Vector2 fishVel = fishBody.velocity;
		if (fishWay) {
			// Move right if left is true
			fishVel.x = fishSpeed;
		}
		else if (!fishWay) {
			// Move left if left is false
			fishVel.x = -fishSpeed;
		}
		fishBody.velocity = fishVel;

	}

	public void setDirectionLeft(bool _boolean)
	{
		fishWay = _boolean;
	}

	public bool getDirectionLeft()
	{
		return fishWay;
	}

    public void setFishSpeed(int _speed)
    {
        fishSpeed = _speed;
    }

	void OnTriggerExit2D(Collider2D other)
	{
		//Once left water go back
		if (other.tag == "Water") 
		{	
			fishTransform.position = fishTransform.position - new Vector3 (520f, 0f, 0f);
		}
	}
}
