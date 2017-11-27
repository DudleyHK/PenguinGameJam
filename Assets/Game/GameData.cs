using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    float killerWhaleTimer = 0;
    int bestPlayer = 0;
    int fishCap = 0;
    int currentFishSpawned = 0;
    List<GameObject> players;

	// Use this for initialization
	void Start () {
        killerWhaleTimer = Random.Range(15, 60);
        currentFishSpawned = 0;
        fishCap = 100;
        bestPlayer = 0;
        players = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
