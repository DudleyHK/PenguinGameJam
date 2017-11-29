using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndStuff : MonoBehaviour {

    public int winnerIndex = 0;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
        {
            winnerIndex = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>().PlayerIndex;

            SceneManager.LoadScene(2);

            GameObject.Find("WinTitle").GetComponent<TextMesh>().text = "Player " + winnerIndex.ToString();
        }



    }
}
