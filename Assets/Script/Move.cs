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

    float moveSpeed = 3.0f;
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
            clickPosition.y = 0;

            clickRotation = hit.point;

            thisPosition = this.transform.position;
            thisPosition.y = 0;

            StartCoroutine(MoveCharac());
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
        print("thisRotation : " + thisPosition);

        dir = Quaternion.LookRotation((clickRotation - thisPosition).normalized);
        dir.x = 0;
        dir.z = 0;

        this.transform.Translate((clickPosition - thisPosition).normalized * Time.deltaTime * moveSpeed);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, dir, tuneSpeed * Time.deltaTime);
        

        yield return null;
    }


}
