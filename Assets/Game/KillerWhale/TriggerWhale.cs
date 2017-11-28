using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWhale : MonoBehaviour {

    public Color tmp;
    public SpriteRenderer whale;
    public List<Transform> attackPoints;

    private KillerSeal killa;
    bool attack;
    bool idle;

    float ratio = 0.0f;
    int rand;

    public float dist;
    float smoothTime = 1.0f;
    Vector3 vel = new Vector3(5.0f, 5.0f, 5.0f);

    void Start () {
        killa = FindObjectOfType<KillerSeal>();
        idle = false;
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
            if (!attack)
            {
                ratio = 0;

                rand = Random.Range(0, 2);
                Vector2 dir = attackPoints[rand].transform.position - whale.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                whale.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                //float step = speed * Time.deltaTime;
                Vector3 targetPos = attackPoints[rand].TransformPoint(new Vector3(0, 0, 0));
                whale.transform.position = Vector3.SmoothDamp(whale.transform.position, targetPos, ref vel, smoothTime);

                //whale.transform.position = attackPoints[rand].position;
                dist = Vector2.Distance(whale.transform.position, targetPos);

                if (!idle)
                {
                    killa.setIdle(true);
                    idle = true;
                }

                if (dist < 250.0f)
                {
                    Debug.Log("RAWR");
                    attack = true;
                    killa.setAttacking(true);
                }
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

    void OnTriggerStay2D(Collider2D other)
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

    public void resetWhale()
    {
        killa = FindObjectOfType<KillerSeal>();
        idle = false;
        attack = false;
        tmp = new Color(0.28f, 0.28f, 0.28f, 0.2f);
        whale.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}
