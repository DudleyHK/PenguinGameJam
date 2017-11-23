using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxyHunger : MonoBehaviour {

    public float oxygen = 10, maxOxygen = 10;
    float hunger = 10, maxHunger = 10;

    public bool InWater;

    GameObject player;
    public GameObject oxygenSprite;

    public Sprite full;
    public Sprite full9_10;
    public Sprite full8_10;
    public Sprite full7_10;
    public Sprite full6_10;
    public Sprite full5_10;
    public Sprite full4_10;
    public Sprite full3_10;
    public Sprite full2_10;
    public Sprite full1_10;
    public Sprite full0_10;
    //Rect OxygenRect;
    //Texture2D OxygenTex;

    //Rect HungerRect;
    //Texture2D HungerTex;

    // Use this for initialization
    void Start () {
        InWater = false;
        oxygenSprite.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        //oxygenSprite = GameObject.Find("OxygenPrefab");

        //full = Resources.Load("Oxygen") as Sprite;
        //halffull = Resources.Load("Oxygen2_4") as Sprite;
        //half = Resources.Load("Oxygen2_4") as Sprite;
        //halfempty = Resources.Load("Oxygen1_4") as Sprite;
    }
	
	// Update is called once per frame
	void Update () {

        oxygenSprite.transform.position = player.transform.position - new Vector3(12.0f, -6.0f, 0.0f);
        checkOxygenLevel();

        if (hunger != 0)
        {
            if (hunger < 0)
            {
                hunger = 0;
            }

            hunger -= Time.deltaTime / 4;

            if (InWater)
            {
                oxygen -= Time.deltaTime / 2;
                oxygenSprite.SetActive(true);
                if (oxygen < 0)
                {
                    oxygen = 0;
                    //oxygenSprite.SetActive(false);
                    //Debug.Log("Drowning!!!");
                }
            }
            else if (!InWater && oxygen < maxOxygen)
            {
                oxygen += Time.deltaTime * 4.0f;
                if (oxygen >= maxOxygen)
                {
                    oxygenSprite.SetActive(false);
                }
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            InWater = true;
            //Debug.Log("InWater");
        }

        if (other.tag == "Fish")
        {
            Destroy(other.gameObject);
            hunger += 0.2f;
            Debug.Log("Eaten Fish!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            InWater = false;
            //Debug.Log("OutWater");
        }
    }

    void OnGUI()
    {
        //float ratioO = oxygen / maxOxygen;
        //float rectWidthO = ratioO * Screen.width / 5;
        //OxygenRect.width = rectWidthO;

        //float ratioH =  hunger / maxHunger;
        //float rectWidthH = ratioH * Screen.width / 5;
        //HungerRect.width = rectWidthH;

        //GUI.DrawTexture(OxygenRect, OxygenTex);
        //GUI.DrawTexture(HungerRect, HungerTex);
    }

    void checkOxygenLevel()
    {
        if (oxygen <= 0.0f)
        {
            oxygenSprite.GetComponent<SpriteRenderer>().sprite = full0_10;
        }
        else if (oxygen <= 1.0f)
        {
            oxygenSprite.GetComponent<SpriteRenderer>().sprite = full1_10;
        }
        else if (oxygen <= 2.0f)
        {
            oxygenSprite.GetComponent<SpriteRenderer>().sprite = full2_10;
        }
        else if (oxygen <= 3.0f)
        {
            oxygenSprite.GetComponent<SpriteRenderer>().sprite = full3_10;
        }
        else if (oxygen <= 4.0f)
        {
            oxygenSprite.GetComponent<SpriteRenderer>().sprite = full4_10;
        }
        else if (oxygen <= 5.0f)
        {
            oxygenSprite.GetComponent<SpriteRenderer>().sprite = full5_10;
        }
        else if (oxygen <= 6.0f)
        {
            oxygenSprite.GetComponent<SpriteRenderer>().sprite = full6_10;
        }
        else if (oxygen <= 7.0f)
        {
            oxygenSprite.GetComponent<SpriteRenderer>().sprite = full7_10;
        }
        else if (oxygen <= 8.0f)
        {
            oxygenSprite.GetComponent<SpriteRenderer>().sprite = full8_10;
        }
        else if (oxygen <= 9.0f)
        {
            oxygenSprite.GetComponent<SpriteRenderer>().sprite = full9_10;
        }
        else
        {
            oxygenSprite.GetComponent<SpriteRenderer>().sprite = full;
        }
    }
}

//OxygenRect = new Rect(player.transform.position.x, player.transform.position.y, 5.0f, 5.0f);
//      OxygenTex = new Texture2D(1, 1);
//      OxygenTex.SetPixel(0, 0, Color.white);
//      OxygenTex.Apply();

//      HungerRect = new Rect(player.transform.position.x, player.transform.position.y, 5.0f, 5.0f);
//      HungerTex = new Texture2D(1, 1);
//      HungerTex.SetPixel(0, 0, Color.red);
//      HungerTex.Apply();