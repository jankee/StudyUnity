using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerT : MonoBehaviour
{
    public List<EnemyT> enemyListT;

    public GameObject[] enemyListTS;

    // Use this for initialization
    void Start()
    {
        enemyListT = new List<EnemyT>();
        enemyListTS = new GameObject[5];
    }

    public void AddEnemyToListT(EnemyT script)
    {
        enemyListT.Add(script);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //enemyListTS = ;
            enemyListT.Add(GameObject.FindObjectOfType<EnemyT>());
            print(enemyListT.Count);
        }
    }
}
