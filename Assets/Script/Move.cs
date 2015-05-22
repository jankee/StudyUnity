using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour 
{

    RaycastHit hit;

    Vector3 click;

    float velocity = 3.0f;

    Vector3 thisPosition;

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
            click = hit.point;
            click.y = 0;

            thisPosition = this.transform.position;
            thisPosition.y = 0;

            if (click != thisPosition)
            {
                print("HI");
                StartCoroutine(MoveCharac());    
            }
            

        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(click, this.transform.position);
    }

    public IEnumerator MoveCharac()
    {
        print("click : " + click);
        print("thisPosition : " + thisPosition);
       
        this.transform.Translate((click - thisPosition).normalized * Time.deltaTime * velocity);    

        yield return null;
    }
}
