using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    NONE = 0,
    GROUNDED = 1,
    SWIMMING = 2,
    DEAD = 3,
}

public struct PlayerData
{
    public PlayerData(PlayerState _PlayerState, float _OxygenLevel, float _HungerLevel, float _Mass)
    {
        playerState = _PlayerState;
        oxygenLevel = _OxygenLevel;
        hungerLevel = _HungerLevel;
        mass = _Mass;
    }

    public PlayerState playerState { get; set; }
    public float hungerLevel { get; set; }
    public float oxygenLevel { get; set; }
    public float mass { get; set; }
}
