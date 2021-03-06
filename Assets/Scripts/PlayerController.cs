using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int posX, posY;

    public GameManager gm;

    int[] UP    = {  0, -1 };
    int[] DOWN  = {  0,  1 };
    int[] LEFT  = { -1,  0 };
    int[] RIGHT = {  1,  0 };

    public int[] around;

    public void Init(int setX, int setY)
    {
        posX = setX;
        posY = setY;

        //Debug.Log(name + ": x = " + posX + ", y = " + posY);
    }

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //WriteLog(gm.GetReady(posX, posY));
    }

    public void Walk(int x, int y, int[] dir) //dir = up: , left: , right: , down;
    {
        WriteLog(dir, "Walk");
        gm.Walk(x, y, dir);
        transform.Translate(dir[0], 0, -dir[1]);
        posX += dir[0];
        posY += dir[1];
    }

    public void Put(int x, int y, int[] dir)
    {
        WriteLog(dir, "Put");
        gm.Put(x, y, dir);
    }

    public int[] Look(int x, int y, int[] dir)
    {
        WriteLog(dir, "Look");
        return gm.Look(x, y, dir);
    }

    private int[] Search(int x, int y, int[] dir)
    {
        WriteLog(dir, "Serch");
        return gm.Search(x, y, dir);
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
        Destroy(other.gameObject);
    }

    public int[] SetDir(int dir)
    {
        switch (dir)
        {
            case 0:
                return UP;
            case 1:
                return LEFT;
            case 2:
                return RIGHT;
            case 3:
                return DOWN;
            default:
                return null;
        }
    }
}
