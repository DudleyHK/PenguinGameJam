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

    public PlayerStates playerState;
    public float hungerLevel;
    public float oxygenLevel;
    public float mass;

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
        //hungerLevel = 0.0f;
        //oxygenLevel = 0.0f;
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


}
