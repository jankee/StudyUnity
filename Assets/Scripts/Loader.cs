using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour 
{
    //GameManager 인스터스화
    public GameObject gameManager;

	// Use this for initialization
	void Awake () 
    {

        Screen.SetResolution(800, 480, true);
        //GameManager의 static instance를 바로 불러올수 있다.

        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
	}
	
}
