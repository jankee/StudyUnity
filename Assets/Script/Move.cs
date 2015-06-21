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

            print("click : " + clickPosition.x);

            thisPosition = this.transform.position;
            thisPosition.y = 0;

            if (thisPosition != clickPosition)
            {
                StopCoroutine("MoveCharac");
                StartCoroutine("MoveCharac");
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



    public IEnumerator MoveCharac()
    {
        playerState = PlayerState.Move;

        while (clickPosition != thisPosition)
        {
            this.transform.Translate((clickPosition - thisPosition).normalized * Time.deltaTime * moveSpeed);
            thisPosition = transform.position;
            thisPosition = new Vector3((Mathf.Round(thisPosition.x * 100.0f)) / 100.0f, 0, (Mathf.Round(thisPosition.z * 100.0f)) / 100.0f );

            //print("speed " + thisPosition);
            yield return null;
        }

        transform.position = clickPosition;
        playerState = PlayerState.Idle;
        print(thisPosition.x);

    }

    public void FindEnemy()
    {

        //enemyCollector = GameObject.FindGameObjectsWithTag("Enemy");

        //enemyPosition = new Vector3[enemyCollector.Length];

        ////for (int i = 0; i < enemyCollector.Length; i++)
        ////    {
        //        foreach (GameObject ec in enemyCollector)
        //        {
        //            for (int i = 0; i < enemyCollector.Length; i++)
        //            {
        //                enemyPosition[i] = ec.transform.position;
        //                print(ec.name);
        //                print(enemyPosition[i]);    
        //            }
                    
        //        }

        //        print("{0} {1}" + enemyCollector[0].name + enemyPosition[0]);
        //    //}

        enemyCollect = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject test = new GameObject();
        //Vector3[] vtest = new Vector3();

        //foreach (GameObject item in enemyCollect)
        //{
        //    test = item;
        //    //vtest[] = item.transform.position;

        //    print(item.name + vtest);

        //}

        //for (int i = 0; i < enemyCollect.Length; i++)
        //{
        //    vtest[i] = enemyCollect[i].transform.position - this.transform.position;
        //}
        
        

        


        //foreach (Vector3 item in aPosition)
        //{
        //    Vector3 a;
        //    Vector3 b;

        //    a = item - this.transform.position;

        //}

        //print("HI");

        
        


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
}
