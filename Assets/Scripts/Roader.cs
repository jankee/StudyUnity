﻿using UnityEngine;
using System.Collections;

public class Roader : MonoBehaviour {

    public GameObject gameManager;

	// Use this for initialization
	void Awake () 
    {
	    if (GameManager.instance == null)
	    {
            Instantiate(gameManager);
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
