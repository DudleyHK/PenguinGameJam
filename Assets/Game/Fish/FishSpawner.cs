﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

    public GameObject fish;
    public GameObject squid;
    public GameObject shrimp;
    private Fish fishObject;
    float randY;

    float minY = -80f, maxY = 80f;

	bool leftSpawn;
    Vector3 spawnLocation;
    [SerializeField]
    float spawnFishRate = 2f;
    float nextSpawn = 0.0f;

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    public void Update() {
        if (Time.time > nextSpawn)
        //if (Time.time > nextSpawn && GameData.FishSpawnedCount < GameData.FishCap)
        {
			leftSpawn = (Random.value > 0.5f);
            
            randY = Random.Range(Water.top - 100, Water.down + 100);
            int randomFishSpeed = Random.Range(12, 60);
			if (leftSpawn == true) {
				nextSpawn = Time.time + spawnFishRate;
				spawnLocation = new Vector3(Water.left + 75, randY, -2);
                GameObject instancedFish = (GameObject)Instantiate(generateFish(), spawnLocation, Quaternion.identity);
                fishObject = instancedFish.GetComponent<Fish>();
                fishObject.setDirectionLeft(true);
                fishObject.setFishSpeed(randomFishSpeed);
                // Debug.Log("spawing fish on the left.");
            }

            else if (leftSpawn == false) {
				nextSpawn = Time.time + spawnFishRate;
                spawnLocation = new Vector3(Water.right - 75, randY, -2);
                GameObject instancedFish = (GameObject)Instantiate(generateFish(), spawnLocation, Quaternion.Euler(180.0f, 0.0f, 180.0f));
                fishObject = instancedFish.GetComponent<Fish>();
                fishObject.setDirectionLeft(false);
                fishObject.setFishSpeed(randomFishSpeed);
               // Debug.Log("spawing fish on the right.");
            }
            GameData.FishSpawnedCount++;
        }
	}

    GameObject generateFish()
    {
        GameObject generatedFish = null;
        float random = 0;
        if (randY < -1200)
        { 
            random = Random.Range(0, 6);
            if(random > 4)
                generatedFish = squid;
            else
                generatedFish = fish;
        }
        else if (randY >= -1200 && randY < -700)
        {
            random = Random.Range(0, 10);
            if (random > 7)
                generatedFish = shrimp;
            else
                generatedFish = fish;
        }
        else
        {
            random = Random.Range(0, 10);
            if (random > 5)
                generatedFish = fish;
            else
                generatedFish = shrimp;
        }
        return generatedFish;
    }

}
