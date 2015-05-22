using UnityEngine;
using System.Collections;

public enum MOVETYPE
{
    IDLE = 0,
    WALK = 1,
    BOXPUSH = 2,
    CHEER = 3,
}

public class Character : MonoBehaviour 
{
//--------------------------------------------------------

    //움직임 시간
    public float MoveTime = 1.0f;

    //이동 속도
    public float MoveDistance = 2.0f;

    //로테이션 각도
    public float RotIncrement = 90.0f;

    //로테이션 시간
    public float RotTime = 1.0f;

    //트렌스폼 캐쉬
    private Transform ThisTransform = null;

    //플레이어의 상태
    public MOVETYPE State = MOVETYPE.IDLE;

    //애니메이터 레퍼런스
    private Animator AnimComp = null;

//----------------------------------------------------------

    public MOVETYPE PlayerState
    {
        get
        {
            return State;
        }
        set
        {
            State = value;
            AnimComp.SetInteger("iState", (int)State);
        }
    }

//-----------------------------------------------------------

	// Use this for initialization
	void Start () 
    {
        //트랜스폼 컴포넌트 가져오기
        ThisTransform = GetComponent<Transform>();

        //애니메이터 컴퍼넌트 가져오기
        AnimComp = GetComponent<Animator>();

        StartCoroutine(HandleInput());
	}

//-------------------------------------------------------------

	public IEnumerator HandleInput()
    {
        while (true)
        {
            while (Mathf.CeilToInt(Input.GetAxis("Vertical")) > 0)
            {
                //Set walk
                PlayerState = MOVETYPE.WALK;

                yield return StartCoroutine(Move(MoveDistance));
            }
            yield return null;
        }
        
    }

    public IEnumerator Move(float Increment = 0)
    {
        //시작위치
        Vector3 StartPos = ThisTransform.position;

        //Dest Postion
        Vector3 DestPos = ThisTransform.position + ThisTransform.forward * Increment;

        //Elapsed Time
        float ElapsedTime = 0.0f;

        while (ElapsedTime < MoveTime)
        {
            Vector3 FinalPos = Vector3.Lerp(StartPos, DestPos, ElapsedTime / MoveTime);

            ThisTransform.position = FinalPos;

            yield return null;

            ElapsedTime += Time.deltaTime;
        }

        ThisTransform.position = DestPos;

        yield break;
    }

    public IEnumerator Rotate(float Increment = 0)
    {
        float StartRot = ThisTransform.rotation.eulerAngles.y;

        float DestRot = StartRot + Increment;

        float ElapsedTime = 0.0f;

        while (ElapsedTime < RotTime)
        {
            float Angle = Mathf.LerpAngle(StartRot, DestRot, ElapsedTime / RotTime);

            ThisTransform.eulerAngles = new Vector3(0, Angle, 0);

            yield return null;

            ElapsedTime += Time.deltaTime;
        }

        ThisTransform.eulerAngles = new Vector3(0, Mathf.FloorToInt(DestRot), 0);
    }
}
