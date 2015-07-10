using UnityEngine;
using System.Collections;

public abstract class MoveObject : MonoBehaviour 
{
    public float moveTime = 0.1f;
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;

	// Use this for initialization
	protected virtual void Start () 
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
        print("Base.Start()");
        //테스트
        //RaycastHit2D hit;
        //Move(4, 4, out hit);
	}

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        //collider를 끄지 않으면 Linecast를 사용할 수 없다
        boxCollider.enabled = false;
        //blockingLayer을 찾아 hit에 넣어 준다
        hit = Physics2D.Linecast(start, end, blockingLayer);
        //collider를 다시 활성화 해준다.
        boxCollider.enabled = true;

        if (hit.collider == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;         
    }

	// Update is called once per frame
	protected IEnumerator SmoothMovement (Vector3 end) 
    {
        //sqrRemainingDistance에 지금위치
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
	}

    protected virtual void AttempMove(int xDir, int yDir)
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
        {
            return;
        }

        //print(hit.transform.GetComponent<Wall>());

        //Wall hitComponent = hit.transform.GetComponent<Wall>();

        //if (!canMove && hitComponent != null)
        //{
        //    OnCanMove(hitComponent);
        //}
    }

    //protected void OnCanMove(Wall component)
    //{
    //    Wall hitWall = component as Wall;

        
    //}

    //protected virtual void AttempMove<T>(int xDir, int yDir)
    //    where T : Component
    //{
    //    print("MoveObject AttempMove");

    //    RaycastHit2D hit;
    //    bool canMove = Move(xDir, yDir, out hit);
        
    //    print(canMove);
    //    if (hit.transform == null)
    //    {
    //        return;
    //    }

    //    T hitComponent = hit.transform.GetComponent<T>();

    //    if (!canMove && hitComponent != null)
    //    {
    //        OnCantMove(hitComponent);
    //    }
    //}

    //protected abstract void OnCantMove<T>(T component)
    //    where T : Component;
}
