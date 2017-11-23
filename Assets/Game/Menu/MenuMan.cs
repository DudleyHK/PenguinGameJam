using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuMan : MonoBehaviour
{


	private GameState gameState;
	private GameState nextState;

	// Use this for initialization
	void Start () {
		gameState = GameState.START;
		nextState = GameState.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		checkState ();
		loadChilds ();
	}

	public GameState currentState
	{
		get
		{
			return gameState;
		}
		set
		{
			gameState = value;
		}
	}

	void checkState()
	{
		//more stuff here
		//might be just useless though honestly
		if (nextState != GameState.NONE)
		{
			gameState = nextState;
		}
	}

	void loadChilds()
	{
		bool show;
		foreach (SpriteRenderer i in this.GetComponentsInChildren<SpriteRenderer>())
		{
			show = false;
			switch (gameState) {
			case GameState.START: 
				if (i.gameObject.tag == "Start") {
					show = true;
				}
				break;
			case GameState.PAUSE:
				if (i.gameObject.tag == "Pause" || i.gameObject.tag == "UI") {
					show = true;
				}
				break;
			case GameState.GAMEOVER:
				if (i.gameObject.tag == "GameOver") {
					show = true;
				}
				break;
			case GameState.PLAY:
				if (i.gameObject.tag == "UI") {
					show = true;
				}
				break;
					}

			/*switch (i.tag) 
			{
			case "Options":
				break;
			case "Lobby":
				break;
			case "Game Over":
				break;
			case "Start":
				break;
			case "Pause":
			case "UI": 


				break;
			}*/
			i.enabled = show;
		}

	}
}
