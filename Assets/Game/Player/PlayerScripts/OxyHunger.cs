using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxyHunger : MonoBehaviour {

    float oxygen = 5, maxOxygen = 5;
    float hunger = 5, maxHunger = 5;

    public bool InWater;
    public GameObject player;

    Rect OxygenRect;
    Texture2D OxygenTex;

    Rect HungerRect;
    Texture2D HungerTex;

	// Use this for initialization
	void Start () {
        InWater = false;
        player = GameObject.FindGameObjectWithTag("Player");

		OxygenRect = new Rect(player.transform.position.x + 20, player.transform.position.y - 50, 5.0f, 5.0f);
        OxygenTex = new Texture2D(1, 1);
        OxygenTex.SetPixel(0, 0, Color.white);
        OxygenTex.Apply();

        HungerRect = new Rect(player.transform.position.x + 20, player.transform.position.y - 60, 5.0f, 5.0f);
        HungerTex = new Texture2D(1, 1);
        HungerTex.SetPixel(0, 0, Color.red);
        HungerTex.Apply();
    }
	
	// Update is called once per frame
	void Update () {
        if (InWater)
        {
            oxygen -= Time.deltaTime;
            if (oxygen < 0)
            {
                oxygen = 0;
                hunger -= Time.deltaTime * 2;
                if (hunger < 0)
                {
                    hunger = 0;
                }
                Debug.Log("Drowning!!!");
            }
        }
        else if (!InWater && oxygen < maxOxygen)
        {
            oxygen += Time.deltaTime * 2.0f;
        }

        if (!InWater && hunger < maxHunger)
        {
            hunger += Time.deltaTime / 2.0f;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            InWater = true;
            Debug.Log("InWater");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            InWater = false;
            Debug.Log("OutWater");
        }
    }

    void OnGUI()
    {
        float ratioO = oxygen / maxOxygen;
        float rectWidthO = ratioO * Screen.width / 5;
        OxygenRect.width = rectWidthO;

        float ratioH =  hunger / maxHunger;
        float rectWidthH = ratioH * Screen.width / 5;
        HungerRect.width = rectWidthH;

        GUI.DrawTexture(OxygenRect, OxygenTex);
        GUI.DrawTexture(HungerRect, HungerTex);
    }
}
