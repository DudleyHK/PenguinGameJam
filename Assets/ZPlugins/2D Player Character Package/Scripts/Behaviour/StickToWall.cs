using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToWall : AbstractBehaviour 
{
    public bool onWallDetected;

    protected float defaultGravityScale;
    protected float defaultDrag;



    private void Start()
    {
        defaultGravityScale = body2D.gravityScale;
        defaultDrag = body2D.drag;

    }

    protected virtual void Update()
    {
        if(collisionState.onWall)
        {
            if(!onWallDetected)
            {
                OnStick();
                onWallDetected = true;
            }

        }
        else
        {
            if(onWallDetected)
            {
                OffWall();
                onWallDetected = false;
            }
        }
    }


    protected virtual void OnStick()
    {
        if(collisionState.standing && body2D.velocity.y > 0f)
        {
            body2D.gravityScale = 0;
            body2D.drag = 100;
        }
    }

    protected virtual void OffWall()
    {
        if(body2D.gravityScale != defaultGravityScale)
        {
            body2D.gravityScale = defaultGravityScale;
            body2D.drag = defaultDrag;
        }
    }
}
