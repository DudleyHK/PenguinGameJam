using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWhale : MonoBehaviour {

    public Color tmp;
    public SpriteRenderer whale;
    public List<Transform> attackPoints;

    KillerSeal killa;
    bool attack;
    float ratio = 0.0f;
    int rand;

	void Start () {
        attack = false;
        tmp = whale.GetComponent<SpriteRenderer>().color;
        whale.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
	
	void Update () {

        //GameData.KillerWhaleTmer--;

        tmp.a += ratio * Time.deltaTime;
        whale.color = tmp;

        if (tmp.a <= 0.1)
        {
            ratio = 0;
        }
        else if (tmp.a >= 0.9)
        {
            ratio = 0;
            // GO TO ATTACK POINT
            if (!attack)
            {
                Debug.Log("Attack");

                attack = true;
                rand = Random.Range(0, 4);
                whale.transform.position = attackPoints[rand].position;

                killa.setAttacking(true);
            }
        }
        else
        {
            if (ratio > 0)
            {
                whale.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            }
            else if (ratio < 0)
            {
                whale.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ratio = 0.05f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ratio = -0.1f;
        }
    }
}
