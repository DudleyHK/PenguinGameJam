using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerManager : MonoBehaviour
{
	private InputState inputState;
    private Walk walkBehaviour;
    private Animator animator;
	private CollisionState collisionState;
    [SerializeField]
    private PlayerData playerData;



    private void Start()
    {
        inputState     = GetComponent<InputState>();
        walkBehaviour  = GetComponent<Walk>();
        animator       = GetComponent<Animator>();
		collisionState = GetComponent<CollisionState>();
        playerData     = GetComponent<PlayerData>(); //PlayerState.NONE, 0.0f, 0.0f, 1.0f
    }

	private void Update()
	{
		if (collisionState.standing) 
		{
			ChangeAnimationState (0);
		}
		if (inputState.absVelX > 0) 
		{
			ChangeAnimationState (1);
		}
		if (inputState.absVelY > 0) 
		{
			ChangeAnimationState (2);
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

    PlayerData PlayerData
    {
        get
        {
            return playerData;
        }
    }
}
