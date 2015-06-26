using UnityEngine;
using System.Collections;

public class ParentTransform : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childObj = transform.GetChild(i);

            print(childObj.name);
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
