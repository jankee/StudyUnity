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
                //floorTile의 랜덤 선택
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                //외각쪽은 outerWallTiles 중 랜덤 하게 선택을 한다.
                if (x == -1 || x == colums || y == -1 || y == rows)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }

                //랜덤선택 후 생성한다.
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }

    }

    //gridPosition의 랜덤 포지션을 선택한다.
    Vector3 RandomPosition()
    {
        //randomIndex에 랜덤 포지션을 지정한다.
        int randomIndex = Random.Range(0, gridPositions.Count);
        //randomPosition에 랜덤 포지션을 지정
        Vector3 randomPosition = gridPositions[randomIndex];
        //지정된 랜덤 인덱스의 좌표를 제거
        gridPositions.RemoveAt(randomIndex);
        //randomPosition을 반환
        return randomPosition;
    }


    //각종 타일들 랜덤 선택
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximun)
    {
        int objectCount = Random.Range(minimum, maximun + 1);
        for (int i = 0; i < objectCount; i++)
		{
		    Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
		}
    }

	// Use this for initialization
	public void StartSetup (int level) 
    {
        BoardSetup();
        InitialiseList();
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximun);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximun);
        //확인 해봐야 되는 Mathf.Log
        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemyTieles, enemyCount, enemyCount);

        Instantiate(exit, new Vector3(colums - 1, rows - 1, 0f), Quaternion.identity);
	}
	
}
