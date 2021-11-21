using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int x, y;

    GameManager gm;

    int[] UP = { -1, 0 };
    int[] DOWN = { 1, 0 };
    int[] LEFT = { 0, -1 };
    int[] RIGHT = { 0, 1 };

    int[] around;

    public void Init(int setX, int setY)
    {
        x = setX;
        y = setY;

        Debug.Log(name + ": x = " + x + ", y = " + y);
    }

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        WriteLog(gm.GetReady(x, y));
    }

    public void Action1()
    {
        around = gm.GetReady(x, y);
    }

    public void Action2()
    {
        // ‰Â•Ï
        gm.Put(x, y, RIGHT);
    }

    void WriteLog(int[] array)
    {
        string text = name + ": [ ";
        foreach (int i in array)
        {
            text += i + " ";
        }
        text += "]";

        Debug.Log(text);
    }
}
