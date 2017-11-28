using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCounter : MonoBehaviour {

    [SerializeField]
    int numberOfActiveControllers, numberOfLockedInPlayers;
    bool gameStarted;

    public PlayerAdding[] possiblePlayers;

    [SerializeField]
    GameObject penguin, iceBurg;

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStarted)
        { 
            numberOfActiveControllers = 0;
            numberOfLockedInPlayers = 0;

            foreach (PlayerAdding player in possiblePlayers)
            {
                if (player.ControllerActive)
                {
                    numberOfActiveControllers++;
                }

                if (player.lockedIn)
                {
                    numberOfLockedInPlayers++;
                }

            }

            if (numberOfActiveControllers == numberOfLockedInPlayers &&
                numberOfActiveControllers > 1
                )
            {
                //Debug.Log("Ready to start");
                startGame();
            }
            else
            {
                //Debug.Log("Waiting for players . . .");
            }
        }

    }

    void startGame()
    {
        gameStarted = true;

        SceneManager.LoadScene(1);

    }

    void OnLevelWasLoaded(int level)
    {
        Debug.Log("Doing the thing");
        if(level == 1)
        {
            Debug.Log("Doing the thing");
            foreach (PlayerAdding player in possiblePlayers)
            {
                if (player.ControllerActive)
                {
                    GameObject newPlayer;
                    newPlayer = (GameObject)Instantiate(penguin, GameObject.Find("PlayerSpawn" + player.thisIndex).transform);
                    newPlayer.GetComponent<PlayerData>().PlayerIndex = player.thisIndex;

                    //GameObject newIceburg;
                    //newIceburg = (GameObject)Instantiate(iceBurg,
                    //                                    new Vector3(GameObject.Find("PlayerSpawn" + player.thisIndex).transform.position.x, GameObject.Find("PlayerSpawn" + player.thisIndex).transform.position.y - 100, GameObject.Find("PlayerSpawn" + player.thisIndex).transform.position.z),
                    //                                    GameObject.Find("PlayerSpawn" + player.thisIndex).transform.rotation);

                  

                    //GameData.addIceberg(newIceburg);

                }
            }
        }
    }


}
