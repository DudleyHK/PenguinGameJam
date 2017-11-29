using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerManager : MonoBehaviour
{
	private InputState inputState;
    private Walk walkBehaviour;
    private Animator animator;
	private CollisionState collisionState;
    public PlayerData PlayerData { get { return GetComponent<PlayerData>(); } }



    private void Start()
    { 
        inputState     = GetComponent<InputState>();
        walkBehaviour  = GetComponent<Walk>();
        animator       = GetComponent<Animator>();
		collisionState = GetComponent<CollisionState>();

      
    }

	private void Update()
	{
        this.transform.localScale = new Vector3(30 , 30, 1);

        if (collisionState.standing) 
		{
            // Idle
            ChangeAnimationState(0);

        }
		if (inputState.absVelX > 0 && collisionState.standing) 
		{
            // Walking
            ChangeAnimationState(0);
		}
		if (inputState.absVelY > 0 && PlayerData.PlayerState != PlayerData.PlayerStates.SWIMMING) 
		{
			ChangeAnimationState (1);
		}

        if(PlayerData.PlayerState == PlayerData.PlayerStates.SWIMMING)
        {
            ChangeAnimationState(2);
        }

        if(PlayerData.PlayerState != PlayerData.PlayerStates.SWIMMING)
        {
            ChangeAnimationState(1);
        }

		animator.speed = walkBehaviour.running ? walkBehaviour.runMultiplier : 1;
	}

    private void ChangeAnimationState(int value)
    {
		animator.SetInteger ("AnimState", value);
    }

    private void initialisePlayerData()
    {
    }
}
