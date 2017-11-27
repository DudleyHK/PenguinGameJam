using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilentKiller : MonoBehaviour
{

    public int rand;
    public float dist;
    public List<Transform> points;
    public SpriteRenderer fSprite;

    bool newTarget;
    float smoothTime = 1.0f;

    Vector3 vel = new Vector3(10.0f, 10.0f, 10.0f);

    // Use this for initialization
    void Start()
    {
        newTarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (newTarget)
        {
            rand = Random.Range(0, 8);
            newTarget = false;
            //StartCoroutine(Delay());
        }

        //if this position x > target position = going right & undo Y sprite flip
        if (this.transform.position.x > points[rand].transform.position.x)
        {
            fSprite.flipY = true;
        }
        //if this position x < target position = going left & apply Y sprite flip
        else if (this.transform.position.x < points[rand].transform.position.x)
        {
            fSprite.flipY = false;
        }

        //Get direction of player and look at it
        Vector2 dir = points[rand].transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //float step = speed * Time.deltaTime;
        Vector3 targetPos = points[rand].TransformPoint(new Vector3(0, 0, 0));
        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPos, ref vel, smoothTime);

        dist = Vector2.Distance(this.transform.position, targetPos);
        if (dist < 30.0f)
        {
            //StopCoroutine(Delay());
            newTarget = true;
        }
    }
}
