using UnityEngine;
using System.Collections;
using System;

public class Test : MonoBehaviour 
{
    public void Awake()
    {
        
    }


    public void Start()
    {
        Main();
    }
    

    static void Main()
    {
        MyList<string> str_list = new MyList<string>();
        str_list[0] = "abc";
        str_list[1] = "def";
        str_list[2] = "jkl";
        str_list[3] = "mno";

        for (int i = 0; i < str_list.Length; i++)
        {
            Debug.Log(str_list[i]);
        }
        Debug.Log("");

        MyList<int> int_list = new MyList<int>();
        int_list[0] = 0;
        int_list[1] = 1;
        int_list[2] = 2;
        int_list[3] = 3;
        int_list[4] = 4;

        for (int i = 0; i < int_list.Length; i++)
        {
            Debug.Log(int_list[i]);
        }

    }
}

public class MyList<T>
{
    private T[] array;

    public MyList()
    {
        array = new T[3];
    }

    public T this[int index]
    {
        get
        {
            return array[index];
        }
        set
        {
            if (index >= array.Length)
            {
                Array.Resize<T>(ref array, index + 1);
                Debug.Log("Array Resized : " + array.Length);
            }
            array[index] = value;
        }
    }

    public int Length
    {
        get
        {
            return array.Length;
        }
    }
}
