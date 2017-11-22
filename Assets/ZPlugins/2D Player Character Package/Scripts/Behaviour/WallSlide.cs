using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : StickToWall 
{
    public float slideVelocity = -5f;



	protected override void Update()
    {
        base.Update();


        if(onWallDetected)
        {
            var velY = slideVelocity;
            body2D.velocity = new Vector2(body2D.velocity.x, velY * Time.deltaTime);
        }
    }


    protected override void OnStick()
    {
        body2D.velocity = Vector2.zero;
    }


    protected override void OffWall()
    {
        // Do nothing.
    }
}
