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
    public GameObject bubble;
    [SerializeField]
    public GameObject bubbleFill;
    

    // Use this for initialization
    void Start()
    {
        getPlayer();
        setDefaultValues();
    }

    // Update is called once per frame
    void Update()
    {

        positionBubble();
        data.HungerLevel -= Time.deltaTime * hungerDecrement;

        if (data.HungerLevel < 0)
        {
            data.HungerLevel = 0;
        }

        if (data.OxygenLevel > data.MaxOxygen / 2)
        {
            bubble.SetActive(false);
            bubbleFill.SetActive(false);
        }
            else
        {
            bubble.SetActive(true);
            bubbleFill.SetActive(true);
        }

        if (data.InWater)
        {
            data.OxygenLevel -= Time.deltaTime * oxygenDecrement;
            if (data.OxygenLevel < 0)
            {
                data.OxygenLevel = 0;
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
            Destroy(other.gameObject);
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
        player = GameObject.FindGameObjectWithTag("Player");
        data = player.GetComponent<PlayerData>();
    }
    void setDefaultValues()
    {
        if (oxygenIncrement < 0.1)
        {
            oxygenIncrement = 10;
        }
        if (oxygenDecrement < 0.1)
        {
            oxygenDecrement = 3;
        }

        if (hungerIncrement < 0.1)
        {
            hungerIncrement = 1.0f;
        }
        if (hungerDecrement < 0.1)
        {
            hungerDecrement = 0.5f;
        }
    }

    void positionBubble()
    {
        bubble.transform.position = player.transform.position - new Vector3(12.0f, -6.0f, 1.0f);
        bubbleFill.transform.position = player.transform.position - new Vector3(12.0f, -6.0f, 0.0f);
        bubbleFill.transform.localScale = new Vector3(bubbleFill.transform.localScale.x, data.OxygenLevel, bubbleFill.transform.localScale.z);
    }
}