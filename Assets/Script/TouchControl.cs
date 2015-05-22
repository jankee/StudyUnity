using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour {

    //마우스 클릭으로 입력된 좌표를 공간 좌표로 변환하는데 사용
    public Camera MainCamera;

    //이동 지점
    public Transform MovePoint;

    GameObject findHero;

    //이동 방향
    Vector3 moveDirection;

    LayerMask layer;

    //마지막 입력 입력 위치
    Vector3 lastInputPosition;

    Vector3 tempVector3;

    Vector2 tempVector2;


	// Use this for initialization
	void Awake () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            findHero = GameObject.Find("hero");
            tempVector3 = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            tempVector3.z = 0;
        }
	}

    //void OnDrawGizmos()
    //{

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(findHero.transform.position, tempVector3);
    //}

    //public IEnumerator CheckMovePoint()
    //{
    //    print(tempVector3);
    //    yield return;
    //}
}
