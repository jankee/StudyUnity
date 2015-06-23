using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    List<GameObject> enemy;

    GameObject closest;

    List<EnemyCollector> enemyCollector;
    GameObject[] enemyCollect;

    int[] array;
    int position = -1;

	// Use this for initialization
	void Start () 
    {
        playerState = PlayerState.Idle;
        enemy = new List<GameObject>();
        enemyCollector = new List<EnemyCollector>();
        array = new int[3];
        closest = null;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButton(0))
        {
            //playerState = PlayerState.Move;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            clickPosition = hit.point;

            clickRotation = hit.point;


            clickPosition = new Vector3((Mathf.Round(clickPosition.x * 100.0f)) / 100.0f, 0, (Mathf.Round(clickPosition.z * 100)) / 100.0f);

            //print("click : " + clickPosition.x);

            thisPosition = this.transform.position;
            thisPosition.y = 0;

            if (thisPosition != clickPosition && closest == null)
            {
                StopCoroutine("MoveCharac");
                StartCoroutine("MoveCharac");
            }
            else if (closest != null)
            {
                print("FindMove");
                StopCoroutine("FindMove");
                StartCoroutine("FindMove");
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            FindEnemy();
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(clickPosition, this.transform.position);
    }

    public IEnumerator FindMove()
    {
        //GameObject findEnemy =  FindEnemy();
        

        Vector3 runDirection = closest.transform.position - this.transform.position;

        runDirection.x = Mathf.Round(runDirection.x * 100.0f) / 100.0f;
        runDirection.z = Mathf.Round(runDirection.z * 100.0f) / 100.0f;
        runDirection.y = 0.0f;

        print(runDirection);

        while (runDirection != thisPosition)
        {
            transform.Translate((runDirection - thisPosition) * Time.deltaTime * moveSpeed);
            thisPosition = transform.position;
            thisPosition = new Vector3((Mathf.Round(thisPosition.x * 100.0f)) / 100.0f, 0, (Mathf.Round(thisPosition.z * 100.0f)) / 100.0f);

            yield return null;
        }

    }

    public IEnumerator MoveCharac()
    {
        playerState = PlayerState.Move;

        while (clickPosition != thisPosition)
        {
            this.transform.Translate((clickPosition - thisPosition).normalized * Time.deltaTime * moveSpeed);
            thisPosition = transform.position;
            thisPosition = new Vector3((Mathf.Round(thisPosition.x * 100.0f)) / 100.0f, 0, (Mathf.Round(thisPosition.z * 100.0f)) / 100.0f );

            yield return null;
        }

        transform.position = clickPosition;
        playerState = PlayerState.Idle;
        print("MoveCharac");

    }

    public IEnumerator MoveCharacter()
    {

        yield return null;
    }

    public GameObject FindEnemy()
    {

        enemyCollect = GameObject.FindGameObjectsWithTag("Enemy");
        
        float distance = Mathf.Infinity;
        Vector3 position = this.transform.position;

        foreach (GameObject go in enemyCollect)
        {
            Vector3 diff = go.transform.position - position;
            //sqrMagnitude는 
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        print(closest.name + closest.transform.position + " / " + distance);

        return closest;
        
    }

    public int this[int index]
    {
        get
        {
            return array[index];
        }
        set
        {
            if (index >= array.Length)
            {
                
            }
        }
    }

    public void AttackEnemy()
    {
        playerState = PlayerState.Attack;
    }
}

public class EnemyCollector : MonoBehaviour
{
    private GameObject enemy;

    public GameObject Enemy
    {
        get { return enemy; }
    }

    private Vector3 enemyPosition;

    public Vector3 EnemyPosition
    {
        get { return enemyPosition; }
    }

    public EnemyCollector(GameObject enemy, Vector3 enemyPosition)
    {
        this.enemy = enemy;
        this.enemyPosition = enemyPosition;
    }
}
