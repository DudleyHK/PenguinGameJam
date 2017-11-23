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

        if(playerData.playerState != PlayerData.PlayerStates.SWIMMING)
            return;

        HasSurfaced();
    }

    /// <summary>
    /// Has the player hit the top of the ocean.
    /// </summary>
    private void HasSurfaced()
    {
        // Is the player above the water?
        if (transform.position.y >= Water.waterOrigin.y + (water.transform.lossyScale.y / 2f))
        {
           Debug.Log("Player out or at top of water.");

            // check which lane the player is in. 
            var laneNumber = iceburgManager.GetLane(transform.position);
            Debug.Log("Lane number is " + laneNumber);

            // is there a iceburg in that lane?
            var targetNode = iceburgManager.GetIceburgJumpNode(laneNumber);
            if(targetNode == Vector2.zero)
            {
                Debug.Log("Run jump animation");
            }
            else
            {
               StartCoroutine(TeleportToPlatform(targetNode));
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
        while(true)
        {
            transform.position = Vector2.Lerp(transform.position, targetNode, 5f * Time.deltaTime);
            if(Vector2.Distance(transform.position, targetNode) <= 0.1f) break;

            yield return false;
        }
        yield return true;
    }
}
