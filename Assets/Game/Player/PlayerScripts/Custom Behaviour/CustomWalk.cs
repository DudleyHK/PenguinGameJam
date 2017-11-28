using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CustomWalk : Walk
{

	public PlayerData playerData;

    protected override void Update()
    {
        running = false;

        var right = inputState.GetButtonValue(inputButtons[0]);
        var left = inputState.GetButtonValue(inputButtons[1]);

        if (right || left)
        {
            var tempSpeed = speed;

            var velX = tempSpeed * (float)inputState.direction;
            body2D.velocity = new Vector2(velX, body2D.velocity.y);
			StartCoroutine(playerData.PlayNext(playerData.walkSound));
        }

    }
}