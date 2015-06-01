using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    //아이템 스텍변수 items를 생성 
    private Stack<Item> items;

    public Stack<Item> Items
    {
        get { return items; }
        set { items = value; }
    }

    //텍스트 사용
    public Text stackTxt;

    //스프라이트 사용
    public Sprite slotEmpty;
    public Sprite slotHighlight;

    //items 리스트가 비여있는지 확인
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
        //items 초기화
        items = new Stack<Item>();

        //slotRect, txtRect를 RectTransform 컨퍼넌트로 초기화
        RectTransform slotRect = GetComponent<RectTransform>();
        RectTransform txtRect = stackTxt.GetComponent<RectTransform>();

        //slotRect의 사이즈 델타 값에 60%fmf txtScaleFactor 변수에 대입
        int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.6f);

        stackTxt.resizeTextMaxSize = txtScaleFactor;
        stackTxt.resizeTextMinSize = txtScaleFactor;

        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
        txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
	}

    public void AddItem(Item item)
    {
        items.Push(item);

        if (items.Count > 1)
        {
            print("Item");
            stackTxt.text = items.Count.ToString();
        }

        ChangeSprite(item.spriteNeutral, item.spriteHighlighted);
    }

    public void AddItems(Stack<Item> items)
    {
        this.items = new Stack<Item>(items);

        stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

        ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);
    }

    //스프라이트 이미지 변경 함수
    void ChangeSprite(Sprite neutral, Sprite highlight)
    {
        //기본 이미지를 적용
        this.GetComponent<Image>().sprite = neutral;

        //SpriteState 컨포넌트를 활용해 이미지를 셋팅한다
        SpriteState st = new SpriteState();
        st.highlightedSprite = highlight;
        st.pressedSprite = neutral;

        this.GetComponent<Button>().spriteState = st;
    }

    void UseItem()
    {
        if (!IsEmpty)
        {
            items.Pop().Use();

            stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

            if (IsEmpty)
            {
                ChangeSprite(slotEmpty, slotHighlight);
                Inventory.EmptySlot--;
            }
        }
    }

    public void ClearSlot()
    {
        items.Clear();
        ChangeSprite(slotEmpty, slotHighlight);
        stackTxt.text = string.Empty;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }
}
