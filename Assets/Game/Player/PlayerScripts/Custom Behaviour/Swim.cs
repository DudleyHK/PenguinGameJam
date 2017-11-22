using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : Fly
{
    public GameObject water;
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

        if (playerData.playerState != PlayerData.PlayerStates.GROUNDED)
        {
            HasSurfaced();
        }
    }

    /// <summary>
    /// Has the player hit the top of the ocean.
    /// </summary>
    private void HasSurfaced()
    {
        var waterOffsetX = water.transform.position.x + (water.transform.lossyScale.x / 2f);
        var waterOffsetY = water.transform.position.y - (water.transform.lossyScale.y / 2f);
        var waterOrigin = new Vector2(waterOffsetX, waterOffsetY);

        // Is the player above the water?
        if (transform.position.y >= waterOrigin.y + (water.transform.lossyScale.y / 2f))
        {
            Debug.Log("Player out or at top of water.");
            if (!applyBoost)
            {
                body2D.AddForceAtPosition(Vector2.up * hopSpeed, transform.position, ForceMode2D.Impulse);
                applyBoost = true;
            }
            underWater = false;
        }
        else
        {
            applyBoost = false;
            underWater = true;
        }
    }
}
