using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour {

    float oxygen = 5, maxOxygen = 5;
    float health = 5, maxHealth = 5;

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

        OxygenRect = new Rect(player.transform.position.x + 30, player.transform.position.y + 25, 5.0f, 5.0f);
        OxygenTex = new Texture2D(1, 1);
        OxygenTex.SetPixel(0, 0, Color.white);
        OxygenTex.Apply();

        HungerRect = new Rect(player.transform.position.x + 30, player.transform.position.y + 10, 5.0f, 5.0f);
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
                health -= Time.deltaTime * 2;
                if (health < 0)
                {
                    health = 0;
                }
                Debug.Log("Drowning!!!");
            }
        }
        else if (!InWater && oxygen < maxOxygen)
        {
            oxygen += Time.deltaTime * 2.0f;
        }

        if (!InWater && health < maxHealth)
        {
            health += Time.deltaTime / 2.0f;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        InWater = true;
        Debug.Log("InWater");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        InWater = false;
        Debug.Log("OutWater");
    }

    void OnGUI()
    {
        float ratioO = oxygen / maxOxygen;
        float rectWidthO = ratioO * Screen.width / 5;
        OxygenRect.width = rectWidthO;

        float ratioH =  health / maxHealth;
        float rectWidthH = ratioH * Screen.width / 5;
        HungerRect.width = rectWidthH;

        GUI.DrawTexture(OxygenRect, OxygenTex);
        GUI.DrawTexture(HungerRect, HungerTex);
    }
}
