using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenHungerHandler : MonoBehaviour
{
    GameObject player;
    PlayerData data;

    [SerializeField]
    float oxygenDecrement;
    [SerializeField]
    float oxygenIncrement;
    [SerializeField]
    float hungerDecrement;
    [SerializeField]
    float hungerIncrement;

    [SerializeField]
    GameObject oxygenSprite;
    [SerializeField]
    GameObject oxygenSpriteFill;

    [SerializeField]
    GameObject hungerSprite;
    [SerializeField]
    GameObject hungerSpriteFill;

    private GameObject bubble;
    private GameObject bubbleFill;

    private GameObject hunger;
    private GameObject hungerFill;

    public GameObject dead;

    // Use this for initialization
    void Start()
    {
        getPlayer();
        setDefaultValues();

        bubble = Instantiate(oxygenSprite, Vector3.zero, Quaternion.identity, this.transform);
        bubbleFill = Instantiate(oxygenSpriteFill, Vector3.zero, Quaternion.identity, this.transform);

        hunger = Instantiate(hungerSprite, Vector3.zero, Quaternion.identity, this.transform);
        hungerFill = Instantiate(hungerSpriteFill, Vector3.zero, Quaternion.identity, this.transform);

        bubble.transform.position = player.transform.position - new Vector3(18.0f, -6.0f, 0.0f);
        bubbleFill.transform.position = player.transform.position - new Vector3(18.0f, -6.0f, -1.0f);

        hunger.transform.position = player.transform.position - new Vector3(18.0f, -36.0f, -1.0f);
        hungerFill.transform.position = player.transform.position - new Vector3(18.0f, -36.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

        positionBubble();
        data.HungerLevel -= Time.deltaTime * hungerDecrement;

        if (data.HungerLevel < 0)
        {
            data.HungerLevel = 0;
            Destroy(gameObject);
            Instantiate(dead, this.transform.position, this.transform.rotation);
        }

        if (data.OxygenLevel > data.MaxOxygen / 2)
        {
            bubble.SetActive(false);
            bubbleFill.SetActive(false);
        }
            else
        {
			//drown sound plays when a player 
			StartCoroutine(data.PlayNext(data.drowningSound));
            bubble.SetActive(true);
            bubbleFill.SetActive(true);
        }

        if (data.HungerLevel > data.MaxHunger / 2)
        {
            hunger.SetActive(false);
            hungerFill.SetActive(false);
        }
        else
		{
			//still need a starving sound bite StartCoroutine(data.PlayNext(data.starvingSound));
            hunger.SetActive(true);
            hungerFill.SetActive(true);
        }

        if (data.InWater)
        {
            data.OxygenLevel -= Time.deltaTime * oxygenDecrement;
            if (data.OxygenLevel < 0)
            {
                data.OxygenLevel = 0;
                Destroy(gameObject);
                Instantiate(dead, this.transform.position, this.transform.rotation);
                // or do we want drown sound to happen when you die?

            }
        }
        else if (!data.InWater && data.OxygenLevel < data.MaxOxygen)
        {
            data.OxygenLevel += Time.deltaTime * oxygenIncrement;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            data.InWater = true;
            //Debug.Log("InWater");
        }

        if (other.tag == "Fish")
		{
			StartCoroutine(data.PlayNext(data.eatSound));
            Destroy(other.gameObject);
            GameData.FishSpawnedCount--;
            data.FishConsumed++;
            if (data.HungerLevel < data.MaxHunger)
            {
                data.HungerLevel += hungerIncrement;
            }
            if (data.HungerLevel > data.MaxHunger)
                data.HungerLevel = data.MaxHunger;
           
            //Debug.Log("Eaten Fish!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            data.InWater = false;
            //Debug.Log("OutWater");
        }
    }

    void getPlayer()
    {
        player = this.gameObject;
        data = player.GetComponent<PlayerData>();
    }

    void setDefaultValues()
    {
        if (oxygenIncrement < 0.1)
        {
            oxygenIncrement = 25f;
        }
        if (oxygenDecrement < 0.1)
        {
            oxygenDecrement = 1.0f;
        }

        if (hungerIncrement < 0.1)
        {
            hungerIncrement = 2.5f;
        }
        if (hungerDecrement < 0.1)
        {
            hungerDecrement = 2.0f;
        }
    }

    void positionBubble()
    {
        
        bubbleFill.transform.localScale = new Vector3(bubbleFill.transform.localScale.x, data.OxygenLevel * 2, bubbleFill.transform.localScale.z);
        hungerFill.transform.localScale = new Vector3(data.HungerLevel / 1.666666666666667f, hungerFill.transform.localScale.y, hungerFill.transform.localScale.z);
    }
}