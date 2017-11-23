using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ButtonState
{
    public bool value;
    public float holdTime = 0f;
}


public enum Directions
{
    Right = 1,
    Left = -1
}




public class InputState : MonoBehaviour 
{
    public Directions direction = Directions.Right;
    public float absVelX = 0f;
    public float absVelY = 0f;

    protected ButtonState state;
    protected Buttons lastButtonReleased;

    private Rigidbody2D body2D;
    private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>();


    private void Awake()
    {
        body2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
       absVelX = Mathf.Abs(body2D.velocity.x);
       absVelY = Mathf.Abs(body2D.velocity.y);
    }


    public virtual void SetButtonValue(Buttons key, bool value)
    {
        if(!buttonStates.ContainsKey(key))
        {
            buttonStates.Add(key, new ButtonState());
        }

        state = buttonStates[key];

        if(state.value && !value)
        {
            //print("Button " + key + " released.. total time: " + state.holdTime);
            state.holdTime = 0f;
            lastButtonReleased = key;
        }
        else if(state.value && value)
        {
            state.holdTime += Time.deltaTime;
            //print("Button " + key +  " down for " + state.holdTime);
        }
        print("Last button presed " + lastButtonReleased);
        state.value = value;
    }


    public bool GetButtonValue(Buttons key)
    {
        if(buttonStates.ContainsKey(key))
        {
            return buttonStates[key].value;
        }
        else
        {
            return false;
        }
    }

	public float GetButtonHoldTime(Buttons key)
	{
		if(buttonStates.ContainsKey(key))
		{
			return buttonStates[key].holdTime;
		}
		else
		{
			return 0;
		}
	}
}
