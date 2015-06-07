using UnityEngine;
using System.Collections;

public enum PlayerState
{
    Idle,
    Move,
    Attack,
}

public class Move : MonoBehaviour 
{

    RaycastHit hit;

    Vector3 clickPosition;
    Vector3 clickRotation;

    float moveSpeed = 3f;
    float tuneSpeed = 3.0f;

    Quaternion dir;
    Vector3 thisPosition;
    //Vector3 thisRotation;

    public PlayerState playerState;

	// Use this for initialization
	void Start () 
    {
        playerState = PlayerState.Idle;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButton(0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            clickPosition = hit.point;

            clickRotation = hit.point;


            clickPosition = new Vector3((Mathf.Round(clickPosition.x * 100.0f)) / 100.0f, 0, (Mathf.Round(clickPosition.z * 100)) / 100.0f);

            print("click : " + clickPosition.x);

            thisPosition = this.transform.position;
            thisPosition.y = 0;

            StopCoroutine("MoveCharac");
            StartCoroutine("MoveCharac");

        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(clickPosition, this.transform.position);
    }



    public IEnumerator MoveCharac()
    {

        while (clickPosition != thisPosition)
        {
            this.transform.Translate((clickPosition - thisPosition).normalized * Time.deltaTime * moveSpeed);
            thisPosition = transform.position;
            thisPosition = new Vector3((Mathf.Round(thisPosition.x * 100.0f)) / 100.0f, 0, (Mathf.Round(thisPosition.z * 100.0f)) / 100.0f );

            print("speed " + thisPosition);
            yield return null;
        }

        transform.position = clickPosition;

        print(thisPosition.x);

    }


}
