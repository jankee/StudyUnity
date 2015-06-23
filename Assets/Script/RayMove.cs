using UnityEngine;
using System.Collections;

public class RayMove : MonoBehaviour 
{
    public float moveSpeed = 5f;

	// Use this for initialization
	void Awake () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
        }
	}
}
