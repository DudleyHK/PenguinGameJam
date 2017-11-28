using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public enum PlayerStates
    {
        NONE = 0,
        GROUNDED = 1,
        SWIMMING = 2,
        DEAD = 3,
        AIRBORN = 4
    }

    [SerializeField]
    PlayerStates playerState;
    [SerializeField]
    float hungerLevel;
    [SerializeField]
    float oxygenLevel;
    [SerializeField]
    float maxHunger;
    [SerializeField]
    int fishConsumed;
    [SerializeField]
    float maxOxygen;
    [SerializeField]
    float mass;
    [SerializeField]
    bool inWater;
    [SerializeField]
    int playerIndex;


	private AudioSource audioSource;
	[SerializeField]
	public AudioClip swimSound;
	[SerializeField]
	public AudioClip walkSound;
	[SerializeField]
	public AudioClip jumpSound;
	[SerializeField]
	public AudioClip drowningSound;
	[SerializeField]
	public AudioClip starvingSound;
	[SerializeField]
	public AudioClip eatSound;

    /*public PlayerData(PlayerState _PlayerState, float _OxygenLevel, float _HungerLevel, float _Mass)
    {
        playerState = _PlayerState;
        oxygenLevel = _OxygenLevel;
        hungerLevel = _HungerLevel;
        mass = _Mass;
    }*/

    void Start()
    {
        //playerState = PlayerStates.NONE;
        hungerLevel = 50.0f;
        oxygenLevel = 50.0f;
        maxHunger = 50.0f;
        maxOxygen = 50.0f;
        //mass = 0.0f;
    }

    public PlayerStates PlayerState
    {
        get
        {
            return playerState;
        }
        set
        {
            playerState = value;
        }
    }

    public float HungerLevel
    {
        get
        {
            return hungerLevel;
        }
        set
        {
            hungerLevel = value;
        }
    }
    public float MaxHunger
    {
        get
        {
            return maxHunger;
        }
    }

    public float MaxOxygen
    {
        get
        {
            return maxOxygen;
        }
    }


    public float OxygenLevel
    {
        get
        {
            return oxygenLevel;
        }
        set
        {
            oxygenLevel = value;
        }
    }

    public float Mass
    {
        get
        {
            return mass;
        }
        set
        {
            mass = value;
        }
    }
    public bool InWater
    {
        get
        {
            return inWater;
        }
        set
        {
            inWater = value;
        }
    }

    public int PlayerIndex
    {
        get
        {
            return playerIndex;
        }
        set
        {
            playerIndex = value;
        }
    }

    public int FishConsumed
    {
        get
        {
            return fishConsumed;
        }
        set
        {
            fishConsumed = value;
        }
    }


	public IEnumerator PlayNext(AudioClip clip)
	{
		float timer = 0f;
		while (audioSource.isPlaying)
		{
			timer += Time.deltaTime;
			yield return false;
			if (timer > 3f)
			{
				yield return true;
			}
		}
		audioSource.PlayOneShot (clip);
		yield return true;
	}
}
