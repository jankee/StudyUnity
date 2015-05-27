using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
    private RectTransform inventoryRect;

    private float inventoryWidth, inventoryHight;

    public int slots;
    public int rows;

    public float slotPadingLeft, slotPadingTop;

    public float slotSize;

    public GameObject slotPrefab;

    private List<GameObject> allSlots;


	// Use this for initialization
	void Start () 
    {
        CreateLayout();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    private void CreateLayout()
    {
        allSlots = new List<GameObject>();

        inventoryWidth = (slots / rows) * (slotSize + slotPadingLeft) + slotPadingLeft;
        inventoryHight = rows * (slotSize + slotPadingTop) + slotPadingTop;
        print(slotSize + slotPadingLeft);
        print("inventoryWidth" + inventoryWidth);

        inventoryRect = GetComponent<RectTransform>();

        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight);

        int colums = slots / rows;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < colums; x++)
            {
                
            }
        }
    }
}
