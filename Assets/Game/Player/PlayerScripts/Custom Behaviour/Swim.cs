using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : Fly
{
    public GameObject water;
    public IceburgManager iceburgManager;
    public bool underWater = false;
    public bool applyBoost = false;
    public float hopSpeed = 10f;
    public float teleportSpeed = 3f;
    public float distanceBreak = 0.75f;


    private void Start()
    {
        if(water == null)
        {
            var findWater = GameObject.FindGameObjectWithTag("Water");
            water = findWater;
        }
    }


    protected override void Update()
    {
        base.Update();

		if (playerData.PlayerState != PlayerData.PlayerStates.GROUNDED)
		{
			HasSurfaced ();
		} 
		else 
		{
			StartCoroutine(playerData.PlayNext(playerData.swimSound));
		}
    }

    /// <summary>
    /// Has the player hit the top of the ocean.
    /// </summary>
    private void HasSurfaced()
    {
        // Is the player above the water?
        if (Water.AboveWater(transform.position))
        {
          // Debug.Log("Player out or at top of water.");

            if(playerData.PlayerState == PlayerData.PlayerStates.SWIMMING)
            {
                // check which lane the player is in. 
                var laneNumber = iceburgManager.GetLane(transform.position);

                // is there a iceburg in that lane?
                var targetNode = iceburgManager.GetIceburgJumpNode(laneNumber);
                if(targetNode == Vector2.zero)
                {
                    Debug.Log("TODO:: Jump out of water animation");
                }
                else
                {
                    StartCoroutine(TeleportToPlatform(targetNode));
                }
            }
            underWater = false;
        }
        else
        {
           // applyBoost = false;
            underWater = true;
        }
    }


    private IEnumerator TeleportToPlatform(Vector2 targetNode)
    {
        collisionState.collisionBox.enabled = false;
        //body2D.bodyType = RigidbodyType2D.Kinematic;
        while(true)
        {
            transform.position = Vector3.LerpUnclamped(transform.position, targetNode, teleportSpeed * Time.deltaTime);
            
            if(Vector2.Distance(transform.position, targetNode) <= distanceBreak) break;

            yield return false;
        }

        collisionState.collisionBox.enabled = true;
        //body2D.bodyType = RigidbodyType2D.Dynamic;
        yield return true;
    }
}
