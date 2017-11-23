using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

    public GameObject fish;
    private Fish fishObject;
    float randY;

	bool leftSpawn;
    Vector2 whereToSpawnFish;
    [SerializeField]
    float spawnFishRate = 2f;
    float nextSpawn = 0.0f;

	// Use this for initialization
	void Start () {
	}

    // Update is called once per frame
    public void Update() {
        if (Time.time > nextSpawn)
        {
			leftSpawn = (Random.value > 0.5f);

            int randomFishSpeed = Random.Range(12, 60);
			if (leftSpawn == true) {
				nextSpawn = Time.time + spawnFishRate;
				//randX = Random.Range(-176, 176);
				randY = Random.Range(80f, -80f);
				whereToSpawnFish = new Vector2(-260f, randY);
                Debug.Log("spawing fish on the left.");
                GameObject instancedFish = (GameObject)Instantiate(fish, whereToSpawnFish, Quaternion.identity);
                fishObject = instancedFish.GetComponent<Fish>();
                fishObject.setDirectionLeft(true);
                fishObject.setFishSpeed(randomFishSpeed);

            }
            
			else if (leftSpawn == false) {
				nextSpawn = Time.time + spawnFishRate;
				randY = Random.Range(80f, -80f);
				whereToSpawnFish = new Vector2(260f, randY);
                GameObject instancedFish = (GameObject)Instantiate(fish, whereToSpawnFish, Quaternion.Euler(180.0f, 0.0f, 180.0f));
                fishObject = instancedFish.GetComponent<Fish>();
                fishObject.setDirectionLeft(false);
                fishObject.setFishSpeed(randomFishSpeed);
                Debug.Log("spawing fish on the right.");
            }
        }
	}

}
