using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerSeal : MonoBehaviour {

    public int rand;
    public float speed;
    public float dist;
    public List<Transform> points;
    public SpriteRenderer fSprite;
    public GameObject silentKilla;

    bool attacking;
    bool setPoint;
    bool idle;

    float smoothTime = 1.0f;
    float fraction = 0;

    Vector3 vel = new Vector3(10.0f,10.0f, 0);
    Vector3 targetPos;
    GameObject player;
    TriggerWhale whaleTrigger;

    // Use this for initialization
    void Start () {
        speed = 0.5f;
        idle = false;
        attacking = false;
        setPoint = false;
        whaleTrigger = FindObjectOfType<TriggerWhale>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if (!attacking && !idle)
        {
            MoveToTarget();
        }
        else if (attacking && !idle)
        {
            AttackPlayer();
        }
        else
        { }
    }

    void AttackPlayer()
    {

        

        if (!setPoint)
        {
            fSprite.color = new Color(255, 255, 255, 1);

            Vector2 dir = player.transform.position - this.transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            smoothTime = 1.0f * Time.deltaTime;
            //float step = speed * Time.deltaTime;
            if (this.transform.position.x > player.transform.position.x)
            {
                targetPos = player.transform.TransformPoint(new Vector3(-400, 40, 0));
                fSprite.flipY = true;
            }
            //if this position x < target position = going left & apply Y sprite flip
            else if (this.transform.position.x < player.transform.position.x)
            {
                targetPos = player.transform.TransformPoint(new Vector3(+400, 40, 0));
                fSprite.flipY = false;
            }
            setPoint = true;
        }

        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, smoothTime);

        dist = Vector2.Distance(this.transform.position, targetPos);
        if (dist < 30.0f)
        {
            attacking = false;
            whaleTrigger.resetWhale();
        }
    }

    void MoveToTarget()
    {
        smoothTime = 1.0f;
        //if this position x > target position = going right & undo Y sprite flip
        if (this.transform.position.x > silentKilla.transform.position.x)
        {
            fSprite.flipY = true;
        }
        //if this position x < target position = going left & apply Y sprite flip
        else if (this.transform.position.x < silentKilla.transform.position.x)
        {
            fSprite.flipY = false;
        }

        //Get direction of player and look at it
        Vector2 dir = silentKilla.transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //float step = speed * Time.deltaTime;
        Vector3 targetPos = silentKilla.transform.TransformPoint(new Vector3(0, 0, 0));
        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPos, ref vel, smoothTime);

        dist = Vector2.Distance(this.transform.position, targetPos);
    }

    public void setIdle(bool _bool)
    {
        idle = _bool;
    }

    public void setAttacking(bool _bool)
    {
        idle = false;
        setPoint = false;
        attacking = _bool;
    }

}
