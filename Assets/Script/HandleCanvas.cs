using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandleCanvas : MonoBehaviour {

    
    private CanvasScaler scaler;

	// Use this for initialization
	void Start () 
    {
        scaler = GetComponent<CanvasScaler>();

        //캔버스 스케일러를 사용하여 시작할 때 마다 스케일모드를 고정시킨다 
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
