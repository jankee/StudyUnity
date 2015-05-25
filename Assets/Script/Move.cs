using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour 
{

    RaycastHit hit;

    Vector3 clickPosition;
    Vector3 clickRotation;

    float moveSpeed = 3.0f;
    float tuneSpeed = 3.0f;

    Quaternion dir;
    Vector3 thisPosition;
    Vector3 thisRotation;
    

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButton(0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            clickPosition = hit.point;
            clickPosition.y = 0;

            clickRotation = hit.point;

            thisPosition = this.transform.position;
            thisPosition.y = 0;

            thisRotation = transform.position;
            
            if (clickPosition != thisPosition)
            {
                print("HI");
                StartCoroutine(MoveCharac());    
            }
            

        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(clickPosition, this.transform.position);
    }

    public IEnumerator MoveCharac()
    {
        print("clickPosition : " + clickRotation);
        print("thisRotation : " + thisRotation);

        dir = Quaternion.LookRotation((clickRotation - thisRotation).normalized);
        dir.x = 0;
        dir.z = 0;

        this.transform.Translate((clickPosition - thisPosition).normalized * Time.deltaTime * moveSpeed);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, dir, tuneSpeed * Time.deltaTime);
        

        yield return null;
    }


}
