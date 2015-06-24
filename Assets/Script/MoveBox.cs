using UnityEngine;
using System.Collections;

public class MoveBox : MonoBehaviour 
{
    public GameObject[] boxes;

    public Vector3 translateSpeed = Vector3.zero;

    public float maxY = 20.0f;
 
    IEnumerator MoveBoxes()
    {
        yield return new WaitForSeconds(2.0f);
        foreach (GameObject box in boxes)
        {
            while (box.transform.position.y < maxY)
            {
                box.transform.Translate(translateSpeed.x * Time.deltaTime,
                    translateSpeed.y * Time.deltaTime, translateSpeed.z * Time.deltaTime);

                yield return null;
            }
        }
        yield return new WaitForSeconds(1.0f);

        print("Helo World");
    }

	// Use this for initialization
	void Start () 
    {
        StartCoroutine("MoveBoxes");

	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
