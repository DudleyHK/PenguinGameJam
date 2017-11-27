using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCounter : MonoBehaviour {

    [SerializeField]
    int numberOfActiveControllers, numberOfLockedInPlayers;

    public PlayerAdding[] possiblePlayers;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
        numberOfActiveControllers   = 0;
        numberOfLockedInPlayers     = 0;

        foreach (PlayerAdding player in possiblePlayers)
        {
            if(player.ControllerActive)
            {
                numberOfActiveControllers++;
            }

            if(player.lockedIn)
            {
                numberOfLockedInPlayers++;
            }
            
        }

        if(numberOfActiveControllers == numberOfLockedInPlayers &&
            numberOfActiveControllers > 1)
        {
            Debug.Log("Ready to start");
            startGame();
        }
        else
        {
            Debug.Log("Waiting for players . . .");
        }

    }

    void startGame()
    {
        SceneManager.LoadScene(1);
    }

}
