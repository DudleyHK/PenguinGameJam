using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public Swim swim;
    public CustomWalk customWalk;
    public CollisionState collisionState;
    public PlayerData playerData;
    public Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }


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
                playerData.PlayerState = PlayerData.PlayerStates.GROUNDED;
                //animator.SetInteger("AnimState", 0);
            }
            return;
        }

        // If the player is underwater and not standing
        if (swim.underWater)
        {
            playerData.PlayerState = PlayerData.PlayerStates.SWIMMING;
            //animator.SetInteger("AnimState", 2);
        }
        // If the player is above water and not standing
        else
        {
            playerData.PlayerState = PlayerData.PlayerStates.AIRBORN;
           // animator.SetInteger("AnimState", 1);
        }
    }
}

