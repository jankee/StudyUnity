using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    private int health = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        health--;
        print("Health : " + health);
        other.GetComponent<Renderer>().material.color = Color.red;
    }


}
