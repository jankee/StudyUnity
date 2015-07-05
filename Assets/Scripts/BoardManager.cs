using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class BoardManager : MonoBehaviour {
    //직렬화를 통해 minimum과 maximun을 저장한다.
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximun;

        public Count(int min, int max)
        {
            minimum = min;
            maximun = max;
        }
    }
    // int타입의 행과 열을 정한다.
    public int colums = 8;
    public int rows = 8;
    //Count형식의 wallCount, foodCount의 미니멈과 맥시멈을 정한다.
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    // exit게임 오브젝트를 생성
    public GameObject exit;
    //각 타일의 배열들을 정의한다.
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTieles;
    public GameObject[] outerWallTiles;

    //각종 타일들의 상위 객체의 설정한다.
    private Transform boardHolder;
    public List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList()
    {
        //그리드포지션을 클리어 한다
        gridPositions.Clear();

        //그리드포지션에 컬럼과 로우만큼의 포지션을 저장한다.
        for (int x = 0; x < colums - 1; x++)
        {
            for (int y = 0; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        //Board 게임 오브젝트를 생성한다.
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < colums + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == colums || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }
            }
        }
    }

	// Use this for initialization
	void Start () 
    {
        BoardSetup();
        print(boardHolder.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
