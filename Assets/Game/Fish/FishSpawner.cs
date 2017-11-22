using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

    public GameObject fish;
    int randX;
    int randY;
    Vector2 whereToSpawnFish;
    public float spawnFishRate = 2f;
    float nextSpawn = 0.0f;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnFishRate;
            randX = Random.Range(-176, 176);
            randY = Random.Range(-60, -30);
            whereToSpawnFish = new Vector2(randX, randY);
            Instantiate(fish, whereToSpawnFish, Quaternion.identity);
        }
	}

}
