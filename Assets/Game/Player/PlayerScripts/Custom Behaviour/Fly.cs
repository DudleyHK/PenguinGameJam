using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fly : AbstractBehaviour 
{
    public float speed = 95f;
    public bool flying = false;
    public float terminalSpeed = 200f;
    public PlayerData playerData;


    protected virtual void Update()
    {
        SetFlying();
        FlyMovement();
    }

    private void SetFlying()
    {
        if(!collisionState.standing)
        {
            flying = true;
        }
        else
        {
            flying = false;
        }
    }

    private void FlyMovement()
    {
        var fly = inputState.GetButtonValue(inputButtons[0]);
        var holdTime = inputState.GetButtonHoldTime(inputButtons[0]);

        if (playerData.PlayerState == PlayerData.PlayerStates.AIRBORN)
        {
            return;
        }


        if(fly && holdTime < 0.1f)
        {
            var boost = body2D.velocity.y + speed;
            body2D.velocity = new Vector2(body2D.velocity.x, boost);
        }

        if(body2D.velocity.y >= terminalSpeed)
        {
            body2D.velocity = new Vector2(body2D.velocity.x, terminalSpeed);
        }
    }
}
