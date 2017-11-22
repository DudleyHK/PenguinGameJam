using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public Swim swim;
    public CustomWalk customWalk;
    public CollisionState collisionState;
    public PlayerData playerData;




   private void Update()
   {
        GameState();
   }



    private void GameState()
    {
        // Is the player standing
        if (collisionState.standing)
        {
            // Are they under water?
            if (swim.underWater)
            {
                // Dont let them start walking
                collisionState.standing = false;
            }
            // Then they are above water
            else
            {
                playerData.playerState = PlayerData.PlayerStates.GROUNDED;
            }
        }

        // If the player is underwater and not standing
        if (swim.underWater)
        {
            playerData.playerState = PlayerData.PlayerStates.SWIMMING;
        }
        // If the player is above water and not standing
        else
        {
            playerData.playerState = PlayerData.PlayerStates.AIRBORN;
        }
    }
}

