using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int posX, posY;

    public GameManager gm;
    Transform mesh;

    int[] UP    = {  0, -1 };
    int[] DOWN  = {  0,  1 };
    int[] LEFT  = { -1,  0 };
    int[] RIGHT = {  1,  0 };

    public int[] around;

    bool motionFlag = false;

    int step = 8;

    public void Init(int setX, int setY)
    {
        posX = setX;
        posY = setY;
        //Debug.Log(name + ": x = " + posX + ", y = " + posY);
    }

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        mesh = transform.Find("default");
    }

    public int[] GetReady()
    {
        int[] temp = gm.GetReady(posX, posY);
        WriteLog(temp, "GetReady");
        return temp;
    }

    public void Walk(int[] dir) //dir = up: , left: , right: , down;
    {
        WriteLog(dir, "Walk");
        gm.Walk(posX, posY, dir);
        //transform.Translate(dir[0], 0, -dir[1]);
        StartCoroutine(WalkMotion(dir));
        posX += dir[0];
        posY += dir[1];
    }

    IEnumerator WalkMotion(int[] dir)
    {
        while(motionFlag) yield return null;
        for(int i = 0; i < step; i++)
        {
            transform.Translate(
                (float)dir[0] / (float)step,
                0,
                -(float)dir[1] / (float)step);
            yield return null;
        }
    }

    public void Put(int[] dir)
    {
        WriteLog(dir, "Put");
        gm.Put(posX, posY, dir);
    }

    public int[] Look(int[] dir)
    {
        WriteLog(dir, "Look");
        int[] temp = gm.Look(posX, posY, dir);
        WriteLog(temp, "Look");
        return temp;
    }

    public int[] Search(int[] dir)
    {
        WriteLog(dir, "Serch");
        int[] temp = gm.Search(posX, posY, dir);
        WriteLog(temp, "Serch");
        return temp;
    }

    public void WriteLog(int[] array, string com)
    {
        string text = name + ": " + com + ": [ ";
        foreach (int i in array)
        {
            text += i + " ";
        }
        text += "]";

        Debug.Log(text);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);

        other.GetComponent<ItemController>().Point();

    }

    public int[] SetDir(int dir)
    {
        switch (dir)
        {
            case 0:
                //mesh.eulerAngles = new Vector3(0, 0, 0);
                motionFlag = true;
                StartCoroutine(RotateMotion(0));
                return UP;
            case 1:
                //mesh.eulerAngles = new Vector3(0, 270, 0);
                motionFlag = true;
                StartCoroutine(RotateMotion(270));
                return LEFT;
            case 2:
                //mesh.eulerAngles = new Vector3(0, 90, 0);
                motionFlag = true;
                StartCoroutine(RotateMotion(90));
                return RIGHT;
            case 3:
                //mesh.eulerAngles = new Vector3(0, 180, 0);
                motionFlag = true;
                StartCoroutine(RotateMotion(180));
                return DOWN;
            default:
                return null;
        }
    }

    IEnumerator RotateMotion(int targetDeg)
    {
        float nowDeg = mesh.eulerAngles.y;

        if (Mathf.Abs(targetDeg - nowDeg) >= 270)
        {
            for (int i = 1; i <= step; i++)
            {
                mesh.eulerAngles = new Vector3(0, nowDeg + (nowDeg - targetDeg) / 3 / step * i, 0);
                yield return null;
            }
        }
        else
        {
            for (int i = 1; i <= step; i++)
            {
                mesh.eulerAngles = new Vector3(0, nowDeg + (targetDeg - nowDeg) / step * i, 0);
                yield return null;
            }
        }

        
        motionFlag = false;
        yield return null;
    }
}
