﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadForSeconds : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Die());
    }
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
