using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public float speed;

    public Inventory inventory;

    private int health = 100;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        HandleMovement();
	}

    void HandleMovement()
    {
        float translation = speed * Time.deltaTime;

        transform.Translate(new Vector3
            (Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical") * translation));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            health--;
            print("Health : " + health);
            other.GetComponent<Renderer>().material.color = Color.red;    
        }

        if (other.tag == "Item")
        {
            print("Item : " + other.name);
            inventory.AddItem(other.GetComponent<Item>());
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            health--;
            print("Health : " + health);
            other.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
