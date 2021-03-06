﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionState : MonoBehaviour 
{
	public LayerMask collisionLayer;
	public bool standing;
	public bool onWall;
	public Vector2 bottomPosition = Vector2.zero;
	public Vector2 rightPosition = Vector2.zero;
	public Vector2 leftPosition = Vector2.zero;
	public float collisionRadius = 10f;
	public Color debugCollisionColour = Color.red;

    public Collider2D collisionBox;
	private InputState inputState;
    private KillerSeal killa;
    public GameObject dead;


	private void Awake () 
	{
		inputState = GetComponent<InputState> ();
        collisionBox = GetComponent<Collider2D>();
        killa = FindObjectOfType<KillerSeal>();
	}


	
	private void FixedUpdate () 
	{
		var pos = bottomPosition;
		pos.x += transform.position.x;
		pos.y += transform.position.y;

		standing = Physics2D.OverlapCircle(pos, collisionRadius, collisionLayer);


		pos = inputState.direction == Directions.Right ? rightPosition : leftPosition;
		pos.x += transform.position.x;
		pos.y += transform.position.y;

		onWall = Physics2D.OverlapCircle (pos, collisionRadius, collisionLayer);

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = debugCollisionColour;

		var positions = new Vector3[] { rightPosition, leftPosition, bottomPosition};

		foreach (var position in positions) 
		{
			var pos = position;
			pos.x += transform.position.x;
			pos.y += transform.position.y;
			Gizmos.DrawWireSphere (pos, collisionRadius);
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KillerWhale")
        {
            if (killa.returnAttacking())
            {
                Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                Destroy(gameObject);
                Instantiate(dead, this.transform.position, this.transform.rotation);
            }
        }
    }
}
