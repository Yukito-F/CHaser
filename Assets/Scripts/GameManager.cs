using UnityEngine;

public class GameManager : MonoBehaviour
{
    //GameObject[,] walls = new GameObject[17, 15];// ‘½•ª•s—v
    const int WIDTH = 15;
    const int HEIGHT = 17;
    byte[,] map = new byte[HEIGHT, WIDTH]; //0:empty, 1:enemy, 2:block, 3:item

    int[] UP    = { -1, 0 };
    int[] DOWN  = {  1, 0 };
    int[] LEFT  = {  0, -1};
    int[] RIGHT = {  0, 1 };

    GameObject pref;

    private void Start()
    {
        mapInit();
        pref = (GameObject)Resources.Load("Wall");
        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                 
                if (map[i, j] == 2)
                {
                    //walls[i, j] =
                    Instantiate(
                        pref,
                        new Vector3(j, 0, HEIGHT - 1 - i),
                        Quaternion.identity
                        );
                }
            }
        }
    }

    void mapInit()
    {
        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                if (i == 0 || i == HEIGHT-1 || j == 0 || j == WIDTH-1)
                {
                    map[i, j] = 2;
                }
                else
                {
                    map[i, j] = 0;
                }

            }
        }
    }

    public int[] GetReady(int x, int y)
    {
        int[] result = new int[9];

        for(int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (CheckArea(x - 1 + j, y - 1 + i))
                {
                    result[j + 3 * i] = 2;
                }
                else if (i == 1 && j == 1)
                {
                    result[j + 3 * i] = 0;
                }
                else
                {
                    result[j + 3 * i] = map[y - 1 + i, x - 1 + j];
                }
            }
        }

        return result;
    }

    public void Walk(int x, int y, int[] dir) //dir = up: , left: , right: , down;
    {
        map[y, x] = 0;
        if(CheckArea(x + dir[1], y + dir[0]))
        {
            // •‰‚¯
            Debug.Log("??????????????????????");
        }
        else
        {
            map[y + dir[0], x + dir[1]] = 1;
        }
    }

    public void Put(int x, int y, int[] dir)
    {
        if (!CheckArea(x + dir[1], y + dir[0]))
        {
            if (map[y + dir[0], x + dir[1]] == 1)
            {
                //Ÿ‚¿
                Debug.Log("YOU WIN!!");
            }
            map[y + dir[0], x + dir[1]] = 2;
            Instantiate(pref, new Vector3(x + dir[1], 0, HEIGHT - 1 - (y + dir[0])), Quaternion.identity);
        }
    }

    public int[] Look(int x, int y, int[] dir)
    {
        int[] result = new int[9];
        int targetX = x + 2 * dir[1], targetY = y + 2 * dir[0];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (CheckArea(targetX - 1 + j, targetY - 1 + i))
                {
                    result[j + 3 * i] = 2;
                }
                else
                {
                    result[j + 3 * i] = map[targetY - 1 + i, targetX - 1 + j];
                }
            }
        }

        return result;
    }

    public int[] Search(int x, int y, int[] dir)
    {
        int[] result = new int[9];

        for (int i = 1; i <= 9; i++)
        {
            if (CheckArea(x + dir[1] * i, y + dir[0] * i))
            {
                result[i - 1] = 2;
            }
            else
            {
                result[i - 1] = map[y + dir[0] * i, x + dir[1] * i];
            }
        }

        return result;
    }

    private bool CheckArea(int x, int y) // êŠO”»’è
    {
        return x < 0 || x > WIDTH - 1 || y < 0 || y > HEIGHT - 1;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            foreach (int k in GetReady(14, 16))
            {
                Debug.Log(k);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {

            Put(1, 1, RIGHT);
        }
    }
}