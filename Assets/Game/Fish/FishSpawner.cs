using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour {

    public GameObject fish;
    float randY;
	public bool leftSpawn;
    Vector2 whereToSpawnFish;
    public float spawnFishRate = 2f;
    float nextSpawn = 0.0f;

	public Fish fishClass;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    public void Update() {
        if (Time.time > nextSpawn)
        {
			leftSpawn = (Random.value > 0.5f);

			if (leftSpawn == true) {
				nextSpawn = Time.time + spawnFishRate;
				//randX = Random.Range(-176, 176);
				randY = Random.Range(80f, -80f);
				whereToSpawnFish = new Vector2(-260f, randY);
				Instantiate(fish, whereToSpawnFish, Quaternion.identity);
				fishClass.setFishWay (false);
				//fishWay.setFishWay (true);
			}
            
			else if (leftSpawn == false) {
				nextSpawn = Time.time + spawnFishRate;
				//randX = Random.Range(-176, 176);
				randY = Random.Range(80f, -80f);
				whereToSpawnFish = new Vector2(260f, randY);
				Instantiate(fish, whereToSpawnFish, Quaternion.Euler(0.0f,0.0f,180.0f));
			}
        }
	}

}
