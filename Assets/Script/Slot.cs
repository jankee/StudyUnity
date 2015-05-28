using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Slot : MonoBehaviour 
{
    private Stack<Item> items;

    public Text stackTxt;

    public Sprite slotEmpty;
    public Sprite slotHighlight;

    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }

    public bool IsAvailable
    {
        get
        {
            return CurrentItem.maxSize > items.Count;
        }
    }

    public Item CurrentItem
    {
        get
        {
            return items.Peek();
        }
    }

	// Use this for initialization
	void Start () 
    {
        items = new Stack<Item>();

        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform txtRect = stackTxt.GetComponent<RectTransform>();

        int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.6f);

        stackTxt.resizeTextMaxSize = txtScaleFactor;
        stackTxt.resizeTextMinSize = txtScaleFactor;

        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void AddItem(Item item)
    {
        items.Push(item);

        if (items.Count > 1)
        {
            
            stackTxt.text = items.Count.ToString();
        }

        ChageSprite(item.spriteNeutral, item.spriteHighlighted);
    }

    void ChageSprite(Sprite neutral, Sprite highlight)
    {
        this.GetComponent<Image>().sprite = neutral;

        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;

        this.GetComponent<Button>().spriteState = st;
    }
}
