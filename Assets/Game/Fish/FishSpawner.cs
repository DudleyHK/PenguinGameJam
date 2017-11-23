using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

    public GameObject fish;
    float randY, randX;
    Vector2 whereToSpawnFish;
    public float spawnFishRate = 2f;
    float nextSpawn = 0.0f;
    float minY = -80.0f, maxY = 80.0f,
        minX = -250.0f, maxX = 250.0f;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnFishRate;
            randY = Random.Range(maxY, minY);
            int random = Random.Range(0, 2);
            if(random == 0)
            {
                randX = minX;
            }
            if (random == 1)
            {
                randX = maxX;
            }
            Debug.Log("Random is: " + random + " so the fish spawns at: " + randX);
            whereToSpawnFish = new Vector2(-260f, randY);
            Instantiate(fish, whereToSpawnFish, Quaternion.identity);
        }
	}

}
