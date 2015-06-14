using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour 
{
    //인벤토리 UI의 렉트트랜스폼 설정
    private RectTransform inventoryRect;

    //인벤의 가로, 세로의 값
    private float inventoryWidth, inventoryHight;

    //정수 타입의 스롯,로우 갯수
    public int slots,rows;

    //실수 타입의 스롯의 왼쪽, 윗쪽 간격
    public float slotPadingLeft, slotPadingTop;

    //실수 타입의 슬롯 사이즈
    public float slotSize;

    //슬롯 프리팹 지정
    public GameObject slotPrefab;

    public GameObject iconPrefab;

    public Canvas canvas;

    public EventSystem eventSystem;

    private static GameObject hoverObject;

    //올스롯 게임리스트 변수 선언
    private List<GameObject> allSlots;

    private static int emptySlot;

    private static Slot from, to;

    private float hoverYOffset;

    private static CanvasGroup canvasgroup;

    public static CanvasGroup Canvasgroup
    {
        get { return Inventory.canvasgroup; }
    }

    private bool fadingIn, fadingOut;

    public float fadingTime;

    // 칼 인벤토리 시작 --------------------------------------------------
    public static GameObject clicked;

    public GameObject selectStackSize;

    private static GameObject selectStackSizeStatic;

    public Text stackText;

    private int splitAmount;

    private int maxStackCount;

    private static Slot movingSlot;

    public GameObject mana;
    public GameObject health;

    //---------------------------------------------------------------------

    public static int EmptySlot
    {
        get { return emptySlot; }
        set { emptySlot = value; }
    }

    public GameObject toolTipObject;
    private static GameObject toolTip;

    public Text sizeTextObject;
    private static Text sizeText;

    public Text visualTextObjct;
    private static Text visualText;

    //---------------------------------------------------------------------------

    public GameObject dropItem;

    private static GameObject playerRef;

    //---------------------------------------------------------------------------


	// Use this for initialization
	void Start () 
    {
        toolTip = toolTipObject;

        sizeText = sizeTextObject;

        visualText = visualTextObjct;

        //------------------------------------

        playerRef = GameObject.Find("Player");

        selectStackSizeStatic = selectStackSize;

        //movingSlot = GameObject.Find("MovingSlot").GetComponent<Slot>();

        //-----------------------------------


        canvasgroup = transform.parent.GetComponent<CanvasGroup>();

        //시작 하면서 레이아웃 함수를 호출
        CreateLayout();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!eventSystem.IsPointerOverGameObject(-1) && from != null)
            {
                from.GetComponent<Image>().color = Color.white;

                foreach (Item item in from.Items)
                {
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

                    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                    v *= 30; 

                    GameObject.Instantiate(dropItem, playerRef.transform.position - v, 
                        Quaternion.identity);
                }

                from.ClearSlot();
                Destroy(GameObject.Find("Hover"));

                to = null;
                from = null;
                //hoverObject = null;
                emptySlot++;
            }
            else if (!eventSystem.IsPointerOverGameObject(-1) && !movingSlot.IsEmpty)
            {
                foreach (Item item in movingSlot.Items)
                {
                    float angle = UnityEngine.Random.Range(0.0f, Mathf.PI * 2);

                    Vector3 v = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));

                    v *= 30;

                    GameObject tmpDrp = (GameObject)GameObject.Instantiate(dropItem, playerRef.transform.position - v,
                        Quaternion.identity);

                    tmpDrp.GetComponent<Item>().SetStats(item);
                }

                movingSlot.ClearSlot();
                Destroy(GameObject.Find("Hover"));
            }
        }

        if (hoverObject != null)
        {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out position);

            position.Set(position.x, position.y - hoverYOffset);

            hoverObject.transform.position = canvas.transform.TransformPoint(position);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (canvasgroup.alpha > 0)
            {
                StartCoroutine("FadeOut");
            }
            else
            {
                StartCoroutine("FadeIn");
            }
        }
	}

    public void ShowToolTip(GameObject slot)
    {
        Slot tmpSlot = slot.GetComponent<Slot>();

        if (!tmpSlot.IsEmpty && hoverObject == null)
        {
            visualText.text = tmpSlot.CurrentItem.GetTooltip();
            sizeText.text = visualText.text;

            toolTip.SetActive(true);

            float xPos = slot.transform.position.x + slotPadingLeft;
            float yPos = slot.transform.position.y - slot.GetComponent<RectTransform>().sizeDelta.y - slotPadingTop;

            toolTip.transform.position = new Vector2(xPos, yPos);
        }

        
    }


    public void HideToolTip()
    {
        toolTip.SetActive(false);
    }

    //인벤토리 레이아웃 함수
    private void CreateLayout()
    {
        //게임 오브젝트 리스트로 allslots 변수 초기화
        allSlots = new List<GameObject>();

        hoverYOffset = slotSize * 0.01f;

        //스롯의 갯수를 emptySlot 에 저장
        emptySlot = slots;

        //인벤의 가로와 세로 사이즈 만든다.
        inventoryWidth = (slots / rows) * (slotSize + slotPadingLeft) + slotPadingLeft;
        inventoryHight = rows * (slotSize + slotPadingTop) + slotPadingTop;

        //inventoryRect를 RectTransform으로 초기화 한다.
        inventoryRect = GetComponent<RectTransform>();

        //inventoryRect의 가로 세로 사이즈에 inventoryWidth, inventoryhight 변수를 넣어준다.
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHight);

        //한줄의 스롯 갯수 컬럼에 저장
        int colums = slots / rows;


        //줄의 갯수만큼 생성
        for (int y = 0; y < rows; y++)
        {
            //컬럼갯수만큼 생성
            for (int x = 0; x < colums; x++)
            {
                //newSlot 으로 인스턴스 스롯프리팹을 생성한다.
                GameObject newSlot = (GameObject)Instantiate(slotPrefab);

                //생성된 newSlot 에 slotRect변수에 RectTransform 컨퍼넌트로 초기화한다.
                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                //newSlot의 이름은 Slot으로 정한다.
                newSlot.name = "Slot";

                //newSlot의 부모를 현재 Inventory로 셋팅한다.
                newSlot.transform.SetParent(this.transform.parent.FindChild("SlotList"));

                //slotRect의 로컬포지션을 아래 공식으로 생성한다.
                slotRect.localPosition = inventoryRect.localPosition + new Vector3
                    (slotPadingLeft * (x + 1) + (slotSize * x), -slotPadingLeft * (y + 1) - (slotSize * y));

                //slotRect의 사이즐를 slotSize로 정한다.
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize * canvas.scaleFactor);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize * canvas.scaleFactor);

                allSlots.Add(newSlot);
            }
        }
    }

    public bool AddItem(Item item)
    {
        if (item.maxSize == 1)
        {
            PlaceEmpty(item);
            return true;
        }

        else
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();

                if (!tmp.IsEmpty)
                {
                    if (tmp.CurrentItem.type == item.type && tmp.IsAvailable)
                    {
                        tmp.AddItem(item);
                        //emptySlot--;
                        return true;
                    }
                }
            }
            //여기때문에 maxSlot이 작동 안했음
            if (emptySlot > 0)
            {
                PlaceEmpty(item);
            }
        }
        return false;
    }

    bool PlaceEmpty(Item item)
    {
        if (emptySlot > 0)
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();

                if (tmp.IsEmpty)
                {
                    tmp.AddItem(item);
                    emptySlot--;
                    return true;
                }
            }
        }
        return false;
    }

    public void MoveItem(GameObject clicked)
    {
        if (from == null)
        {
            if (!clicked.GetComponent<Slot>().IsEmpty)
            {
                print("MoveItem");

                from = clicked.GetComponent<Slot>();
                from.GetComponent<Image>().color = Color.gray;

                hoverObject = (GameObject)Instantiate(iconPrefab);
                hoverObject.GetComponent<Image>().sprite = clicked.GetComponent<Image>().sprite;
                hoverObject.name = "Hover";

                RectTransform hoverTransform = hoverObject.GetComponent<RectTransform>();
                RectTransform clickedTransform = clicked.GetComponent<RectTransform>();

                hoverTransform.SetSizeWithCurrentAnchors
                    (RectTransform.Axis.Horizontal, clickedTransform.sizeDelta.x);
                hoverTransform.SetSizeWithCurrentAnchors
                    (RectTransform.Axis.Vertical, clickedTransform.sizeDelta.y);

                hoverObject.transform.SetParent(GameObject.Find("Canvas").transform, true);
                hoverObject.transform.localScale = from.gameObject.transform.localScale;
            }
            
        }
        else if (to == null)
        {
            print("to");
            to = clicked.GetComponent<Slot>();
            Destroy(GameObject.Find("Hover"));
        }

        if (to != null && from != null)
        {
            print("to from");
            Stack<Item> tmpTo = new Stack<Item>(to.Items);
            to.AddItems(from.Items);

            if (tmpTo.Count == 0)
            {
                from.ClearSlot();
            }
            else
            {
                from.AddItems(tmpTo);
            }

            from.GetComponent<Image>().color = Color.white;

            to = null;
            from = null;
            hoverObject = null;
        }
    }

    private IEnumerator FadeOut()
    {
        if (!fadingOut)
        {
            fadingOut = true;
            fadingIn = false;

            StopCoroutine("FadeIn");

            float startAlpha = canvasgroup.alpha;

            float rate = 1.0f / fadingTime;

            float progress = 0.0f;

            while (progress > 0)
            {
                canvasgroup.alpha = Mathf.Lerp(startAlpha, 0, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }

            canvasgroup.alpha = 0;
            fadingOut = false;
        }
        
    }

    private IEnumerator FadeIn()
    {
        fadingOut = false;
        fadingIn = true;

        StopCoroutine("FadeOut");

        float startAlpha = canvasgroup.alpha;

        float rate = 1.0f / fadingTime;

        float progress = 0.0f;

        while (progress > 1)
        {
            canvasgroup.alpha = Mathf.Lerp(startAlpha, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }

        canvasgroup.alpha = 1.0f;
        fadingIn = false;
    }
}
