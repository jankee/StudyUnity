using UnityEngine;
using System.Collections;

public enum ItemType
{
    MANA,
    HEALTH,
}

public class Item : MonoBehaviour 
{

    public ItemType type;

    //기본 스프라이트
    public Sprite spriteNeutral;

    //하이라이트 스프라인트
    public Sprite spriteHighlighted;

    public int maxSize;


    public void Use()
    {
        switch (type)
        {
            case ItemType.MANA:
                print("I just used a mana potion");
                break;
            case ItemType.HEALTH:
                print("I just used a health potion");
                break;
        }
    }
}
