using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndText : MonoBehaviour {

    public GameObject endInfo;

	// Use this for initialization
	void Start ()
    {
        endInfo = GameObject.Find("EndChacker");
	}

    void Awake()
    {
        endInfo = GameObject.Find("EndChacker");
    }

    // Update is called once per frame
    void Update ()
    {
        endInfo = GameObject.Find("EndChecker");
        GetComponent<TextMesh>().text = "Player " + endInfo.GetComponent<EndStuff>().winnerIndex.ToString();

        if (Input.GetKeyDown("joystick button 7"))
        {
            SceneManager.LoadScene(0);
        }

    }
}
