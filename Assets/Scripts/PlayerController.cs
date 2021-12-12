using UnityEngine;

public class PlayerController : MonoBehaviour
{
    int posX, posY;

    GameManager gm;

    int[] UP    = {  0, -1 };
    int[] DOWN  = {  0,  1 };
    int[] LEFT  = { -1,  0 };
    int[] RIGHT = {  1,  0 };

    int[] around;

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

    public void Action1()
    {
        around = gm.GetReady(posX, posY);
        WriteLog(around, "GetReady");
    }

    public void Action2()
    {
        // ‰Â•Ï
        //Put(x, y, RIGHT);
        WriteLog(new int[] { posX, posY }, "Pos");
        Walk(posX, posY, RIGHT);
    }

    private void Walk(int x, int y, int[] dir) //dir = up: , left: , right: , down;
    {
        WriteLog(dir, "Walk");
        gm.Walk(x, y, dir);
        transform.Translate(dir[0], 0, -dir[1]);
        posX += dir[0];
        posY += dir[1];
    }

    private void Put(int x, int y, int[] dir)
    {
        WriteLog(dir, "Put");
        gm.Put(x, y, dir);
    }

    private int[] Look(int x, int y, int[] dir)
    {
        WriteLog(dir, "Look");
        return gm.Look(x, y, dir);
    }

    private int[] Search(int x, int y, int[] dir)
    {
        WriteLog(dir, "Serch");
        return gm.Search(x, y, dir);
    }

    void WriteLog(int[] array, string com)
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
}
