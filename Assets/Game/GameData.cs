using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {

    static float killerWhaleTimer = 0;
    static int bestPlayer = 0;
    static int fishCap = 0;
    static int currentFishSpawned = 0;
    static List<GameObject> players;
    static List<GameObject> icebergs;
    static bool isIcebergMelting = false;

    // Use this for initialization
    void Start () {
        killerWhaleTimer = Random.Range(15, 60);
        currentFishSpawned = 0;
        fishCap = 100;
        bestPlayer = 0;
        players = null;
        isIcebergMelting = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(!IsIcebergMelting)
        {
            chooseMeltedIceberg();
        }
	}

    public static int BestPlayer()
    {
        int bestPlayersIndex = 0;
        int bestHunger = 0;

        foreach (GameObject player in players)
        {
            PlayerData data = player.GetComponent<PlayerData>();
            if (data.FishConsumed > bestHunger)
            {
                bestHunger = data.FishConsumed;
                bestPlayersIndex = data.PlayerIndex;
            }
        }
        return bestPlayersIndex;
    }

    public static void chooseMeltedIceberg()
    {
        if(icebergs.Capacity > 0)
        {
            int icebergIndex = Random.Range(0, icebergs.Capacity);
            IceburgFloating data = icebergs[icebergIndex - 1].GetComponent<IceburgFloating>();
            isIcebergMelting = true;
            data.Melting = true;
        }
        
    }

    public static int FishSpawnedCount
    {
        get
        {
            return currentFishSpawned;
        }
        set
        {
            currentFishSpawned = value;
        }
    }

    public static int FishCap
    {
        get
        {
            return fishCap;
        }
        set
        {
            fishCap = value;
        }
    }

    public static float KillerWhaleTmer
    {
        get
        {
            return killerWhaleTimer;
        }
        set
        {
            killerWhaleTimer = value;
        }
    }

    public static bool IsIcebergMelting
    {
        get
        {
            return isIcebergMelting;
        }
        set
        {
            isIcebergMelting = value;
        }
    }
}
